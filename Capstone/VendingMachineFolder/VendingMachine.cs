using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone.VendingMachineFolder
{
    public class VendingMachine
    {
        private Dictionary<string, VendingMachineItem> inv = new Dictionary<string, VendingMachineItem>();

        public decimal TotalSales { get; private set; }

        public decimal CurrentBal { get; private set; }

        public VendingMachine()
        {
            this.TotalSales = 0.00M;
            this.CurrentBal = 0.00M;

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
                        this.inv.Add(slot, new Chip(slot, name, price, type));
                    }
                    else if (type == "Candy")
                    {
                        this.inv.Add(slot, new Candy(slot, name, price, type));
                    }
                    else if (type == "Drink")
                    {
                        this.inv.Add(slot, new Drink(slot, name, price, type));
                    }
                    else
                    {
                        this.inv.Add(slot, new Gum(slot, name, price, type));
                    }
                }
            }
        }

        public void AddBal(decimal amount)
        {
          this.CurrentBal += amount;
        }

        public VendingMachineItem CalcBal(string code, VendingMachine vm)
        {
            while (true)
            {
                if (!this.inv.ContainsKey(code))
                {
                    throw new Exception("Invalid code, please try again.");
                }
                else if (vm.CurrentBal < this.inv[code].Price)
                {
                    throw new Exception("Not enough money to purchase.");
                }
                else if (this.inv[code].RemainingInventory == 0)
                {
                    throw new Exception("OUT OF STOCK");
                }
                else
                {
                    this.CurrentBal -= this.inv[code].Price;
                    this.inv[code].DecrementItem(code, this.inv);
                    return this.inv[code];
                }
            }
        }

        public void ChangeBack()
        {
            const decimal quarter = 0.25M;
            const decimal dime = 0.10M;
            const decimal nickel = 0.05M;

            Console.WriteLine($"You have been given back ${this.CurrentBal}");
            int quarterAmt = (int)(this.CurrentBal / quarter);
            this.CurrentBal -= quarterAmt * quarter;
            int dimeAmt = (int)(this.CurrentBal / dime);
            this.CurrentBal -= dimeAmt * dime;
            int nickelAmt = (int)(this.CurrentBal / nickel);
            this.CurrentBal -= nickelAmt * nickel;
            Console.WriteLine($"That is {quarterAmt} quarters, {dimeAmt} dimes and {nickelAmt} nickels.");
            Console.ReadLine(); // comment out if running unit test - ChangeReturnTests
        }
    }
}
