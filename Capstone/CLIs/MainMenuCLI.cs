using DeliveryApp.CLIs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class MainMenuCLI : CLI
    {
        public override void Run()
        {
            while (true)
            {


                Console.Clear();
                Console.WriteLine("Please select an option: ");
                Console.WriteLine("1) Display Inventory");
                Console.WriteLine("2) Purchase");
                Console.WriteLine("Q) Quit");
                string mainChoice = GetString("> Pick one: ");

                if (mainChoice == "1")
                {
                    ViewInv();
                    Console.ReadLine();
                }
                else if (mainChoice == "2")
                {
                    PurchaseMenuCLI purchaseMenu = new PurchaseMenuCLI();
                    purchaseMenu.Run();
                }
                else if (mainChoice == "Q")
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
    }
}
