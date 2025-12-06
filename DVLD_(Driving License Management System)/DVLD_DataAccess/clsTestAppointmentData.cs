using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsTestAppointmentData
    {
        // ###################   CRUD Methods   ###################

        public static bool GetTestAppointmentInfoByID(
            int TestAppointmentID,ref int testTypeID, ref int localDrivingLicenseApplicationID,
            ref DateTime appointmentDate, ref Decimal paidFees, ref int createdByUserID,
            ref bool isLocked, ref int retakeTestApplicationID)
        {
            string query = @"SELECT TestTypeID,
                             	   LocalDrivingLicenseApplicationID,
                             	   AppointmentDate,
                             	   PaidFees,
                             	   CreatedByUserID,
                             	   IsLocked,
                             	   RetakeTestApplicationID
                             FROM TestAppointments
                             WHERE TestAppointmentID = @TestAppointmentID;";


            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@TestAppointmentID", SqlDbType.Int).Value  = TestAppointmentID;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            testTypeID = reader.GetInt32(0);
                            localDrivingLicenseApplicationID = reader.GetInt32(1);
                            appointmentDate = reader.GetDateTime(2);
                            paidFees = reader.GetDecimal(3);
                            createdByUserID = reader.GetInt32(4);
                            isLocked = reader.GetBoolean(5);

                            retakeTestApplicationID = reader.IsDBNull(6)
                                ? -1
                                : reader.GetInt32(6);

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


        // ###################   Other Methods   ###################
        public static int GetTestID(int TestAppointmentID)
        {
            string query = @"SELECT TestID FROM Tests 
                             WHERE TestAppointmentID=@TestAppointmentID;";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {

                command.Parameters.Add("@TestAppointmentID", SqlDbType.Int).Value = TestAppointmentID;

                try
                {
                    connection.Open();

                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        return insertedID;
                }
                catch (Exception ex)
                {
                    //Console.WriteLine("Error: " + ex.Message);
                    return -1;
                }
            }
            return -1;
        }
    }
}
