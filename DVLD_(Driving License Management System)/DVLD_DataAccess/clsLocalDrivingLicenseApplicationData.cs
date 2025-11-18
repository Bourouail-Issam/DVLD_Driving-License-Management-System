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

        public static bool DeleteLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {

            int rowsAffected = 0;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete LocalDrivingLicenseApplications 
                                where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandTimeout=30;

            cmd.Parameters.Add("@LocalDrivingLicenseApplicationID", SqlDbType.Int).Value = 
                LocalDrivingLicenseApplicationID;

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return (rowsAffected > 0);
        }

        public static int AddNewLocalDrivingLicenseApplication(int ApplicationID, int LicenseClassID)
        {

            //this function will return the new person id if succeeded and -1 if not.
            int LocalDrivingLicenseApplicationID = -1;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO LocalDrivingLicenseApplications (ApplicationID,LicenseClassID)
                                         VALUES (@ApplicationID,@LicenseClassID);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add("ApplicationID", SqlDbType.Int).Value = ApplicationID;
            cmd.Parameters.Add("LicenseClassID", SqlDbType.Int).Value = LicenseClassID;

            cmd.CommandTimeout = 30;

            try
            {
                conn.Open();

                object result = cmd.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    LocalDrivingLicenseApplicationID = insertedID;
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
            }

            finally
            {
                conn.Close();
            }

            return LocalDrivingLicenseApplicationID;
        }

        public static bool UpdateLocalDrivingLicenseApplication
            (int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
        {

            int rowsAffected = 0;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  LocalDrivingLicenseApplications  
                            set ApplicationID = @ApplicationID,
                                LicenseClassID = @LicenseClassID
                            where LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add("@LocalDrivingLicenseApplicationID", SqlDbType.Int).Value = LocalDrivingLicenseApplicationID;
            cmd.Parameters.Add("ApplicationID", SqlDbType.Int).Value = ApplicationID;
            cmd.Parameters.Add("LicenseClassID", SqlDbType.Int).Value = LicenseClassID;
            
            cmd.CommandTimeout = 30;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                conn.Close();
            }

            return (rowsAffected > 0);
        }
    }
}
