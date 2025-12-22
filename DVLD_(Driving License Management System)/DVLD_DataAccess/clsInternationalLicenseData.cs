using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsInternationalLicenseData
    {
        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {

            DataTable dt = new DataTable();

            const string query = @"SELECT InternationalLicenseID, 
                                  ApplicationID,
                                  IssuedUsingLocalLicenseID, 
                                  IssueDate, 
                                  ExpirationDate, 
                                  IsActive
                           FROM InternationalLicenses 
                           WHERE DriverID = @DriverID
                           ORDER BY ExpirationDate DESC";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@DriverID", DriverID);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                            dt.Load(reader);
                    }
                }

                catch (SqlException sqlEx)
                {
                    // Logger.LogError($"Database error: {sqlEx.Message}");
                }
                catch (Exception ex)
                {
                    // Logger.LogError($"Error: {ex.Message}");
                }
            }
            return dt;
        }
    }
}
