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

namespace DVLD__Driving_License_Management_System_.Tests.Controls
{
    public partial class ctrlSecheduledTest : UserControl
    {
        private clsTestType.enTestType _TestTypeID;
        private int _TestID = -1;
        public int TestID
        {
            get { return _TestID; }
        }

        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private int _LocalDrivingLicenseApplicationID = -1;

        private clsTestAppointment _TestAppointment;
        private int _TestAppointmentID = -1;
        public int TestAppointmentID
        {
            get  { return _TestAppointmentID;}
        }

        public ctrlSecheduledTest()
        {
            InitializeComponent();
        }
        public void LoadInfo(int TestAppointmentID)
        {
            _TestAppointmentID = TestAppointmentID;

            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);
            //incase we did not find any appointment .
            if (_TestAppointment == null)
            {
                MessageBox.Show(
                    "Error: No  Appointment ID = " + _TestAppointmentID.ToString(),
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );

                _TestAppointmentID = -1;
                return;
            }

            _TestID = _TestAppointment.TestID;
            _LocalDrivingLicenseApplicationID = _TestAppointment.LocalDrivingLicenseApplicationID;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show(
                    "Error: No Local Driving License Application with ID = " + _LocalDrivingLicenseApplicationID.ToString(),
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                return;
            }
            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplication.PersonFullName;

            //this will show the trials for this test before 
            lblTrial.Text = _LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestTypeID).ToString();

            lblDate.Text = clsFormat.DateToShort(_TestAppointment.AppointmentDate);
            lblFees.Text = _TestAppointment.PaidFees.ToString();
            lblTestID.Text = (_TestAppointment.TestID == -1) ? "Not Taken Yet" : _TestAppointment.TestID.ToString();

        }
    }
}
