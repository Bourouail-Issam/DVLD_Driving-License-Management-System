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

        private void frmListLocalDrivingLicesnseApplications_Load(object sender, EventArgs e)
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
    }
}
