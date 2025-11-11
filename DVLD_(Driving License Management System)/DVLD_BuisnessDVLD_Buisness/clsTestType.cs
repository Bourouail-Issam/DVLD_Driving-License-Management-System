using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BuisnessDVLD_Buisness
{
    public class clsTestType
    {
        private enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode = enMode.AddNew;

        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };
        public clsTestType.enTestType ID { set; get; }

        public string Title { set; get; }
        public string Description { set; get; }
        public float Fees { set; get; }

        public clsTestType()
        {
            this.ID = clsTestType.enTestType.VisionTest;
            this.Title = "";
            this.Description = "";
            this.Fees = 0;
            _Mode = enMode.AddNew;
        }

        public clsTestType(clsTestType.enTestType ID, string TestTypeTitle, string Description, float TestTypeFees)
        {
            this.ID = ID;
            this.Title = TestTypeTitle;
            this.Description = Description;
            this.Fees = TestTypeFees;
            _Mode = enMode.Update;
        }


        //################################ CRUD Methods ################################
        public static DataTable GetAllTestTypes()
        {
            return clsTestTypeData.GetAllTestTypes();
        }
    }
}
