using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BuisnessDVLD_Buisness
{
    public class clsCountry
    {
        public int ID { set; get; }
        public string CountryName { set; get; }

        public clsCountry()
        {
            this.ID = -1;
            this.CountryName = "";
        }

        private clsCountry(int ID, string CountryName)
        {
            this.ID = ID;
            this.CountryName = CountryName;
        }

        public static DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();
        }

        public static clsCountry Find(int countryID)
        {
            string countryName = "";

            if (clsCountryData.GetCountryInfoByID(countryID, ref countryName))
                return new clsCountry(countryID, countryName);
            else
                return null;
        }
        public static clsCountry Find(string countryName)
        {
            int countryID = -1;

            if (clsCountryData.GetCountryInfoByName(ref countryID,countryName))
                return new clsCountry(countryID, countryName);
            else
                return null;
        }


    }
}
