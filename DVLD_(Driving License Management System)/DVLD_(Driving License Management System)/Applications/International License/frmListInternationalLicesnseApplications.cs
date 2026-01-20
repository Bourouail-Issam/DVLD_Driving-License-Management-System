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

namespace DVLD__Driving_License_Management_System_.Applications.International_License
{
    public partial class frmListInternationalLicesnseApplications : Form
    {
        private frmMain _frmMain;
        private DataTable _dtInternationalLicenseApplications;
        public frmListInternationalLicesnseApplications(frmMain frmMain)
        {
            InitializeComponent();
            _frmMain = frmMain;
        }

        private void frmListInternationalLicesnseApplications_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            _dtInternationalLicenseApplications = clsInternationalLicense.GetAllInternationalLicenses();
            cbFilterBy.SelectedIndex = 0;

            dgvInternationalLicenses.DataSource = _dtInternationalLicenseApplications;
            lblInternationalLicensesRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();

            if (dgvInternationalLicenses.Rows.Count > 0)
            {
                dgvInternationalLicenses.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicenses.Columns[0].Width = 160;

                dgvInternationalLicenses.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicenses.Columns[1].Width = 150;

                dgvInternationalLicenses.Columns[2].HeaderText = "Driver ID";
                dgvInternationalLicenses.Columns[2].Width = 130;

                dgvInternationalLicenses.Columns[3].HeaderText = "L.License ID";
                dgvInternationalLicenses.Columns[3].Width = 130;

                dgvInternationalLicenses.Columns[4].HeaderText = "Issue Date";
                dgvInternationalLicenses.Columns[4].Width = 180;

                dgvInternationalLicenses.Columns[5].HeaderText = "Expiration Date";
                dgvInternationalLicenses.Columns[5].Width = 180;

                dgvInternationalLicenses.Columns[6].HeaderText = "Is Active";
                dgvInternationalLicenses.Columns[6].Width = 120;

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            _frmMain.MakeMainPictureVisible();
            this.Close();
        }

        private void btnNewApplication_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
            //refresh
            frmListInternationalLicesnseApplications_Load(null, null);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbIsReleased.Visible = true;
                cbIsReleased.Focus();
                cbIsReleased.SelectedIndex = 0;
            }
            else
            {
                cbIsReleased.Visible = false;

                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow numbers only becasue all fiters are numbers.
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void _FilterDgvPeople(string columnName, string txtFilterValue)
        {

            if (!String.IsNullOrWhiteSpace(txtFilterValue))
            {
                _dtInternationalLicenseApplications.DefaultView.RowFilter = $"CONVERT({columnName}, 'System.String') LIKE '{txtFilterValue}%'";
                dgvInternationalLicenses.DataSource = _dtInternationalLicenseApplications;
            }
            else
            {
                _dtInternationalLicenseApplications.DefaultView.RowFilter = "";
            }
            lblInternationalLicensesRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();
        }
        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "International License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;
                case "Application ID":
                    {
                        FilterColumn = "ApplicationID";
                        break;
                    }
                    ;

                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Local License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;

                case "Is Active":
                    FilterColumn = "IsActive";
                    break;


                default:
                    FilterColumn = "None";
                    break;
            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtInternationalLicenseApplications.DefaultView.RowFilter = "";
                lblInternationalLicensesRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();
                return;
            }
            _FilterDgvPeople(FilterColumn, txtFilterValue.Text);
        }
    }
}
