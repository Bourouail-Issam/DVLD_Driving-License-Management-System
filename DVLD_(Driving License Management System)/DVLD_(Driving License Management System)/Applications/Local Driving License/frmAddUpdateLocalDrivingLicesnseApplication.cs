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

namespace DVLD__Driving_License_Management_System_.Applications.Local_Driving_License
{
    public partial class frmAddUpdateLocalDrivingLicesnseApplication : Form
    {
        public enum enMode {addNew=0, Update=1 };
        private enMode _Mode;

        private int _LocalDrivingLicenseApplicationID = -1;

        FormMover _formMover;
        public frmAddUpdateLocalDrivingLicesnseApplication()
        {
            InitializeComponent();
            _Mode = enMode.addNew;
        }
        public frmAddUpdateLocalDrivingLicesnseApplication(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _Mode = enMode.Update;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddUpdateLocalDrivingLicesnseApplication_Load(object sender, EventArgs e)
        {

            _formMover = new FormMover(this, panelMoveForm);
        }
    }
}
