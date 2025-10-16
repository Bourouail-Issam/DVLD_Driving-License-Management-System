using DVLD__Driving_License_Management_System_.Global_Classes;
using DVLD__Driving_License_Management_System_.Properties;
using DVLD_BuisnessDVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DVLD__Driving_License_Management_System_.People
{
    public partial class frmAddUpdatePerson : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        private int _PersonID = -1;
        private clsPerson _person;


        public frmAddUpdatePerson()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddUpdatePerson(int PersonID )
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _PersonID = PersonID;
        }

        private void _FillCountriesInComoboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();

            foreach (DataRow row in dtCountries.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);
            }
        }

        private void _ResetDefualtValues()
        {
            //this will initialize the reset the defaule values
            _FillCountriesInComoboBox();

            if (_Mode == enMode.AddNew)
            {
                lbNameOfForm.Text = "Add New Person";
                _person = new clsPerson();
            }
            else
                lbNameOfForm.Text = "Update Person";

            //set default image for the person.
            if (rbMale.Checked) 
                pbImage.Image = Resources.Men_p;
            else
                pbImage.Image = Resources.Female_p;

            //hide/show the remove linke incase there is no image for the person.
            llbRemoveImage.Visible = (pbImage.ImageLocation != null);

            //we set the max date to 18 years from today, and set the default value the same.
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            //should not allow adding age more than 100 years
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);

            //this will set default country to Morocco.
            cbCountry.SelectedIndex = cbCountry.FindString("Morocco");

            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            rbMale.Checked = true;
            txtNationalNo.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
        }

        private void _LoadDataPerson()
        {
            _person = clsPerson.Find(_PersonID);

            if (_person == null)
            {
                MessageBox.Show(
                    "No Person with ID = " + _PersonID,
                    "Person Not Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                    );

                this.Close();
                return;
            }


            //the following code will not be executed if the person was not found
            lbPersonID.Text = _PersonID.ToString();
            txtNationalNo.Text = _person.NationalNo;
            txtFirstName.Text = _person.FirstName;
            txtSecondName.Text = _person.SecondName;
            txtThirdName.Text = _person.ThirdName;
            txtLastName.Text = _person.LastName;

            dtpDateOfBirth.Value = _person.DateOfBirth;

            if (_person.Gendor == 0)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;

            //cbCountry.SelectedIndex = cbCountry.FindString(_person.);
            txtPhone.Text = _person.Phone;
            txtEmail.Text = _person.Email;
            txtAddress.Text = _person.Address;
            cbCountry.SelectedIndex = cbCountry.FindString(_person.CountryInfo.CountryName);

            //load person image incase it was set.
            if (_person.ImagePath != "")
                pbImage.ImageLocation = _person.ImagePath;


            //hide/show the remove linke incase there is no image for the person.
            llbRemoveImage.Visible = (_person.ImagePath != "");
        }

        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if (_Mode == enMode.Update)
                _LoadDataPerson();
        }
    
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LLbSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog_ImagePerson.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog_ImagePerson.FilterIndex = 1;
            openFileDialog_ImagePerson.RestoreDirectory = true;

            if (openFileDialog_ImagePerson.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog_ImagePerson.FileName;
                pbImage.Load(selectedFilePath);
                llbRemoveImage.Visible = true;
            }
        }

        private void llbRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbImage.ImageLocation = null;

            if (rbMale.Checked)
                pbImage.Image = Resources.Men_p;
            else
                pbImage.Image = Resources.Female_p;

                llbRemoveImage.Visible = false;
        }


        // Validation Error Provider
        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            // First: set AutoValidate property of your Form to EnableAllowFocusChange in designer 
            TextBox Temp = ((TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {
                errorProvider1.SetError(Temp, null);
            }

        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
            {
                errorProvider1.SetError(txtNationalNo, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }

            //Make sure the national number is not used by another person
            if (clsPerson.isPersonExist(txtNationalNo.Text.Trim()) && txtNationalNo.Text.Trim() != _person.NationalNo)
            {
                errorProvider1.SetError(txtNationalNo, "National Number is used for another person!");
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {

            //no need to validate the email incase it's empty.
            if (txtEmail.Text.Trim() == "")
                return;

            //validate email format
            if (!clsValidation.ValidateEmail(txtEmail.Text))
            {
                errorProvider1.SetError(txtEmail, "Invalid Email Address Format!");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            }

        }
    }
}
