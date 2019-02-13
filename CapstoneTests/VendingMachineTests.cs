using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.VendingMachineFolder;
namespace CapstoneTests
{
    /// <summary>
    /// Tests the methods within the Vending Machine class
    /// </summary>
    [TestClass]
    public class VendingMachineTests
    {
        // Test requires current bal to be normal set

        /// <summary>
        /// Tests the ChangeBack() method
        /// </summary>
        /// <param name="balance">The balance input for the Vending Machine</param>
        /// <param name="expectedBalance">The expected balance on output.</param>
        [DataTestMethod]
        [DataRow("10.00", "0.00")]
        [DataRow("10.30", "0.00")]
        [DataRow("11.90", "0.00")]
        public void ChangeReturnTests(string balance, string expectedBalance)
        {
            decimal decBalance = decimal.Parse(balance);
            decimal decExpected = decimal.Parse(expectedBalance);
            VendingMachine machine = new VendingMachine();

            //machine.CurrentBal = decBalance; //TODO Fix Later
            machine.ChangeBack(); // comment out the readline at the end of ChangeBack if running this unit test

            Assert.AreEqual(decExpected, machine.CurrentBal);
        }

        /// <summary>
        /// Tests the CalcBal() method
        /// </summary>
        /// <param name="balance">The balance input for the Vending Machine </param>
        /// <param name="code">The code of the item being tested for.</param>
        /// <param name="expectedOutput">The expected balance on output.</param>
        [DataTestMethod]
        [DataRow("6", "A1", "2.95")]
        [DataRow("7", "B1", "5.20")]
        [DataRow("8.25", "B3", "6.75")]
        public void CalcBalTests(string balance, string code, string expectedOutput)
        {
            decimal decBalance = decimal.Parse(balance);
            decimal decExpected = decimal.Parse(expectedOutput);

            VendingMachine machine = new VendingMachine();
            //machine.CurrentBal = decBalance; //TODO Fix later
            machine.PurchaseItem(code);

            Assert.AreEqual(decExpected, machine.CurrentBal);
        }
    }
}
