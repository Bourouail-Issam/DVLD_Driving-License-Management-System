using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsDetainedLicenseData
    {
        public static bool IsLicenseDetained(int LicenseID)
        {
            const string query = @"SELECT IIF(
                                 EXISTS (
                                     SELECT 1 
                                     FROM DetainedLicenses 
                                     WHERE LicenseID = @LicenseID  
                                       AND IsReleased = 0
                                 ),
                                 1,
                                 0
                             );";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LicenseID", LicenseID);

                try
                {
                    connection.Open();
                    return Convert.ToBoolean(command.ExecuteScalar());
                }
                catch (SqlException sqlEx)
                {
                    // Logger.LogError($"Database error: {sqlEx.Message}");
                    return false;
                }
                catch (Exception ex)
                {
                    // Logger.LogError($"Error: {ex.Message}");
                    return false;
                }
            }
        }

        public static int AddNewDetainedLicense(
         int LicenseID, DateTime DetainDate,
         float FineFees, int CreatedByUserID)
        {
            int DetainID = -1;


            const string query = @"
                          INSERT INTO dbo.DetainedLicenses
                              (LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased)
                          VALUES
                              (@LicenseID, @DetainDate, @FineFees, @CreatedByUserID, 0);
                          
                          SELECT SCOPE_IDENTITY();";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@LicenseID", LicenseID);
                cmd.Parameters.AddWithValue("@DetainDate", DetainDate);
                cmd.Parameters.AddWithValue("@FineFees", FineFees);
                cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                try
                {
                    conn.Open();

                    object result = cmd.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        DetainID = insertedID;
                    }
                }

                catch (Exception ex)
                {
                    //Console.WriteLine("Error: " + ex.Message);
                }
            }

            return DetainID;
        }
    }
}
