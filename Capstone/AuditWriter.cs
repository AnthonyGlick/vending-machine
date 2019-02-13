using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone
{
    public class AuditWriter
    {
        /// <summary>
        /// Writes a line item to the audit log.
        /// </summary>
        /// <param name="currentBal"></param>
        /// <param name="action"></param>
        /// <param name="previousBal"></param>
        public void WriteAudit(decimal currentBal, string action, decimal previousBal)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("audit.txt", true))
                {
                    sw.WriteLine($"{DateTime.Now,-15} {action,-20} {previousBal,-5} {currentBal}");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error writing audit.");
            }
        }
    }
}
