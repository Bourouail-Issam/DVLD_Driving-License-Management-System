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
    }
}
