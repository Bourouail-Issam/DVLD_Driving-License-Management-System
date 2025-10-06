using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD__Driving_License_Management_System_
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }


        // Navigation buttons in Main Dashbord

        private void btnApplications_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            btnApplications.BackColor = Color.FromArgb(63, 93, 127);

            rjDropdownMenuApplications.Show(btnApplications, btnApplications.Width, 0);

        }

        private void btnPeople_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            btnPeople.BackColor = Color.FromArgb(63, 93, 127);
        }

        private void btnDrivers_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            btnDrivers.BackColor = Color.FromArgb(63, 93, 127);
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            btnUsers.BackColor = Color.FromArgb(63, 93, 127);
        }

        private void btnAccountSetting_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            btnAccountSetting.BackColor = Color.FromArgb(63, 93, 127);

            rjDropdownMenuAccountSettings.Show(btnAccountSetting, btnAccountSetting.Width, 0);

        }



        // Method to reset the background color of all buttons to the default color

        private void ResetButtonColors()
        {
            Color defaultColor = Color.FromArgb(5, 34, 67); 

            btnApplications.BackColor = defaultColor;
            btnPeople.BackColor = defaultColor;
            btnDrivers.BackColor = defaultColor;
            btnUsers.BackColor = defaultColor;
            btnAccountSetting.BackColor = defaultColor;
        }

      
    }
}
