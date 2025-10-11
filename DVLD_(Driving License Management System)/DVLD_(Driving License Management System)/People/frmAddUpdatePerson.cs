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
    }
}
