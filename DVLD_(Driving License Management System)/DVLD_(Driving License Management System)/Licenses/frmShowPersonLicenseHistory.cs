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

namespace DVLD__Driving_License_Management_System_.Licenses
{
    public partial class frmShowPersonLicenseHistory : Form
    {
        private int _PersonID = -1;
        private FormMover _FormMover;
        public frmShowPersonLicenseHistory()
        {
            InitializeComponent();
        }
        public frmShowPersonLicenseHistory(int personId)
        {
            InitializeComponent();
            _PersonID = personId;
        }
        private void frmShowPersonLicenseHistory_Load(object sender, EventArgs e)
        {
            if (_PersonID != -1)
            {
                ctrlPersonCardWithFilter1.LoadPersonInfo(_PersonID);
                ctrlPersonCardWithFilter1.FilterEnabled = false;
                ctrlDriverLicenses1.LoadInfoByPersonID(_PersonID);

            }
            else
            {
                ctrlPersonCardWithFilter1.Enabled = true;
                ctrlPersonCardWithFilter1.FilterFocus();
                ctrlPersonCardWithFilter1.allowChangeTab += ctrlPersonCardWithFilter1_OnPersonSelected;
            }

            _FormMover = new FormMover(this, panelMoveForm);
        }
        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            MessageBox.Show("Heyy");
            _PersonID = obj;
            if (_PersonID == -1)
            {
            }
            else
            {
            }
        }

        private void btnClose1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
