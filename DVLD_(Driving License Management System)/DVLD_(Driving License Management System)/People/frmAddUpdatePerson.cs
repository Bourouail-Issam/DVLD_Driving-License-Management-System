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
using System.IO;


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
            //First: set AutoValidate property of your Form to EnableAllowFocusChange in designer
            TextBox Temp = ((TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
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
                e.Cancel= true;
                errorProvider1.SetError(txtNationalNo, "This field is required!");
                return;
            }

            ////Make sure the national number is not used by another person
            if (clsPerson.isPersonExist(txtNationalNo.Text.Trim()) && txtNationalNo.Text.Trim() != _person.NationalNo)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "National Number is used for another person!");
                return;
            }

            errorProvider1.SetError(txtNationalNo, null);
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            //no need to validate the email incase it's empty.
            if (txtEmail.Text.Trim() == "")
            {
                errorProvider1.SetError(txtEmail, null);
                return;
            }

            //validate email format
            if (!clsValidation.ValidateEmail(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid Email Address Format!");
                return;
            }

            errorProvider1.SetError(txtEmail, null);
        }

        private void txtPhone_Validating(object sender, CancelEventArgs e)
        {
            // Check if phone number is empty
            if (string.IsNullOrWhiteSpace(txtPhone.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPhone, "Phone number is required!");
                return;
            }

            // Validate phone number format using clsValidation
            if (!clsValidation.ValidatePhoneNumber(txtPhone.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPhone, "Invalid phone number! Example: +212612345678 or +14085551234");
                return;
            }
         
            errorProvider1.SetError(txtPhone, null);
        }


        private void rbMale_Click(object sender, EventArgs e)
        {
            //change the defualt image to male incase there is no image set.
            if (pbImage.ImageLocation == null)
                pbImage.Image = Resources.Men_p;
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            //change the defualt image to female incase there is no image set.
            if (pbImage.ImageLocation == null)
                pbImage.Image = Resources.Female_p;
        }

        private bool _HandlePersonImage()
        {
            // this procedure will handle the person image,
            // it will take care of deleting the old image from the folder
            // in case the image changed. and it will rename the new image with guid and 
            // place it in the images folder.

            if (_person.ImagePath != pbImage.ImageLocation)
            {
                if (_person.ImagePath != string.Empty)
                {
                    try
                    {
                        File.Delete(_person.ImagePath);
                    }
                    catch(IOException iox)
                    {
                        MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                if (pbImage.ImageLocation != null)
                {
                    //then we copy the new image to the image folder after we rename it
                    string SourceImageFile = pbImage.ImageLocation.ToString();


                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        pbImage.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(!_HandlePersonImage()) return;

            int NationalityCountryID = clsCountry.Find(cbCountry.Text).ID;

            _person.FirstName = txtFirstName.Text;
            _person.SecondName = txtSecondName.Text;
            _person.ThirdName = txtThirdName.Text;
            _person.LastName = txtLastName.Text;
            _person.NationalNo = txtNationalNo.Text;
            _person.Phone = txtPhone.Text;
            _person.Email = txtEmail.Text;
            _person.Address = txtAddress.Text;
            _person.DateOfBirth = dtpDateOfBirth.Value;

            if (rbMale.Checked)
                _person.Gendor = 0;
            else
                _person.Gendor = 1;

            _person.NationalityCountryID = NationalityCountryID;

            if (pbImage.ImageLocation != null)
                _person.ImagePath = pbImage.ImageLocation;
            else
                _person.ImagePath = string.Empty;

            if (_person.Save())
            {
                lbPersonID.Text = _person.PersonID.ToString();
                //change form mode to update.
                _Mode = enMode.Update;
                lbNameOfForm.Text = "Update Person";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
