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

            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New User";
                this.Text = "Add New User";
                _User = new clsUser();
                //tpLoginInfo.Enabled = true;
                ctrlPersonCardWithFilter1.FilterFocus();

                //
                ctrlPersonCardWithFilter1.FilterEnabled = true;
                MakeBtnSaveDisable();

            }
            else
            {
                lblTitle.Text = "Update User";
                this.Text = "Update User";
                tpLoginInfo.Enabled = true;
                MakeBtnSaveEnable();
            }

            lblUserID.Text = "[????]";
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
                ctrlPersonCardWithFilter1.allowChangeTab += DisablePermissionTabSelection;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPersonInfoNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                MakeBtnSaveEnable();
                tpLoginInfo.Enabled = true;
                _allowChange = true;
                tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                return;
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
                    MakeBtnSaveEnable();
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

        private void DisablePermissionTabSelection()
        {
            _allowChange = false;
            _Mode = enMode.AddNew;
            _ResetDefualtValues();
        }

        private void tcUserInfo_Selecting(object sender, TabControlCancelEventArgs e)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show(
                    "Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error",
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error
                    );
                return;
            }


            _User.PersonID = ctrlPersonCardWithFilter1.PersonID;
            _User.UserName = txtUserName.Text.Trim();
            _User.Password = txtPassword.Text.Trim();
            _User.IsActive = chkIsActive.Checked;

            if (_User.Save())
            {
                lblUserID.Text = _User.UserID.ToString();
                //change form mode to update.
                _Mode = enMode.Update;
                lblTitle.Text = "Update User";
                this.Text = "Update User";

                MessageBox.Show(
                    "Data Saved Successfully.",
                    "Saved", MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );
            }
            else
                MessageBox.Show(
                    "Error: Data Is not Saved Successfully.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
        }


        //################################ Validation Methods ################################
        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "Username cannot be blank");
            }
            else
                errorProvider1.SetError(txtUserName, null);

            if (_Mode == enMode.AddNew)
            {
                if(clsUser.isUserExist(txtUserName.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUserName, "username is used by another user");
                    return;
                }
                else
                    errorProvider1.SetError(txtUserName, null);
            }
            else
            {
                //incase update make sure not to use anothers user name
                if (_User.UserName != txtUserName.Text.Trim())
                {
                    if(clsUser.isUserExist(txtUserName.Text.Trim()))
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(txtUserName, "username is used by another user");
                        return;
                    }
                    else
                        errorProvider1.SetError(txtUserName, null);
                }
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(
                    txtPassword,
                    "Password cannot be blank"
                    );
            }
            else
                errorProvider1.SetError(txtPassword, null);   
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(
                    txtConfirmPassword, 
                    "Password Confirmation does not match Password!"
                    );
            }
            else
                errorProvider1.SetError(txtConfirmPassword, null);
        }
    }
}
