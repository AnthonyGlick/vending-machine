using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.VendingMachineFolder
{
    public class VendingMachine
    {
        public decimal TotalSales { get; private set;}
        public decimal CurrentBal { get; private set; }
        
        public VendingMachine()
        {
            this.TotalSales = 0.00M;
            this.CurrentBal = 0.00M;
        }




    }
}
