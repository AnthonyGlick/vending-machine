using System;
using System.IO;
using Capstone.CLIs;
using System.Collections.Generic;
using System.Text;
using Capstone.VendingMachineFolder;

namespace Capstone.CLIs
{
    public class MainMenuCLI : CLI
    {
        public override void Run(VendingMachine vm)
        {
            string error = string.Empty;
            while (true)
            {
                Console.WriteLine(error);
                Console.WriteLine("Welcome To The Vendo-Matic 500! ");
                Console.WriteLine("Please select an option: ");
                Console.WriteLine("1) Display Inventory");
                Console.WriteLine("2) Purchase");
                Console.WriteLine("Q) Quit");
                Console.WriteLine();
                string mainChoice = this.GetString("> Selection: ").ToLower();

                if (mainChoice == "1")
                {
                    this.DisplayInv();
                    error = string.Empty;
                    Console.ReadLine();
                }
                else if (mainChoice == "2")
                {
                    PurchaseMenuCLI purchaseMenu = new PurchaseMenuCLI();
                    purchaseMenu.Run(vm);
                    error = string.Empty;
                }
                else if (mainChoice == "q")
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

        public void DisplayInv()
        {
            Console.Clear();
            Console.WriteLine("Inventory".PadLeft(23));
            Console.WriteLine();
            using (StreamReader sr = new StreamReader("vendingmachine.csv"))
            {
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split("|");
                    string code = line[0];
                    string name = line[1];
                    decimal price = decimal.Parse(line[2]);
                    string type = line[3];
                    Console.WriteLine($"{code, -3} {name, -20} ${price, -5} {type}");
                }
            }
        }
    }
}
