﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Capstone.VendingMachineFolder;

namespace Capstone.CLIs
{
    class PurchaseMenuCLI : CLI
    {
        List<VendingMachineItem> purchased = new List<VendingMachineItem>();

        public override void Run(VendingMachine vm)
        {
            while (true)
            {              
                Console.WriteLine("1) Feed Money");
                Console.WriteLine("2) Select Product");
                Console.WriteLine("3) Finish Transaction");
                Console.WriteLine("B) Back");
                Console.WriteLine();
                Console.WriteLine($"Current money provided: ${vm.CurrentBal}"); 
                string purchaseChoice = GetString("> Pick one: ").ToLower();

                if (purchaseChoice == "1")
                {
                    decimal oldBal = vm.CurrentBal;

                    FeedMoney(vm);

                    WriteAudit(vm, "FEED MONEY", oldBal);
                }
                else if (purchaseChoice == "2")
                {
                    decimal oldBal = vm.CurrentBal;

                    VendingMachineItem purchasedItem = SelectProduct(vm); 
                    purchased.Add(purchasedItem);



                    WriteAudit(vm, $"{purchasedItem.Name} {purchasedItem.Slot}", oldBal);
                }
                else if (purchaseChoice == "3")
                {
                    decimal oldBal = vm.CurrentBal;

                    FinishTransaction(vm);

                    WriteAudit(vm, "GIVE CHANGE", oldBal);

                    foreach(VendingMachineItem purchase in purchased)
                    {
                        purchase.MakeFoodSound();
                        purchased.Remove(purchase);
                    }
                    return;
                }
                else if (purchaseChoice == "b")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option.");
                    Console.WriteLine();
                }
            }
        }

        public void FeedMoney(VendingMachine vm)
        {
            Console.Clear();
            Console.WriteLine("How much money do you wish to put in?");
            Console.WriteLine("(Accepts $1, $2, $5, $10");
            decimal amount = decimal.Parse(GetString("> Type amount here: "));
            vm.AddBal(amount);
            return;
        }

        public VendingMachineItem SelectProduct(VendingMachine vm)
        {
            bool tryAgain = true;
            VendingMachineItem purchasedItem = null;
            while (tryAgain == true)
            {
                string code = GetString("Please enter product code: ").ToUpper();
                try
                {
                    purchasedItem = vm.CalcBal(code);
                    tryAgain = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return purchasedItem;
        }

        public void WriteAudit(VendingMachine vm, string action, decimal previousBal)
        {
            using(StreamWriter sw = new StreamWriter("audit.txt"))
            {
                foreach(VendingMachineItem purchase in purchased)
                {
                    sw.WriteLine($"{DateTime.Now,-15} {action,-20} {previousBal,-5} {vm.CurrentBal}");
                }
            }
        }

        public void WriteSales()
        {

        }

        public void FinishTransaction(VendingMachine vm)
        {
            vm.ChangeBack();
            return;
        }
    }
}
