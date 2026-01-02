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
        // ###################   CURD Methods   ###################
        public static bool GetTestInfoByID(int testID,
           ref int testAppointmentId, ref bool testResult,
           ref string notes, ref int createdByUserId)
        {
            const string query = @"
                 SELECT TestAppointmentID, TestResult, Notes, CreatedByUserID
                 FROM Tests 
                 WHERE TestID = @TestID";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn)) 
            {
                cmd.Parameters.Add("@TestID", SqlDbType.Int).Value = testID;
                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            testAppointmentId = reader.GetInt32(0);
                            testResult = reader.GetBoolean(1);
                            notes = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                            createdByUserId = reader.GetInt32(3);

                            // The record was found
                            return  true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
            return false;
        }


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

        public static bool GetLastTestByPersonAndTestTypeAndLicenseClass
          ( int LocalDrivingLicenseApplicationID, int PersonID, int LicenseClassID, 
            int TestTypeID, ref int TestID,
            ref int TestAppointmentID, ref bool TestResult,
            ref string Notes, ref int CreatedByUserID)
        {
            const string query = @"
                 SELECT TOP 1 
                     T.TestID, 
                     T.TestAppointmentID, 
                     T.TestResult, 
                     T.Notes, 
                     T.CreatedByUserID
                 FROM Tests T
                 INNER JOIN TestAppointments TA 
                     ON T.TestAppointmentID = TA.TestAppointmentID
                 INNER JOIN LocalDrivingLicenseApplications LDLA 
                     ON TA.LocalDrivingLicenseApplicationID = LDLA.LocalDrivingLicenseApplicationID
                 INNER JOIN Applications A 
                     ON LDLA.ApplicationID = A.ApplicationID
                 WHERE A.ApplicantPersonID = @PersonID 
                   AND LDLA.LicenseClassID = @LicenseClassID
                   AND TA.TestTypeID = @TestTypeID
                   AND LDLA.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                 ORDER BY T.TestID DESC";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@PersonID", PersonID);
                cmd.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            TestID = reader.GetInt32(0);
                            TestAppointmentID = reader.GetInt32(1);
                            TestResult = reader.GetBoolean(2);
                            Notes = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                            CreatedByUserID = reader.GetInt32(4);

                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
            return false;
        }

    }
}
