using DVLD__Driving_License_Management_System_.Licenses;
using DVLD__Driving_License_Management_System_.Licenses.Local_Licenses;
using DVLD__Driving_License_Management_System_.Tests;
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

namespace DVLD__Driving_License_Management_System_.Applications.Local_Driving_License
{
    public partial class frmListLocalDrivingLicesnseApplications : Form
    {
        private frmMain _frmMain;
        private DataTable _dtLocalDrivingLicenseApplication;

        public frmListLocalDrivingLicesnseApplications(frmMain frmMain)
        {
            InitializeComponent();
            _frmMain = frmMain;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            _frmMain.MakeMainPictureVisible();
            this.Close();
        }

        private void _frmListLocalDrivingLicesnseApplications_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            _dtLocalDrivingLicenseApplication = 
                clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();

            dgvLocalDrivingLicenseApplications.DataSource = _dtLocalDrivingLicenseApplication;
            lbRecords.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();

            cbFilterBy.SelectedIndex = 0;

            if (dgvLocalDrivingLicenseApplications.Rows.Count>0)
            {
                dgvLocalDrivingLicenseApplications.Columns[0].HeaderText = "L.D.L.AppID";
                dgvLocalDrivingLicenseApplications.Columns[0].Width = 100;

                dgvLocalDrivingLicenseApplications.Columns[1].HeaderText = "Driving Class";
                dgvLocalDrivingLicenseApplications.Columns[1].Width = 300;

                dgvLocalDrivingLicenseApplications.Columns[2].HeaderText = "National No.";
                dgvLocalDrivingLicenseApplications.Columns[2].Width = 150;

                dgvLocalDrivingLicenseApplications.Columns[3].HeaderText = "Full Name";
                dgvLocalDrivingLicenseApplications.Columns[3].Width = 350;

                dgvLocalDrivingLicenseApplications.Columns[4].HeaderText = "Application Date";
                dgvLocalDrivingLicenseApplications.Columns[4].Width = 170;

                dgvLocalDrivingLicenseApplications.Columns[5].HeaderText = "Passed Tests";
                dgvLocalDrivingLicenseApplications.Columns[5].Width = 100;

                dgvLocalDrivingLicenseApplications.Columns[6].HeaderText = "Status";
                dgvLocalDrivingLicenseApplications.Columns[6].Width = 100;
            }
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicesnseApplication frm =
                new frmAddUpdateLocalDrivingLicesnseApplication();
            frm.ShowDialog();
            _frmListLocalDrivingLicesnseApplications_Load(null, null);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
            else
            {
                _dtLocalDrivingLicenseApplication.DefaultView.RowFilter = "";
                lbRecords.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
            }
        }

        private void _FilterDgvLocalDrivingLicenseApplication(string columnName, string txtFilterValue)
        {

            if (!String.IsNullOrWhiteSpace(txtFilterValue))
            {
                _dtLocalDrivingLicenseApplication.DefaultView.RowFilter = $"CONVERT({columnName}, 'System.String') LIKE '{txtFilterValue}%'";
                dgvLocalDrivingLicenseApplications.DataSource = _dtLocalDrivingLicenseApplication;
            }
            else
            {
                _dtLocalDrivingLicenseApplication.DefaultView.RowFilter = "";
            }
            lbRecords.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {

                case "L.D.L.AppID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Status":
                    FilterColumn = "Status";
                    break;


                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtLocalDrivingLicenseApplication.DefaultView.RowFilter = "";
                lbRecords.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
                return;
            }

            _FilterDgvLocalDrivingLicenseApplication(FilterColumn, txtFilterValue.Text);
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id is selected.
            if (cbFilterBy.Text == "L.D.L.AppID")
            {
                e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
            }
        }

        // Context menu strip events
        private void tsmEditTool_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            frmAddUpdateLocalDrivingLicesnseApplication frm =
                         new frmAddUpdateLocalDrivingLicesnseApplication(LocalDrivingLicenseApplicationID);
            frm.ShowDialog();

