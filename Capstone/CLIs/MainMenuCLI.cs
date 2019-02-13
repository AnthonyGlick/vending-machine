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
        private VendingMachine vm;
        public MainMenuCLI(VendingMachine vm)
        {
            this.vm = vm;
        }

        public override void Run()
        {
            string error = string.Empty;
            while (true)
            {
                Console.Write("Errors: ");
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
                    PurchaseMenuCLI purchaseMenu = new PurchaseMenuCLI(vm);
                    purchaseMenu.Run();
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

            string[] slots = vm.Slots;
            foreach (string slot in slots)
            {
                VendingMachineItem item = vm.GetItemAtSlot(slot);
                Console.WriteLine($"{item.Slot,-3} {item.Name,-20} ${item.Price,-5} {item.Type}");
            }
        }
    }
}
