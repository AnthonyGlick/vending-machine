using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Capstone.VendingMachineFolder;

namespace Capstone.CLIs
{
    public class PurchaseMenuCLI : CLI
    {
        private VendingMachine vm;
        public PurchaseMenuCLI(VendingMachine vm)
        {
            this.vm = vm;
        }

        private List<VendingMachineItem> purchased = new List<VendingMachineItem>();

        public override void Run()
        {
            string error = string.Empty;
            AuditWriter auditWriter = new AuditWriter();

            while (true)
            {
                Console.Clear();
                Console.Write("Errors: ");
                Console.WriteLine(error);
                Console.WriteLine("1) Feed Money");
                Console.WriteLine("2) Select Product");
                Console.WriteLine("3) Finish Transaction");
                Console.WriteLine("B) Back");
                Console.WriteLine();
                Console.WriteLine($"Current money provided: ${vm.CurrentBal}");
                string purchaseChoice = this.GetString("> Selection: ").ToLower();

                if (purchaseChoice == "1")
                {
                    decimal oldBal = vm.CurrentBal;

                    this.DisplayFeedMoney(vm);
                    auditWriter.WriteAudit(vm.CurrentBal, "FEED MONEY", oldBal);

                    error = string.Empty;
                }
                else if (purchaseChoice == "2")
                {
                    VendingMachineItem purchasedItem = this.DisplaySelectProduct(vm);
                    if (purchasedItem != null)
                    {
                        decimal oldBal = vm.CurrentBal;


                        this.purchased.Add(purchasedItem);

                        error = string.Empty;

                        auditWriter.WriteAudit(vm.CurrentBal, $"{purchasedItem.Name} {purchasedItem.Slot}", oldBal);
                    }
                    else
                    {
                        error = "Please try again.";
                    }
                }
                else if (purchaseChoice == "3")
                {
                    if (this.purchased.Count > 0)
                    {
                        decimal oldBal = vm.CurrentBal;

                        this.FinishTransaction(vm);

                        error = string.Empty;

                        auditWriter.WriteAudit(vm.CurrentBal, "GIVE CHANGE", oldBal);
                        

                        foreach (VendingMachineItem purchase in this.purchased)
                        {
                            Console.WriteLine(purchase.MakeFoodSound());
                        }

                        this.WriteSales(vm);

                        this.purchased.RemoveRange(0, this.purchased.Count - 1);

                        Console.ReadLine();

                        return;
                    }
                    else
                    {
                        error = "You havent purchased anything silly!";
                    }
                }
                else if (purchaseChoice == "b")
                {
                    break;
                }
                else
                {
                    error = "Invalid option.";
                }

                Console.Clear();
            }
        }

        /// <summary>
        /// Displays the feed money prompt and adds the balance to the machine.
        /// </summary>
        /// <param name="vm"></param>
        public void DisplayFeedMoney(VendingMachine vm)
        {
            Console.Clear();
            Console.WriteLine("How much money do you wish to put in?");
            Console.WriteLine("(Accepts $1, $2, $5, $10)");

            while (true)
            {
                decimal amount = decimal.Parse(this.GetString("> Type amount here: "));
                if (amount == 1.00M || amount == 2.00M || amount == 5.00M || amount == 10.0M)
                {
                    vm.AddBal(amount);
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter a valid dollar amount");
                }
            }

            return;
        }

        /// <summary>
        /// Prompts the user to select a product and purchases it.
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public VendingMachineItem DisplaySelectProduct(VendingMachine vm)
        {
            bool tryAgain = true;
            VendingMachineItem purchasedItem = null;
            while (tryAgain == true)
            {
                Console.Clear();
                string code = this.GetString("Please enter product code: ").ToUpper();
                try
                {
                    purchasedItem = vm.PurchaseItem(code);
                    tryAgain = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                    return null;
                }
            }

            return purchasedItem;
        }

        

        // TODO Outputs blank file for some reason
        public void WriteSales(VendingMachine vm)
        {
            string[] line = new string[2];

            using (StreamReader sr = new StreamReader("sales.txt"))
            {
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine().Split("|");
                }
            }

            using (StreamWriter sw = new StreamWriter("sales1.txt"))
            {
                for (int i = 0; i < this.purchased.Count - 1; i++)
                {
                    if (line[0] == this.purchased[i].Name)
                    {
                        int quantity = int.Parse(line[1]);
                        quantity++;
                        sw.WriteLine($"{line[0]}|{quantity}");
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        public void FinishTransaction(VendingMachine vm)
        {
            vm.ChangeBack();
            return;
        }
    }
}
