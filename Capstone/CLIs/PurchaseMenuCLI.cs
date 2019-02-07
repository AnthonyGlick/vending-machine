using System;
using System.Collections.Generic;
using System.Text;
using Capstone.VendingMachineFolder;

namespace Capstone.CLIs
{
    class PurchaseMenuCLI : CLI
    {
        public override void Run(VendingMachine vm)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1) Feed Money");
                Console.WriteLine("2) Select Product");
                Console.WriteLine("3) Finish Transaction");
                Console.WriteLine("B) Back");
                Console.WriteLine();
                Console.WriteLine($"Current money provided: ${vm.CurrentBal}"); 
                string purchaseChoice = GetString("> Pick one: ").ToLower();

                if (purchaseChoice == "1")
                {
                    FeedMoney(vm);
                }
                else if (purchaseChoice == "2")
                {
                    SelectProduct();
                }
                else if (purchaseChoice == "3")
                {
                    FinishTransaction();
                }
                else if (purchaseChoice == "b")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option.");
                    Console.ReadLine();
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

        public void SelectProduct()
        {

        }
        public void FinishTransaction()
        {

        }
    }
}
