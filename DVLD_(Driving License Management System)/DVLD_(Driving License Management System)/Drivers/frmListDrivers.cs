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
    }
}
