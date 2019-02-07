using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.VendingMachineFolder
{
    public abstract class VendingMachineItem
    {
     public string Slot { get; }
     public string Name { get; }
     public decimal Price { get; }
     public string Type { get; }
     public int RemainingInventory { get; private set; }

   
    }
}
