using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsLicenseData
    {
        public static int AddNewLicense(int ApplicationID, int DriverID, int LicenseClass,
            DateTime IssueDate, DateTime ExpirationDate, string Notes,
            decimal PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            int LicenseID = -1;

            string query = @"INSERT INTO Licenses
                                   (ApplicationID,
                                    DriverID,
                                    LicenseClass,
                                    IssueDate,
                                    ExpirationDate,
                                    Notes,
                                    PaidFees,
                                    IsActive,IssueReason,
                                    CreatedByUserID)
                             VALUES
                                   (
                                   @ApplicationID,
                                   @DriverID,
                                   @LicenseClass,
                                   @IssueDate,
                                   @ExpirationDate,
                                   @Notes,
                                   @PaidFees,
                                   @IsActive,@IssueReason, 
                                   @CreatedByUserID);
                              SELECT SCOPE_IDENTITY();";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = ApplicationID;
                cmd.Parameters.Add("@DriverID", SqlDbType.Int).Value = DriverID;
                cmd.Parameters.Add("@LicenseClass", SqlDbType.Int).Value = LicenseClass;
                cmd.Parameters.Add("@IssueDate", SqlDbType.DateTime).Value = IssueDate;
                cmd.Parameters.Add("@ExpirationDate", SqlDbType.DateTime).Value = ExpirationDate;

                if (string.IsNullOrWhiteSpace(Notes))
                    cmd.Parameters.AddWithValue("@Notes", DBNull.Value);
                else
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 500).Value = Notes;

                cmd.Parameters.Add("@PaidFees", SqlDbType.SmallMoney).Value = PaidFees;
                cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = IsActive;
                cmd.Parameters.Add("@IssueReason", SqlDbType.TinyInt).Value = IssueReason;

                cmd.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = CreatedByUserID;

                try
                {
                    conn.Open();

                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                        LicenseID = Convert.ToInt32(result);
                }
                catch (SqlException sqlEx)
                {
                    //Logger.LogError($"DB Error in AddNewLicense: {sqlEx.Message}");
                    LicenseID = -1;
                }
                catch (Exception ex)
                {
                    LicenseID = -1;
                }
            }

            return LicenseID;
        }

        public static bool UpdateLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClass,
            DateTime IssueDate, DateTime ExpirationDate, string Notes,
            decimal PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {

            int rowsAffected = 0;

            string query = @"UPDATE Licenses
                           SET ApplicationID=@ApplicationID, DriverID = @DriverID,
                              LicenseClass = @LicenseClass,
                              IssueDate = @IssueDate,
                              ExpirationDate = @ExpirationDate,
                              Notes = @Notes,
                              PaidFees = @PaidFees,
                              IsActive = @IsActive,
                              IssueReason = @IssueReason,
                              CreatedByUserID = @CreatedByUserID
                         WHERE LicenseID = @LicenseID";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@LicenseID", SqlDbType.Int).Value = LicenseID;
                cmd.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = ApplicationID;
                cmd.Parameters.Add("@DriverID", SqlDbType.Int).Value = DriverID;
                cmd.Parameters.Add("@LicenseClass", SqlDbType.Int).Value = LicenseClass;
                cmd.Parameters.Add("@IssueDate", SqlDbType.DateTime).Value = IssueDate;
                cmd.Parameters.Add("@ExpirationDate", SqlDbType.DateTime).Value = ExpirationDate;

                if (string.IsNullOrWhiteSpace(Notes))
                    cmd.Parameters.AddWithValue("@Notes", DBNull.Value);
                else
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 500).Value = Notes;

                cmd.Parameters.Add("@PaidFees", SqlDbType.SmallMoney).Value = PaidFees;
                cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = IsActive;
                cmd.Parameters.Add("@IssueReason", SqlDbType.TinyInt).Value = IssueReason;
                cmd.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = CreatedByUserID;

                try
                {
                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (SqlException sqlEx)
                {
                    //Logger.LogError($"DB Error in UpdateLicense: {sqlEx.Message}");
                    rowsAffected = 0;
                }
                catch (Exception ex)
                {
                    //Console.WriteLine("Error: " + ex.Message);
                    rowsAffected = 0;
                }
            }
            return (rowsAffected > 0);
        }

        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            int LicenseID = -1;


            string query = @"SELECT TOP 1 L.LicenseID
                             FROM Licenses L 
                             INNER JOIN Drivers D ON L.DriverID = D.DriverID
                             WHERE L.LicenseClass = @LicenseClassID
                               AND D.PersonID = @PersonID
                               AND L.IsActive = 1
                             ORDER BY L.LicenseID DESC;";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonID;
                cmd.Parameters.Add("@LicenseClassID", SqlDbType.Int).Value = LicenseClassID;

                try
                {
                    conn.Open();

                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                        LicenseID = Convert.ToInt32(result);
                }
                catch (SqlException sqlEx)
                {
                    LicenseID = -1;
                }
                catch (Exception ex)
                {
                    //Console.WriteLine("Error: " + ex.Message);
                    LicenseID = -1;
                }
            }
            return LicenseID;
        }
    }
}
