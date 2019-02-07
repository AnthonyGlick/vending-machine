using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.VendingMachineFolder
{
    public class Chip : VendingMachineItem
    {
        public Chip(string slot, string name, decimal price, string type) : base(slot, name, price, type)
        {

        }

        protected override string MakeFoodSound()
        {
            return "Crunch Crunch, Yum!";
        }
    }
}
