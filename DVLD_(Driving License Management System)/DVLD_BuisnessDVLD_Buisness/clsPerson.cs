using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DVLD_BuisnessDVLD_Buisness
{
    public class clsPerson   
    {
        enum enMode { AddNew = 0, Update = 1 };
        enMode _Mode = enMode.AddNew;


        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte Gendor { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }

        private string _ImagePath;
        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }

        public clsCountry CountryInfo;


        public clsPerson()
        {
            PersonID = -1;
            NationalNo = string.Empty;
            FirstName = string.Empty;
            SecondName = string.Empty;
            ThirdName = string.Empty;
            LastName = string.Empty;
            DateOfBirth = DateTime.MinValue;
            Gendor = 0;
            Address = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            NationalityCountryID = -1;
            ImagePath = string.Empty;
            _Mode = enMode.AddNew;
        }
        public clsPerson(
            int personID, 
            string nationalNo, 
            string firstName,
            string secondName, 
            string thirdName,
            string lastName,
            DateTime dateOfBirth,
            byte gendor, 
            string address, 
            string phone, 
            string email,
            int nationalityCountryID,
            string imagePath )
        {
            this.PersonID = personID;
            this.NationalNo = nationalNo;
            this.FirstName = firstName;
            this.SecondName = secondName;
            this.ThirdName = thirdName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.Gendor = gendor;
            this.Address = address;
            this.Phone = phone;
            this.Email = email;
            this.NationalityCountryID = nationalityCountryID;
            this.ImagePath = imagePath;
            this.CountryInfo = clsCountry.GetCountryInfoByID(nationalityCountryID);
            _Mode = enMode.Update;
        }

        //################################ CRUD Methods ################################

        public static DataTable GetAllPersons()
        {
            return clsPersonData.GetAllPersons();
        }

        static public clsPerson Find(int PersonID)
        {
            string nationalNo = "", firstName = "", secondName = "",
                   thirdName = "", lastName = "", address = "",
                   phone = "", email = "", imagePath = "";

            DateTime dateOfBirth = DateTime.Now;
            byte gendor = 0;
            int nationalityCountryID = -1;


            bool isFound = clsPersonData.GetPersonInfoByID(
                         PersonID, ref nationalNo,
                         ref firstName, ref secondName, ref thirdName,
                         ref lastName, ref dateOfBirth, ref gendor,
                         ref address, ref phone, ref email,
                         ref nationalityCountryID, ref imagePath);


            if (isFound)
                //we return new object of that person with the right data
                return new clsPerson(
                              PersonID, nationalNo, firstName,
                              secondName, thirdName, lastName,
                              dateOfBirth, gendor, address,
                              phone, email,
                              nationalityCountryID, imagePath
                              );
            else
                return null;
        }

        private bool _AddNewPerson()
        {
            //call DataAccess Layer 

            this.PersonID = clsPersonData.AddNewPerson(this.NationalNo, this.FirstName,
                  this.SecondName, this.ThirdName, this.LastName,
                  this.DateOfBirth, this.Gendor, this.Address, this.Phone, this.Email,
                  this.NationalityCountryID, this.ImagePath);

            return (PersonID != -1);
        }

        //################################ Save Method ################################
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewPerson())
                        {
                            _Mode = enMode.Update;
                            return true;
                        }
                        else
                            return false;
                    }
                case enMode.Update:
                    {
                        return true;
                    }
            }
            return false;
        }
    }
}