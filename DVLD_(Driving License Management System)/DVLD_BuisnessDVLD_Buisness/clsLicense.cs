using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BuisnessDVLD_Buisness
{
    public  class clsLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public clsLicenseClass LicenseClassIfo;
        public clsDriver DriverInfo;

        public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4 };

        public int LicenseID { set; get; }
        public int ApplicationID { set; get; }
        public int DriverID { set; get; }
        public int LicenseClass { set; get; }
        public DateTime IssueDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public string Notes { set; get; }
        public float PaidFees { set; get; }
        public bool IsActive { set; get; }
        public enIssueReason IssueReason { set; get; }
        public int CreatedByUserID { set; get; }

        public string IssueReasonText
        {
            get
            {
                return GetIssueReasonText(this.IssueReason);
            }
        }


        // ###################   Constructors   ###################

        clsLicense()
        {
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClass = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = "";
            this.PaidFees = 0;
            this.IsActive = true;
            this.IssueReason = enIssueReason.FirstTime;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        public clsLicense(
            int LicenseID, int ApplicationID, int DriverID,
            int LicenseClass,DateTime IssueDate, 
            DateTime ExpirationDate, string Notes,
            float PaidFees, bool IsActive, 
            enIssueReason IssueReason, int CreatedByUserID
            )

        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClass = LicenseClass;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;

            this.LicenseClassIfo = clsLicenseClass.Find(this.LicenseClass);
            this.DriverInfo = clsDriver.FindByDriverID(DriverID);
            Mode = enMode.Update;
        }

        // ###################   CRUD Methods   ###################

        static public clsLicense Find(int LicenseID)
        {
            int ApplicationID = -1, DriverID = -1 , LicenseClass = -1 , CreatedByUserID = -1;
            DateTime IssueDate = default, ExpirationDate = default;
            string Notes = string.Empty;
            float PaidFees = 0;
            bool IsActive = false;
            byte IssueReason = 0;

            if(clsLicenseData.GetLicenseInfoByID(LicenseID,ref ApplicationID, ref DriverID,
                ref LicenseClass, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees,
                ref IsActive, ref IssueReason, ref CreatedByUserID))
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass,
                                     IssueDate, ExpirationDate, Notes,PaidFees,
                                     IsActive, (enIssueReason)IssueReason, CreatedByUserID);
            }
            return null;
        }

        public static string GetIssueReasonText(enIssueReason IssueReason)
        {

            switch (IssueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.DamagedReplacement:
                    return "Replacement for Damaged";
                case enIssueReason.LostReplacement:
                    return "Replacement for Lost";
                default:
                    return "First Time";
            }
        }

        private bool _AddNewLicense()
        {
            //call DataAccess Layer 

            this.LicenseID = clsLicenseData.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClass,
               this.IssueDate, this.ExpirationDate, this.Notes, (decimal)this.PaidFees,
               this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);


            return (this.LicenseID != -1);
        }

        private bool _UpdateLicense()
        {
            //call DataAccess Layer 

            return clsLicenseData.UpdateLicense(this.LicenseID, this.ApplicationID,this.DriverID, 
                this.LicenseClass,this.IssueDate, this.ExpirationDate, this.Notes, (decimal)this.PaidFees,
               this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;


                case enMode.Update:
                    return _UpdateLicense();
            }
            return false;
        }

        public static bool IsLicenseExistByPersonID(int PersonID, int LicenseClassID)
        {
            return (GetActiveLicenseIDByPersonID(PersonID, LicenseClassID) != -1);
        }

        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            return clsLicenseData.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);
        }


    }
}
