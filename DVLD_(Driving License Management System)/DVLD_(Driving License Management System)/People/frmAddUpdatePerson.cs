using DVLD__Driving_License_Management_System_.Properties;
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
            dtpDateOfBirth.MaxDate = DateTime.Now.AddDays(-18);
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
            rTxtAddress.Text = "";
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
            rTxtAddress.Text = _person.Address;
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
    }
}
