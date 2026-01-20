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
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
