using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsTestData
    {
        

        // ###################   Other Methods   ###################
        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            string query = @"SELECT COUNT(TestTypeID)
                             FROM Tests 
                             INNER JOIN TestAppointments 
                                  ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
						     WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID 
                             AND TestResult = 1";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@LocalDrivingLicenseApplicationID", SqlDbType.Int).Value = 
                    LocalDrivingLicenseApplicationID;
                try
                {
                    conn.Open();

                    object result = cmd.ExecuteScalar();

                    if (result != null && byte.TryParse(result.ToString(), out byte ptCount))
                        return ptCount;    
                }

                catch (Exception ex)
                {
                    //Console.WriteLine("Error: " + ex.Message);
                    return 0;
                }
            }
            return 0;
        }
    }
}
