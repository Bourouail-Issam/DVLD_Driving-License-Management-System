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
using static DVLD_BuisnessDVLD_Buisness.clsTestType;

namespace DVLD__Driving_License_Management_System_.Tests.Test_Types
{
    public partial class frmEditTestType : Form
    {
        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        private clsTestType _TestType;
        public frmEditTestType(clsTestType.enTestType testTypeID)
        {
            InitializeComponent();
            _TestTypeID = testTypeID;
        }

        private void frmEditTestType_Load(object sender, EventArgs e)
        {
            _TestType = clsTestType.Find(_TestTypeID);

            if (_TestType != null)
            {
                lblTestTypeID.Text = ((int)_TestTypeID).ToString();
                txtTitle.Text = _TestType.Title;
                txtDescription.Text = _TestType.Description;
                txtFees.Text = _TestType.Fees.ToString();
            }
            else
            {
                MessageBox.Show(
                    "Could not find Test Type with id = " + _TestTypeID.ToString(),
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error 
                    );     
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
