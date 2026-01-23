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

namespace DVLD__Driving_License_Management_System_.Licenses.Detain_License
{
    public partial class frmDetainLicenseApplication : Form
    {
        private FormMover _formMover;
        private int _NewLicenseID = -1;
        public frmDetainLicenseApplication()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDetainLicenseApplication_Load(object sender, EventArgs e)
        {
            _formMover = new FormMover(this, panelMoveForm);
        }
    }
}
