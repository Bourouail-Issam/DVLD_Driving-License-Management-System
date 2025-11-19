using DVLD__Driving_License_Management_System_.Global_Classes;
using DVLD__Driving_License_Management_System_.People.Controls;
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

namespace DVLD__Driving_License_Management_System_.Applications.Local_Driving_License
{
    public partial class frmAddUpdateLocalDrivingLicesnseApplication : Form
    {
        public enum enMode {addNew=0, Update=1 };
        private enMode _Mode;

        private bool _allowChange = false;


        private int _LocalDrivingLicenseApplicationID = -1;
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
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

        private void _FillLicenseClassesInComoboBox()
        {
            DataTable LicenseClassList = clsLicenseClass.GetAllLicenseClasses();
            cbLicenseClass.Items.Clear();

            foreach (DataRow row in LicenseClassList.Rows)
            {
                cbLicenseClass.Items.Add(row["ClassName"]);
            }
        }

        void MakeBtnSaveDisable()
        {
            btnSave.Enabled = false;
            btnSave.Cursor = Cursors.Arrow;
            btnSave.BackColor = Color.FromArgb(89, 146, 202);
            btnSave.ForeColor = Color.Black;
        }

        void MakeBtnSaveEnable()
        {
            btnSave.Enabled = true;
            btnSave.Cursor = Cursors.Hand;
            btnSave.BackColor = Color.RoyalBlue;
            btnSave.ForeColor = Color.White;
        }

        private void _ResetDefualtValues()
        {
            //this will initialize the reset the defaule values
            _FillLicenseClassesInComoboBox();

            if (_Mode == enMode.addNew)
            {
                lblTitle.Text = "New Local Driving License Application";
                this.Text = "New Local Driving License Application";
                _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();

                tpApplicationInfo.Enabled = false;
                cbLicenseClass.SelectedIndex = 2;
                lblApplicationDate.Text = DateTime.Now.ToString("MMM/dd/yyyy");
                lblFees.Text =clsApplicationType.Find((int)clsApplication.enApplicationType.NewDrivingLicense).Fees.ToString() + " $";
                lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;

                MakeBtnSaveDisable();
                ctrlPersonCardWithFilter1.FilterFocus();
            }
            else
            {
                lblTitle.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";

                tpApplicationInfo.Enabled = true;
                MakeBtnSaveEnable();
            }

            lblLocalDrivingLicebseApplicationID.Text = "[????]";
        }

        private void _LoadData()
        {
            ctrlPersonCardWithFilter1.FilterEnabled = false;
            _LocalDrivingLicenseApplication =
                clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);


            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show(
                    "No Application with ID = " + _LocalDrivingLicenseApplicationID, 
                    "Application Not Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                    );
                this.Close();

                return;
            }
            ctrlPersonCardWithFilter1.LoadPersonInfo(_LocalDrivingLicenseApplication.ApplicantPersonID);
            lblLocalDrivingLicebseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text = _LocalDrivingLicenseApplication.ApplicationDate.ToString("MMM/dd/yyyy");
            cbLicenseClass.SelectedIndex =
                cbLicenseClass.FindString(clsLicenseClass.Find(_LocalDrivingLicenseApplication.LicenseClassID).ClassName);
            lblFees.Text = _LocalDrivingLicenseApplication.PaidFees.ToString()+" $";
            lblCreatedByUser.Text = clsUser.FindByUserID(_LocalDrivingLicenseApplication.CreatedByUserID).UserName;
        }

        private void DisablePermissionTabSelection()
        {
            _allowChange = false;
            _Mode = enMode.addNew;
            _ResetDefualtValues();
        }

        private void frmAddUpdateLocalDrivingLicesnseApplication_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if (_Mode == enMode.Update)
                _LoadData();
            else
                ctrlPersonCardWithFilter1.allowChangeTab += DisablePermissionTabSelection;

            _formMover = new FormMover(this, panelMoveForm);
        }

        private void tcApplicationInfo_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!_allowChange)
            {
                e.Cancel = true;
                MessageBox.Show(
                    "Use the 'Next' button to access the Login Info page",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
            }
        }

        private void btnApplicationInfoNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                MakeBtnSaveEnable();
                tpApplicationInfo.Enabled = true;
                _allowChange = true;
                tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpApplicationInfo"];
                return;
            }

            //incase of add new mode.
            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {
                MakeBtnSaveEnable();
                tpApplicationInfo.Enabled = true;
                _allowChange = true;
                tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpApplicationInfo"];
            }
            else
            {
                MessageBox.Show(
                    "Please Select a Person",
                    "Select a Person",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                ctrlPersonCardWithFilter1.FilterFocus();
            }
        }
    }
}
