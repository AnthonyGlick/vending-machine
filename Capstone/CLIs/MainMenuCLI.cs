using Capstone.CLIs;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.VendingMachineFolder;
using System.IO;


namespace Capstone.CLIs
{
    public class MainMenuCLI : CLI
    {
        public override void Run(VendingMachine vm)
        {
            while (true)
            {
                Console.WriteLine("Welcome To The Vendo-Matic 500! ");
                Console.WriteLine("Please select an option: ");
                Console.WriteLine("1) Display Inventory");
                Console.WriteLine("2) Purchase");
                Console.WriteLine("Q) Quit");
                string mainChoice = GetString("> Selection: ").ToLower();

                if (mainChoice == "1")
                {
                    DisplayInv();
                    Console.ReadLine();
                }
                else if (mainChoice == "2")
                {
                    PurchaseMenuCLI purchaseMenu = new PurchaseMenuCLI();
                    purchaseMenu.Run(vm);
                }
                else if (mainChoice == "q")
                {
                    break;
                }
                else
                {
                    
                    Console.WriteLine("Invalid option.");
                    Console.WriteLine("");
                }
                Console.Clear();
            }
        }

        public void DisplayInv()
        {
            Console.Clear();
            using (StreamReader sr = new StreamReader("vendingmachine.csv"))
            {
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split("|");
                    string code = line[0];
                    string name = line[1];
                    decimal price = decimal.Parse(line[2]);
                    string type = line[3];
                    Console.WriteLine($"{code, - 3} {name, - 20} {price, -5} {type}");
                }
            }
            Console.ReadLine();
        }
    }
}
