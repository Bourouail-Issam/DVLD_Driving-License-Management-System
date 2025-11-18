using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsLocalDrivingLicenseApplicationData
    {
        // ###################   CURD Methods   ###################
        public static DataTable GetAllLocalDrivingLicenseApplications()
        {

            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT *
                              FROM LocalDrivingLicenseApplications_View
                              order by ApplicationDate Desc";


            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandTimeout = 30;
            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                    dt.Load(reader);

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


        public static bool GetLocalDrivingLicenseApplicationInfoByID(
           int LocalDrivingLicenseApplicationID, 
           ref int ApplicationID, ref int LicenseClassID)
        {
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"SELECT * FROM LocalDrivingLicenseApplications 
                             WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add("@LocalDrivingLicenseApplicationID",SqlDbType.Int).Value = LocalDrivingLicenseApplicationID;
            cmd.CommandTimeout = 30;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    ApplicationID = (int)reader["ApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];

                    // The record was found
                    isFound = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                conn.Close();
            }

            return isFound;
        }

    }
}
