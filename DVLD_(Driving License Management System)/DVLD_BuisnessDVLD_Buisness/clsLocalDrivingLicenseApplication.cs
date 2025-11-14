using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BuisnessDVLD_Buisness
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {
        private enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode = enMode.AddNew;
        public int LocalDrivingLicenseApplicationID { set; get; }

        public int LicenseClassID { set; get; }
        public clsLicenseClass LicenseClassInfo;

        public string PersonFullName
        {
            get
            {
                return clsPerson.Find(ApplicantPersonID).FullName;
            }
        }

        // ###################   Constructors   ###################
        public clsLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.LicenseClassID = -1;

            _Mode = enMode.AddNew;
        }
        public clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int ApplicantPersonID,
             DateTime ApplicationDate, int ApplicationTypeID,
              enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
               float PaidFees, int CreatedByUserID, int LicenseClassID)     
            : base(ApplicationID,ApplicantPersonID,ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate,
                  PaidFees, CreatedByUserID) 
        {
            this.LocalDrivingLicenseApplicationID= LocalDrivingLicenseApplicationID;
            this.LicenseClassID= LicenseClassID;
            this.LicenseClassInfo = clsLicenseClass.Find(LicenseClassID);

            _Mode = enMode.Update;
        }
    }
}
