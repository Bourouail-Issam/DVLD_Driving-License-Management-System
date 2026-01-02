using DVLD__Driving_License_Management_System_.Global_Classes;
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

namespace DVLD__Driving_License_Management_System_.Tests
{
    public partial class frmTakeTest : Form
    {
        private int _AppointmentID;
        private clsTestType.enTestType _TestType;

        private int _TestID = -1;
        private clsTest _Test;
        private FormMover _formMover;
        public frmTakeTest(int AppointmentID, clsTestType.enTestType TestType)
        {
            InitializeComponent();
            _AppointmentID = AppointmentID;
            _TestType = TestType;
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctrlSecheduledTest1.TestTypeID = _TestType;
            ctrlSecheduledTest1.LoadInfo(_AppointmentID);

            btnSave.Enabled = ctrlSecheduledTest1.TestAppointmentID == -1 ? false : true;

            int _TestID = ctrlSecheduledTest1.TestID;
            if (_TestID != -1)
            {
                _Test = clsTest.Find(_TestID);

                if (_Test.TestResult)
                    rbPass.Checked = true;
                else
                    rbFail.Checked = true;
                txtNotes.Text = _Test.Notes;

                lblUserMessage.Visible = true;
                rbFail.Enabled = false;
                rbPass.Enabled = false;
            }

            else
                _Test = new clsTest();
            _formMover = new FormMover(this, panelMoveForm);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();  
        }
    }
}
