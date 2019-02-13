using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone.VendingMachineFolder
{
    public class VendingMachineStocker
    {
        public Dictionary<string, VendingMachineItem> GetInventory()
        {
            Dictionary<string, VendingMachineItem> inv = new Dictionary<string, VendingMachineItem>();

            try
            {
                using (StreamReader sr = new StreamReader("vendingmachine.csv"))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] itemInfo = sr.ReadLine().Split("|");

                        string slot = itemInfo[0];
                        string name = itemInfo[1];
                        decimal price = decimal.Parse(itemInfo[2]);
                        string type = itemInfo[3];

                        if (type == "Chip")
                        {
                            inv.Add(slot, new Chip(slot, name, price, type));
                        }
                        else if (type == "Candy")
                        {
                            inv.Add(slot, new Candy(slot, name, price, type));
                        }
                        else if (type == "Drink")
                        {
                            inv.Add(slot, new Drink(slot, name, price, type));
                        }
                        else
                        {
                            inv.Add(slot, new Gum(slot, name, price, type));
                        }
                    }
                }
            }
            catch(IOException ex)
            {
                Console.WriteLine("Error reading inventory.");
            }

            return inv;
        }
    }
}
