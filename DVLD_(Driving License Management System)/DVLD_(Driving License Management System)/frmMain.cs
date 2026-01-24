using DVLD__Driving_License_Management_System_.Applications.Application_Types;
using DVLD__Driving_License_Management_System_.Applications.International_License;
using DVLD__Driving_License_Management_System_.Applications.Local_Driving_License;
using DVLD__Driving_License_Management_System_.Applications.Release_Detained_License;
using DVLD__Driving_License_Management_System_.Applications.Renew_Local_License;
using DVLD__Driving_License_Management_System_.Applications.Replace_Lost_Or_Damaged_License;
using DVLD__Driving_License_Management_System_.Drivers;
using DVLD__Driving_License_Management_System_.Global_Classes;
using DVLD__Driving_License_Management_System_.Licenses.Detain_License;
using DVLD__Driving_License_Management_System_.People;
using DVLD__Driving_License_Management_System_.Tests.Test_Types;
using DVLD__Driving_License_Management_System_.User;
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
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }


        // Navigation buttons in Main Dashbord

        private void btnApplications_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            btnApplications.BackColor = Color.FromArgb(63, 93, 127);
            rjDropdownMenuApplications.Show(btnApplications, btnApplications.Width, 0);
            MakeMainPictureVisible();

        }

        private void btnPeople_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            btnPeople.BackColor = Color.FromArgb(63, 93, 127);
            panelMainForm.Visible = false;

            frmListPeople frm = new frmListPeople(this);
            frm.MdiParent = this;
            frm.Show();
        }

        private void btnDrivers_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            btnDrivers.BackColor = Color.FromArgb(63, 93, 127);
            btnPeople.BackColor = Color.FromArgb(63, 93, 127);
            panelMainForm.Visible = false;

            frmListDrivers frm = new frmListDrivers(this);  
            frm.MdiParent = this;                       
            frm.Show();
        }
        
        private void btnUsers_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            btnUsers.BackColor = Color.FromArgb(63, 93, 127);
            panelMainForm.Visible = false;

            frmListUsers frm = new frmListUsers(this);
            frm.MdiParent = this;
            frm.Show();

        }

        private void btnAccountSetting_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            btnAccountSetting.BackColor = Color.FromArgb(63, 93, 127);
            rjDropdownMenuAccountSettings.Show(btnAccountSetting, btnAccountSetting.Width, 0);
            MakeMainPictureVisible();
        }


        // Method to reset the background color of all buttons to the default color
        public void ResetButtonColors()
        {
            Color defaultColor = Color.FromArgb(5, 34, 67); 

            btnApplications.BackColor = defaultColor;
            btnPeople.BackColor = defaultColor;
            btnDrivers.BackColor = defaultColor;
            btnUsers.BackColor = defaultColor;
            btnAccountSetting.BackColor = defaultColor;
        }


        // Make PictureBox Front
        public void MakeMainPictureVisible()
        {
            panelMainForm.Visible = true;
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo(clsGlobal.CurrentUser.PersonID);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }


        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            btnApplications.BackColor = Color.FromArgb(63, 93, 127);
            panelMainForm.Visible = false;

            frmListApplicationTypes frm = new frmListApplicationTypes(this);
            frm.MdiParent = this;
            frm.Show();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            btnApplications.BackColor = Color.FromArgb(63, 93, 127);
            panelMainForm.Visible = false;

            frmListTestTypes frm = new frmListTestTypes(this);
            frm.MdiParent = this;
            frm.Show();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {      

            frmAddUpdateLocalDrivingLicesnseApplication frm =
                new frmAddUpdateLocalDrivingLicesnseApplication();
            
            frm.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            btnApplications.BackColor = Color.FromArgb(63, 93, 127);
            panelMainForm.Visible = false;

            frmListLocalDrivingLicesnseApplications frm =
                new frmListLocalDrivingLicesnseApplications(this);
            frm.MdiParent = this;
            frm.Show();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
        }

        private void internationalLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            btnApplications.BackColor = Color.FromArgb(63, 93, 127);
            panelMainForm.Visible = false;

            frmListInternationalLicesnseApplications frm = new frmListInternationalLicesnseApplications(this);
            frm.MdiParent = this;
            frm.Show();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLocalDrivingLicenseApplication frm = new frmRenewLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void replacementForLostDamagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplaceLostOrDamagedLicenseApplication frm = new frmReplaceLostOrDamagedLicenseApplication();
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();
            frm.ShowDialog();
        }

        private void manageDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ResetButtonColors();
            btnApplications.BackColor = Color.FromArgb(63, 93, 127);
            panelMainForm.Visible = false;

            frmListDetainedLicenses frm = new frmListDetainedLicenses(this);
            frm.MdiParent = this;
            frm.Show();
        }

        private void releaseLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            btnApplications.BackColor = Color.FromArgb(63, 93, 127);
            panelMainForm.Visible = false;

            frmListLocalDrivingLicesnseApplications frm =
                new frmListLocalDrivingLicesnseApplications(this);
            frm.MdiParent = this;
            frm.Show();
        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
        }
    }
}
