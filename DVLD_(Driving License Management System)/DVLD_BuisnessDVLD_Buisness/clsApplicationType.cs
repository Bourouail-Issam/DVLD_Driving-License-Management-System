using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BuisnessDVLD_Buisness
{
    public class clsApplicationType
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ID { set; get; }
        public string Title { set; get; }
        public float Fees { set; get; }

        public clsApplicationType()

        {
            this.ID = -1;
            this.Title = "";
            this.Fees = 0;
            Mode = enMode.AddNew;
        }

        public clsApplicationType(int ID, string ApplicationTypeTitel, float ApplicationTypeFees)
        {
            this.ID = ID;
            this.Title = ApplicationTypeTitel;
            this.Fees = ApplicationTypeFees;
            Mode = enMode.Update;
        }

        //################################ CRUD Methods ################################

        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypeData.GetAllApplicationTypes();
        }

        public static clsApplicationType Find(int ID)
        {
            string Title = ""; float Fees = 0;

            if (clsApplicationTypeData.GetApplicationTypeInfoByID(ID, ref Title, ref Fees))

                return new clsApplicationType(ID, Title, Fees);
            else
                return null;

        }

        private bool _UpdateApplicationType()
        {
            //call DataAccess Layer 

            return clsApplicationTypeData.UpdateApplicationType(this.ID, this.Title, this.Fees);
        }


        // Note: This function _AddNewApplicationType isn't currently used in the project.
        // I’ve added it just in case we need this feature in the future.
        // It might save time later if a similar functionality is required.
        private bool _AddNewApplicationType()
        {
            //call DataAccess Layer 

            this.ID = clsApplicationTypeData.AddNewApplicationType(this.Title, this.Fees);

            return (this.ID != -1);
        }

        public bool Save()
        {
            switch (Mode)
            {
                // Note: This function _AddNewApplicationType isn't currently used in the project.
                // I’ve added it just in case we need this feature in the future.
                // It might save time later if a similar functionality is required.
                case enMode.AddNew:
                    {
                        if (_AddNewApplicationType())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        else
                            return false;
                    }

                case enMode.Update:
                    return _UpdateApplicationType();

            }
            return false;
        }

    }
}
