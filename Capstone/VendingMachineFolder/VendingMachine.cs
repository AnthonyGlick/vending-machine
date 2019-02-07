using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone.VendingMachineFolder
{
    public class VendingMachine
    {
        private List<VendingMachineItem> inv = new List<VendingMachineItem>();
        public decimal TotalSales { get; private set;}
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
                    inv.Add(new itemInfo[3](itemInfo[0], itemInfo[1], itemInfo[2], itemInfo[3]))

                }
            }

        }

        public void AddBal(decimal amount)
        {
          this.CurrentBal += amount;
        }



    }
}
