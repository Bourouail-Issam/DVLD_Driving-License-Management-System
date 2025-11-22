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

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = ApplicationID;
                command.Parameters.Add("@DriverID", SqlDbType.Int).Value = DriverID;
                command.Parameters.Add("@LicenseClass", SqlDbType.Int).Value = LicenseClass;
                command.Parameters.Add("@IssueDate", SqlDbType.DateTime).Value = IssueDate;
                command.Parameters.Add("@ExpirationDate", SqlDbType.DateTime).Value = ExpirationDate;

                if (string.IsNullOrWhiteSpace(Notes))
                    command.Parameters.AddWithValue("@Notes", DBNull.Value);
                else
                    command.Parameters.Add("@Notes", SqlDbType.NVarChar, 500).Value = Notes;

                command.Parameters.Add("@PaidFees", SqlDbType.SmallMoney).Value = PaidFees;
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = IsActive;
                command.Parameters.Add("@IssueReason", SqlDbType.TinyInt).Value = IssueReason;

                command.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = CreatedByUserID;

                try
                {
                    connection.Open();

                    object result = command.ExecuteScalar();

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
    }
}
