﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.VendingMachineFolder
{
    public class Drink : VendingMachineItem
    {
        public Drink(string slot, string name, decimal price, string type) : base(slot, name, price, type)
        {
        }

        public override string MakeFoodSound()
        {
            return "Glug Glug, Yum!";
        }
    }
}