            _frmListLocalDrivingLicesnseApplications_Load(null, null);
        }

        private void tsmDeleteApplication_Click(object sender, EventArgs e)
        {
            if (
                MessageBox.Show(
                "Are you sure do want to delete this application?", 
                "Confirm", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question) == DialogResult.No
                )
                   return;


            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =
                clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication != null)
            {
                if (LocalDrivingLicenseApplication.delete())
                {
                    MessageBox.Show(
                        "Application Deleted Successfully.", 
                        "Deleted", 
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                    //refresh the form again.
                    _frmListLocalDrivingLicesnseApplications_Load(null, null);
                }
                else
                {
                    MessageBox.Show(
                        "Could not delete applicatoin, other data depends on it.",
                        "Error",
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Error
                        );
                }
            }

        }

        private void tsmShowDetails_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID =
                 (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            frmLocalDrivingLicenseApplicationInfo frm =
                new frmLocalDrivingLicenseApplicationInfo(LocalDrivingLicenseApplicationID);
            frm.ShowDialog();
            _frmListLocalDrivingLicesnseApplications_Load(null, null);
        }

        private void tsmCancelApplicaiton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Are you sure do want to cancel this application?", 
                "Confirm",
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question) == DialogResult.No
                )
                return;

            int LocalDrivingLicenseApplicationID =
                (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =
                clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);


            if (LocalDrivingLicenseApplication != null)
            {
                if (LocalDrivingLicenseApplication.Cancel())
                {
                    MessageBox.Show(
                        "Application Cancelled Successfully.",
                        "Cancelled",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                    //refresh the form again.
                    _frmListLocalDrivingLicesnseApplications_Load(null, null);
                }
                else
                    MessageBox.Show(
                        "Could not cancel applicatoin.", 
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                        );
            }
        }

        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =
                    clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID
                                                    (LocalDrivingLicenseApplicationID);

            int TotalPassedTests = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[5].Value;

            bool LicenseExists = LocalDrivingLicenseApplication.IsLicenseIssued();

            //Enabled only if person passed all tests and Does not have license. 
            tsmIssueDrivingLicenseFirstTime.Enabled = (TotalPassedTests == 3) && !LicenseExists && LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New;

            tsmShowLicense.Enabled = LicenseExists;

            tsmEditTool.Enabled = !LicenseExists && 
                (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);

            tsmScheduleTestsMenue.Enabled = !LicenseExists;

            //Enable/Disable Cancel Menue Item
            //We only canel the applications with status=new.
            tsmCancelApplicaiton.Enabled = 
                LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New;

            //Enable/Disable Delete Menue Item
            //We only allow delete incase the application status is new not complete or Cancelled.
            tsmDeleteApplication.Enabled =
                (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);

            //Enable Disable Schedule menue and it's sub menue
            bool PassedVisionTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.VisionTest); ;
            bool PassedWrittenTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.WrittenTest);
            bool PassedStreetTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.StreetTest);

            tsmScheduleTestsMenue.Enabled =
                (!PassedVisionTest || !PassedWrittenTest || !PassedStreetTest) &&
                (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);

            if (tsmScheduleTestsMenue.Enabled)
            {
                //To Allow Schdule vision test, Person must not passed the same test before.
                tsmScheduleVisionTest.Enabled = !PassedVisionTest;

                //To Allow Schdule written test, Person must pass the vision test and must not passed the same test before.
                tsmScheduleWrittenTest.Enabled = PassedVisionTest && !PassedWrittenTest;

                //To Allow Schdule steet test, Person must pass the vision * written tests, and must not passed the same test before.
                tsmScheduleStreetTest.Enabled = PassedVisionTest && PassedWrittenTest && !PassedStreetTest;
            }
        }

        private void tsmShowLicense_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = 
                (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            int LicenseID = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(
               LocalDrivingLicenseApplicationID).GetActiveLicenseID();

            if (LicenseID != -1)
            {
                frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show(
                    "No License Found!", 
                    "No License",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
        }

        private void tsmShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = 
                (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = 
                clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(
                    LocalDrivingLicenseApplicationID
                    );

            frmShowPersonLicenseHistory frm = 
                new frmShowPersonLicenseHistory(localDrivingLicenseApplication.ApplicantPersonID);
            frm.ShowDialog();
        }

        private void _ScheduleTest(clsTestType.enTestType TestType)
        {

            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            frmListTestAppointments frm = new frmListTestAppointments(LocalDrivingLicenseApplicationID, TestType);
            frm.ShowDialog();
            //refresh
            _frmListLocalDrivingLicesnseApplications_Load(null, null);

        }

        private void tsmScheduleVisionTest_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.VisionTest);
        }

        private void tsmScheduleWrittenTest_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.WrittenTest);
        }

        private void tsmScheduleStreetTest_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.StreetTest);
        }

        private void tsmIssueDrivingLicenseFirstTime_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            frmIssueDriverLicenseFirstTime frm = new frmIssueDriverLicenseFirstTime(LocalDrivingLicenseApplicationID);
            frm.ShowDialog();
            //refresh
            _frmListLocalDrivingLicesnseApplications_Load(null, null);
        }
    }
}
