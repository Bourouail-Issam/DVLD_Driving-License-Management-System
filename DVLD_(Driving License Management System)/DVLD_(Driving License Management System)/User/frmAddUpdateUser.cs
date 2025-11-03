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

namespace DVLD__Driving_License_Management_System_.User
{
    public partial class frmAddUpdateUser : Form
    {
        FormMover _formMover;
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        private int _UserID = -1;
        clsUser _User;

        private bool _allowChange = false;
        public frmAddUpdateUser()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddUpdateUser(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            _Mode = enMode.Update;           
        }

        private void _ResetDefualtValues()
        {
            //this will initialize the reset the defaule values

            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New User";
                this.Text = "Add New User";
                _User = new clsUser();
                tpLoginInfo.Enabled = true;
                ctrlPersonCardWithFilter1.FilterFocus();

                // My
                ctrlPersonCardWithFilter1.FilterEnabled = true;
                btnSave.Enabled = false;
            }
            else
            {
                lblTitle.Text = "Update User";
                this.Text = "Update User";
                tpLoginInfo.Enabled = true;
                btnSave.Enabled = true;

            }

            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            chkIsActive.Checked = true;
        }

        private void _LoadData()
        {
            _User = clsUser.FindByUserID(_UserID);
            ctrlPersonCardWithFilter1.FilterEnabled = false;

            if (_User == null)
            {
                MessageBox.Show(
                    "No User with ID = " + _User, "User Not Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                    );
                this.Close();

                return;
            }

            //the following code will not be executed if the person was not found
            lblUserID.Text = _User.UserID.ToString();
            txtUserName.Text = _User.UserName;
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;
            chkIsActive.Checked = _User.IsActive;
            ctrlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);
        }

        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            _formMover = new FormMover(this, panelMoveForm);

            _ResetDefualtValues();

            if (_Mode == enMode.Update)
                _LoadData();
            else
                ctrlPersonCardWithFilter1.allowChangeTab += ChangePermissionTabSelection;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPersonInfoNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpLoginInfo.Enabled = true;
                _allowChange = true;
                tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
            }

            //incase of add new mode.
            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {
                if (clsUser.isUserExistForPersonID(ctrlPersonCardWithFilter1.PersonID))
                {
                    MessageBox.Show(
                        "Selected Person already has a user, choose another one.",
                        "Select another Person",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                        );
                    ctrlPersonCardWithFilter1.FilterFocus();

                }
                else
                {
                    btnSave.Enabled = true;
                    tpLoginInfo.Enabled = true;
                    _allowChange = true;
                    tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                }
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
        private void ChangePermissionTabSelection()
        {
            _allowChange = false;
        }
        private void tcUserInfo_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!_allowChange)
            {
                e.Cancel = true;
                MessageBox.Show(
                    "Yoou need to enter in button Next to go to Page Login Info",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
            }       
        }
    }
}
