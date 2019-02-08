using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.VendingMachineFolder;

namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTests
    {
        //Test requires current bal to be normal set
        [DataTestMethod]
        [DataRow("10.00" , "0.00")]
        [DataRow("10.30", "0.00")]
        [DataRow("11.90", "0.00")]
        public void ChangeReturnTests(string balance, string expectedBalance)
        {
            decimal decBalance = decimal.Parse(balance);
            decimal decExpected = decimal.Parse(expectedBalance);
            VendingMachine machine = new VendingMachine();

            machine.CurrentBal = decBalance;
            machine.ChangeBack(); // comment out the readline at the end of ChangeBack if running this unit test

            Assert.AreEqual(decExpected, machine.CurrentBal);
        }

        [DataTestMethod]
        [DataRow("6", "A1", "2.95")]
        [DataRow("7", "B1", "5.20")]
        [DataRow("8.25", "B3", "6.75")]
        public void CalcBalTests(string balance, string code, string expectedOutput)
        {
            decimal decBalance = decimal.Parse(balance);
            decimal decExpected = decimal.Parse(expectedOutput);

            VendingMachine machine = new VendingMachine();
            machine.CurrentBal = decBalance;
            machine.CalcBal(code);

            Assert.AreEqual(decExpected, machine.CurrentBal);
        }
    }
}
