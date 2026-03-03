using DVLD__Driving_License_Management_System_.Login;
using DVLD_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD__Driving_License_Management_System_
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (!clsLogEvent.CreateEventLog()) 
            {
                MessageBox.Show(
                    "Failed to initialize Event Log!\nApplication will exit.",
                    "Critical Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }
            Application.Run(new frmLogin());
        }
    }
}
