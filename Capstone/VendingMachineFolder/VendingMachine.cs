using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace Capstone.VendingMachineFolder
{
    public class VendingMachine
    {
        private Dictionary<string, VendingMachineItem> inv = new Dictionary<string, VendingMachineItem>();

        public decimal TotalSales { get; private set; }

        public decimal CurrentBal { get; private set; }
        public string[] Slots
        {
            get
            {
                return inv.Keys.ToArray();
            }
        }

        public VendingMachine()
        {
            this.TotalSales = 0.00M;
            this.CurrentBal = 0.00M;

            VendingMachineStocker stocker = new VendingMachineStocker();
            this.inv = stocker.GetInventory();
        }

        public void AddBal(decimal amount)
        {
            this.CurrentBal += amount;
        }

        public VendingMachineItem PurchaseItem(string code)
        {
            if (!this.inv.ContainsKey(code))
            {
                throw new Exception("Invalid code, please try again.");
            }
            else if (this.CurrentBal < this.inv[code].Price)
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
                this.inv[code].DecrementItem();
                return this.inv[code];
            }
        }

        public VendingMachineItem GetItemAtSlot(string slot)
        {
            return inv[slot];
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
