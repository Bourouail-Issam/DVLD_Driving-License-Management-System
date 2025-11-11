using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public  class clsCountryData
    {
        public static DataTable GetAllCountries()
        {

            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Countries order by CountryName";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandTimeout = 30;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();

            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;

        }

        public static bool GetCountryInfoByID(int ID, ref string CountryName)
        {
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Countries WHERE CountryID = @CountryID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value= ID;
            cmd.CommandTimeout = 30;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    CountryName = (string)reader["CountryName"];
                    // The record was found
                    isFound = true;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isFound;
        }

        public static bool GetCountryInfoByName(ref int ID, string CountryName)
        {
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Countries WHERE CountryName = @CountryName";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add("@CountryName",SqlDbType.NVarChar).Value = CountryName;
            cmd.CommandTimeout = 30;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    ID = (int)reader["CountryID"];
                    // The record was found
                    isFound = true;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isFound;
        }
    }
}
