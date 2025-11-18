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


        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicesnseApplication frm =
                new frmAddUpdateLocalDrivingLicesnseApplication();
            frm.ShowDialog();
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

            frmListLocalDrivingLicesnseApplications_Load(null, null);
        }
    }
}
