using SMSExchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SMSServerTest
{
    internal static class Program
    {

        internal static SMSClient SmsClient;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SmsClient = new SMSClient("Just now");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
