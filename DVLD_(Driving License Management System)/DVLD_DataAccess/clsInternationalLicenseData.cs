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
        // ###################   CURD Methods   ###################
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

        public static bool GetInternationalLicenseInfoByID(int InternationalLicenseID,
            ref int ApplicationID,
            ref int DriverID, ref int IssuedUsingLocalLicenseID,
            ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedByUserID)
        {
            const string query = @"SELECT ApplicationID,
                                    DriverID,
                                    IssuedUsingLocalLicenseID,
                                    IssueDate,
                                    ExpirationDate,
                                    IsActive,
                                    CreatedByUserID
                            FROM InternationalLicenses 
                            WHERE InternationalLicenseID = @InternationalLicenseID";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {

                cmd.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ApplicationID = (int)reader["ApplicationID"];
                            DriverID = (int)reader["DriverID"];
                            IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                            IssueDate = (DateTime)reader["IssueDate"];
                            ExpirationDate = (DateTime)reader["ExpirationDate"];
                            IsActive = (bool)reader["IsActive"];
                            CreatedByUserID = (int)reader["CreatedByUserID"];

                            // The record was found
                            return true;
                        }
                    }
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
            return false;
        }

    }
}
