using DVLD__Driving_License_Management_System_.Global_Classes;
using DVLD_BuisnessDVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD__Driving_License_Management_System_.Licenses.Local_Licenses
{
    public partial class frmIssueDriverLicenseFirstTime : Form
    {
        private int _LocalDrivingLicenseApplicationID;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private FormMover _formMover; 
        public frmIssueDriverLicenseFirstTime(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmIssueDriverLicenseFirstTime_Load(object sender, EventArgs e)
        {
            txtNotes.Focus();
            _LocalDrivingLicenseApplication =
                clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);


            if (_LocalDrivingLicenseApplication == null)
            {

                MessageBox.Show(
                    "No Applicaiton with ID=" + _LocalDrivingLicenseApplicationID.ToString(), 
                    "Not Allowed",
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error
                    );
                this.Close();
                return;
            }


            if (!_LocalDrivingLicenseApplication.PassedAllTests())
            {
                MessageBox.Show(
                    "Person Should Pass All Tests First.",
                    "Not Allowed", 
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                this.Close();
                return;
            }


            int LicenseID = _LocalDrivingLicenseApplication.GetActiveLicenseID();
            if (LicenseID != -1)
            {
                MessageBox.Show(
                    "Person already has License before with License ID=" + LicenseID.ToString(), 
                    "Not Allowed", 
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                this.Close();
                return;
            }

            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicenseApplicationID);
            _formMover = new FormMover(this, panelMoveForm);
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {

        }
    }
}
    