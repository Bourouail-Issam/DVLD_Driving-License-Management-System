using DVLD__Driving_License_Management_System_.Global_Classes;
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
    public partial class frmShowLicenseInfo : Form
    {
        private FormMover _formMover;
        private int _LicenseID;
        public frmShowLicenseInfo(int licenseID)
        {
            InitializeComponent();
            _LicenseID = licenseID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfo1.LoadInfo(_LicenseID);
            _formMover = new FormMover(this, panelMoveForm);
        }
    }
}
