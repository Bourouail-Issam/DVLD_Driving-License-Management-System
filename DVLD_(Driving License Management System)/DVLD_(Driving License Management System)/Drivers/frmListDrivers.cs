using DVLD__Driving_License_Management_System_.Licenses;
using DVLD__Driving_License_Management_System_.People;
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

namespace DVLD__Driving_License_Management_System_.Drivers
{
    public partial class frmListDrivers : Form
    {
        private frmMain _frmMain;
        private DataTable _dtAllDrivers;
        public frmListDrivers(frmMain frmMain)
        {
            InitializeComponent();
            _frmMain = frmMain;
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            _frmMain.MakeMainPictureVisible();
            this.Close();
        }
        private void frmListDrivers_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;

            cbFilter.SelectedIndex = 0;
            _dtAllDrivers = clsDriver.GetAllDrivers();

            dgvDrivers.DataSource = _dtAllDrivers;
            lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();

            if (dgvDrivers.Rows.Count > 0)
            {
                dgvDrivers.Columns[0].HeaderText = "Driver ID";
                dgvDrivers.Columns[0].Width = 120;

                dgvDrivers.Columns[1].HeaderText = "Person ID";
                dgvDrivers.Columns[1].Width = 120;

                dgvDrivers.Columns[2].HeaderText = "National No.";
                dgvDrivers.Columns[2].Width = 140;

                dgvDrivers.Columns[3].HeaderText = "Full Name";
                dgvDrivers.Columns[3].Width = 320;

                dgvDrivers.Columns[4].HeaderText = "Date";
                dgvDrivers.Columns[4].Width = 170;

                dgvDrivers.Columns[5].HeaderText = "Active Licenses";
                dgvDrivers.Columns[5].Width = 150;
            }
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilter.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
            else
            {
                if (_dtAllDrivers != null)
                {
                    _dtAllDrivers.DefaultView.RowFilter = "";
                    lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();
                }
            }
        }


        private void _FilterDgvPeople(string columnName, string txtFilterValue)
        {

            if (!String.IsNullOrWhiteSpace(txtFilterValue))
            {
                _dtAllDrivers.DefaultView.RowFilter = $"CONVERT({columnName}, 'System.String') LIKE '{txtFilterValue}%'";
                dgvDrivers.DataSource = _dtAllDrivers;
            }
            else
            {
                _dtAllDrivers.DefaultView.RowFilter = "";
            }
            lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            //Map Selected Filter to real Column name 33
            switch (cbFilter.Text)
            {
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }
            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllDrivers.DefaultView.RowFilter = "";
                lblRecordsCount.Text =  dgvDrivers.Rows.Count.ToString();
                return;
            }
            _FilterDgvPeople(FilterColumn, txtFilterValue.Text);
        }

        private void tsmShowDetails_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvDrivers.CurrentRow.Cells[1].Value;
            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
            //refresh
            frmListDrivers_Load(null, null);
        }

        private void tsmIssueInternationalLicense_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented yet.");
        }

        private void tsmShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvDrivers.CurrentRow.Cells[1].Value;
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        }
    }
}
