using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BuisnessDVLD_Buisness
{
    public class clsLicenseClass
    {
        private enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode = enMode.AddNew;

        public int LicenseClassID { set; get; }
        public string ClassName { set; get; }
        public string ClassDescription { set; get; }
        public byte MinimumAllowedAge { set; get; }
        public byte DefaultValidityLength { set; get; }
        public float ClassFees { set; get; }

        // ###################   Constructors   ###################
        public clsLicenseClass()
        {
            this.LicenseClassID = -1;
            this.ClassName = "";
            this.ClassDescription = "";
            this.MinimumAllowedAge = 18;
            this.DefaultValidityLength = 10;
            this.ClassFees = 0;

            _Mode = enMode.AddNew;

        }

        public clsLicenseClass(
            int LicenseClassID, string ClassName,string ClassDescription,
            byte MinimumAllowedAge, byte DefaultValidityLength, float ClassFees
            )
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;
            _Mode = enMode.Update;
        }

        //################################ CRUD Methods ################################
        public static DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassData.GetAllLicenseClasses();
        }

        public static clsLicenseClass Find(int LicenseClassID)
        {
            string ClassName = "", ClassDescription = "";
            byte MinimumAllowedAge = 18,DefaultValidityLength = 10; 
            float ClassFees = 0;

            if (clsLicenseClassData.GetLicenseClassInfoByID(LicenseClassID, ref ClassName, ref ClassDescription,
                    ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))

                return new clsLicenseClass(LicenseClassID, ClassName, ClassDescription,
                    MinimumAllowedAge, DefaultValidityLength, ClassFees);
            else
                return null;

        }
        public static clsLicenseClass Find(string ClassName)
        {
            int LicenseClassID = -1; 
            string ClassDescription = "";
            byte MinimumAllowedAge = 18,DefaultValidityLength = 10;
            decimal ClassFees = 0;

            if (clsLicenseClassData.GetLicenseClassInfoByClassName(ClassName, ref LicenseClassID, ref ClassDescription,
                    ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))

                return new clsLicenseClass(LicenseClassID, ClassName, ClassDescription,
                    MinimumAllowedAge, DefaultValidityLength, (float)ClassFees);
            else
                return null;

        }
    }
}
