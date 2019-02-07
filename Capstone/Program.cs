using System;
using Capstone.CLIs;
using Capstone.VendingMachineFolder;


namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            VendingMachine vm = new VendingMachine();
            MainMenuCLI menu = new MainMenuCLI();
            menu.Run(vm);
            



        }
    }
}
