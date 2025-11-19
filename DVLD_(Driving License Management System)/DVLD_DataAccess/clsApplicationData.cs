using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsApplicationData
    {

        // ###################   CURD Methods   ###################
        public static bool GetApplicationInfoByID(int ApplicationID,
            ref int ApplicantPersonID, ref DateTime ApplicationDate, ref int ApplicationTypeID,
            ref byte ApplicationStatus, ref DateTime LastStatusDate,
            ref float PaidFees, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Applications WHERE ApplicationID = @ApplicationID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = ApplicationID;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    ApplicantPersonID = (int)reader["ApplicantPersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationStatus = (byte)reader["ApplicationStatus"];
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];

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

        public static bool DeleteApplication(int ApplicationID)
        {

            int rowsAffected = 0;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete Applications 
                                where ApplicationID = @ApplicationID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            cmd.CommandTimeout = 30;

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

        public static int AddNewApplication(
            int applicantPersonID,
            DateTime applicationDate,
            int applicationTypeID,
            byte applicationStatus,
            DateTime lastStatusDate,
            decimal paidFees,
            int createdByUserID)
        {
            int applicationID = -1; // ✅ camelCase

            string query = @"INSERT INTO Applications ( 
                                 ApplicantPersonID, ApplicationDate, ApplicationTypeID,
                                 ApplicationStatus, LastStatusDate,
                                 PaidFees, CreatedByUserID)
                             VALUES (@ApplicantPersonID, @ApplicationDate, @ApplicationTypeID,
                                     @ApplicationStatus, @LastStatusDate, @PaidFees, @CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";

         
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@ApplicantPersonID", SqlDbType.Int).Value = applicantPersonID;
                cmd.Parameters.Add("@ApplicationDate", SqlDbType.DateTime).Value = applicationDate;
                cmd.Parameters.Add("@ApplicationTypeID", SqlDbType.Int).Value = applicationTypeID;
                cmd.Parameters.Add("@ApplicationStatus", SqlDbType.TinyInt).Value = applicationStatus;
                cmd.Parameters.Add("@LastStatusDate", SqlDbType.DateTime).Value = lastStatusDate;
                cmd.Parameters.Add("@PaidFees", SqlDbType.SmallMoney).Value = paidFees;
                cmd.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = createdByUserID;

                cmd.CommandTimeout = 30;

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        applicationID = insertedID;
                }
                catch (SqlException sqlEx)
                {
                    // Logger.LogError($"DB Error: {sqlEx.Message}");
                    applicationID = -1;
                }
                catch (Exception ex)
                {
                    // Logger.LogError($"Error: {ex.Message}");
                    applicationID = -1;
                }             
            }

            return applicationID;
        }


        public static bool UpdateApplication(
            int applicationID, int applicantPersonID, 
            DateTime applicationDate, int applicationTypeID,
            byte applicationStatus, DateTime lastStatusDate,
            decimal paidFees, int createdByUserID
            )
        {

            int rowsAffected = 0;
          

            string query = @"Update  Applications  
                            set ApplicantPersonID = @ApplicantPersonID,
                                ApplicationDate = @ApplicationDate,
                                ApplicationTypeID = @ApplicationTypeID,
                                ApplicationStatus = @ApplicationStatus, 
                                LastStatusDate = @LastStatusDate,
                                PaidFees = @PaidFees,
                                CreatedByUserID=@CreatedByUserID
                            where ApplicationID=@ApplicationID";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {

                cmd.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = applicationID;
                cmd.Parameters.Add("@ApplicantPersonID", SqlDbType.Int).Value = applicantPersonID;
                cmd.Parameters.Add("@ApplicationDate", SqlDbType.DateTime).Value = applicationDate;
                cmd.Parameters.Add("@ApplicationTypeID", SqlDbType.Int).Value = applicationTypeID;
                cmd.Parameters.Add("@ApplicationStatus", SqlDbType.TinyInt).Value = applicationStatus;
                cmd.Parameters.Add("@LastStatusDate", SqlDbType.DateTime).Value = lastStatusDate;
                cmd.Parameters.Add("@PaidFees", SqlDbType.SmallMoney).Value = paidFees;
                cmd.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = createdByUserID;
                cmd.CommandTimeout = 30;
                try
                {
                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (SqlException sqlEx) 
                {
                    // Logger.LogError($"DB Error: {sqlEx.Message}");
                    return false;
                }
                catch (Exception ex)
                {
                    // Logger.LogError($"Error: {ex.Message}");
                    return false;
                }

                return (rowsAffected > 0);
            }
        }


        // ###################   Methods   ###################

        public static int GetActiveApplicationIDForLicenseClass(int personID, int applicationTypeID, int licenseClassID)
        {
            int active_ApplicationId = -1;
            const int ACTIVE_STATUS = 1;

            string query = @"SELECT TOP 1 App.ApplicationID  
                            FROM Applications  App
                            INNER JOIN LocalDrivingLicenseApplications LDLA
                                ON  App.ApplicationID = LDLA.ApplicationID
                            WHERE   App.ApplicantPersonID = @applicantPersonID 
                                AND App.ApplicationTypeID = @applicationTypeID 
							    AND LDLA.LicenseClassID = @licenseClassID
                                AND App.ApplicationStatus = @status
                            ORDER BY App.ApplicationID DESC;";

   
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using(SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@applicantPersonID", SqlDbType.Int).Value = personID;
                cmd.Parameters.Add("@applicationTypeID", SqlDbType.Int).Value = applicationTypeID;
                cmd.Parameters.Add("@licenseClassID", SqlDbType.Int).Value = licenseClassID;
                cmd.Parameters.Add("@status", SqlDbType.Int).Value = ACTIVE_STATUS;

                cmd.CommandTimeout=30;

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int appID))
                        active_ApplicationId = appID;
                }
                catch(SqlException sqlEx)
                {
                    // Logger.LogError($"DB Error: {sqlEx.Message}");
                     active_ApplicationId=-1;
                }
                catch (Exception ex)
                {
                    // Logger.LogError($"Error: {ex.Message}");
                     active_ApplicationId=-1;
                }

                return active_ApplicationId;
            }
        }
    }
}


