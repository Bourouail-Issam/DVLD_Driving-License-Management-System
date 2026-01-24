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

namespace DVLD__Driving_License_Management_System_.Applications.Release_Detained_License
{
    public partial class frmListDetainedLicenses : Form
    {
        private frmMain _frmMain;
        private DataTable _dtDetainedLicenses;
        public frmListDetainedLicenses(frmMain frmMain  )
        {
            InitializeComponent();
            _frmMain = frmMain;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListDetainedLicenses_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            _dtDetainedLicenses = clsDetainedLicense.GetAllDetainedLicenses();

            dgvDetainedLicenses.DataSource = _dtDetainedLicenses;
            lbRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();
            cbFilterBy.SelectedIndex = 0;

            if (dgvDetainedLicenses.Rows.Count > 0)
            {
                dgvDetainedLicenses.Columns[0].HeaderText = "D.ID";
                dgvDetainedLicenses.Columns[0].Width = 90;

                dgvDetainedLicenses.Columns[1].HeaderText = "L.ID";
                dgvDetainedLicenses.Columns[1].Width = 90;

                dgvDetainedLicenses.Columns[2].HeaderText = "D.Date";
                dgvDetainedLicenses.Columns[2].Width = 160;

                dgvDetainedLicenses.Columns[3].HeaderText = "Is Released";
                dgvDetainedLicenses.Columns[3].Width = 110;

                dgvDetainedLicenses.Columns[4].HeaderText = "Fine Fees";
                dgvDetainedLicenses.Columns[4].Width = 110;

                dgvDetainedLicenses.Columns[5].HeaderText = "Release Date";
                dgvDetainedLicenses.Columns[5].Width = 160;

                dgvDetainedLicenses.Columns[6].HeaderText = "N.No.";
                dgvDetainedLicenses.Columns[6].Width = 90;

                dgvDetainedLicenses.Columns[7].HeaderText = "Full Name";
                dgvDetainedLicenses.Columns[7].Width = 330;

                dgvDetainedLicenses.Columns[8].HeaderText = "Rlease App.ID";
                dgvDetainedLicenses.Columns[8].Width = 150;

            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _dtDetainedLicenses.DefaultView.RowFilter = "";
            lbRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();

            if (cbFilterBy.Text == "Is Released")
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
    }
}
