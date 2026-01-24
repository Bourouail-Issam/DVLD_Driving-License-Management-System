using System;
using System.Collections.Generic;
using System.Data;
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

        public static bool UpdateDetainedLicense(int DetainID,
            int LicenseID, DateTime DetainDate,
            float FineFees, int CreatedByUserID)
        {

            int rowsAffected = 0;

            const string query = @"UPDATE dbo.DetainedLicenses
                              SET LicenseID = @LicenseID, 
                                  DetainDate = @DetainDate, 
                                  FineFees = @FineFees,
                                  CreatedByUserID = @CreatedByUserID
                              WHERE DetainID=@DetainID;";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@DetainID", DetainID);
                cmd.Parameters.AddWithValue("@LicenseID", LicenseID);
                cmd.Parameters.AddWithValue("@DetainDate", DetainDate);
                cmd.Parameters.AddWithValue("@FineFees", FineFees);
                cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


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
            }
            return (rowsAffected > 0);
        }

        public static DataTable GetAllDetainedLicenses()
        {
            DataTable dt = new DataTable();

            const string query = @"SELECT DetainID, LicenseID, DetainDate, IsReleased, FineFees, 
                                          ReleaseDate, NationalNo, FullName, ReleaseApplicationID
                                   FROM detainedLicenses_View 
                                   ORDER BY IsReleased ,DetainID;";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
                catch (Exception ex)
                {
                    // Console.WriteLine("Error: " + ex.Message);
                }
            }

            return dt;
        }

        public static bool GetDetainedLicenseInfoByID(int DetainID,
            ref int LicenseID, ref DateTime DetainDate,
            ref float FineFees, ref int CreatedByUserID,
            ref bool IsReleased, ref DateTime ReleaseDate,
            ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool isFound = false;


            const string query = @"SELECT DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID,
                        IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID
                 FROM DetainedLicenses 
                 WHERE DetainID = @DetainID";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@DetainID", SqlDbType.Int).Value = DetainID;

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            LicenseID = (int)reader["LicenseID"];
                            DetainDate = (DateTime)reader["DetainDate"];
                            FineFees = Convert.ToSingle(reader["FineFees"]);
                            CreatedByUserID = (int)reader["CreatedByUserID"];

                            IsReleased = (bool)reader["IsReleased"];

                            // those fildes in Database is not requirerd they need hck if nulls
                            ReleaseDate = reader["ReleaseDate"] == DBNull.Value 
                                ? DateTime.MaxValue
                                : (DateTime)reader["ReleaseDate"];

                            ReleasedByUserID = reader["ReleasedByUserID"] == DBNull.Value
                                ? -1
                                : (int)reader["ReleasedByUserID"];

                            ReleaseApplicationID = reader["ReleaseApplicationID"] == DBNull.Value
                                ? -1
                                : (int)reader["ReleaseApplicationID"];

                            // The record was found
                            isFound = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Console.WriteLine("Error: " + ex.Message);
                    isFound = false;
                }
            }
            return isFound;
        }
    }
}
