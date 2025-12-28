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

        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {

            DataTable dt = new DataTable();

            string query = @"SELECT TestAppointmentID, AppointmentDate,PaidFees, IsLocked
                             FROM TestAppointments
                             WHERE  
                             (TestTypeID = @TestTypeID) 
                             AND (LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)
                             ORDER BY TestAppointmentID DESC;";


            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
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

        public static int AddNewTestAppointment(
            int TestTypeID, int LocalDrivingLicenseApplicationID,
            DateTime AppointmentDate, float PaidFees, int CreatedByUserID, int RetakeTestApplicationID)
        {
            int TestAppointmentID = -1;
            const string query = @"INSERT INTO TestAppointments 
                                    (TestTypeID,
                                     LocalDrivingLicenseApplicationID,
                                     AppointmentDate,
                                     PaidFees,
                                     CreatedByUserID,
                                     IsLocked,
                                     RetakeTestApplicationID)
                   VALUES (@TestTypeID,
                           @LocalDrivingLicenseApplicationID,
                           @AppointmentDate,
                           @PaidFees,
                           @CreatedByUserID,
                           0,
                           @RetakeTestApplicationID);
                   SELECT SCOPE_IDENTITY();";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                cmd.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
                cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
                cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                if (RetakeTestApplicationID == -1)
                    cmd.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        TestAppointmentID = insertedID;
                    }
                }
                catch (Exception ex)
                {
                    //Console.WriteLine("Error: " + ex.Message);
                }
            } 

            return TestAppointmentID;
        }

        public static bool UpdateTestAppointment(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID,
            DateTime AppointmentDate, float PaidFees,
            int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {
            int rowsAffected = 0;

            string query = @"Update  TestAppointments  
                               SET TestTypeID = @TestTypeID,
                                   LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID,
                                   AppointmentDate = @AppointmentDate,
                                   PaidFees = @PaidFees,
                                   CreatedByUserID = @CreatedByUserID,
                                   IsLocked=@IsLocked,
                                   RetakeTestApplicationID=@RetakeTestApplicationID
                             WHERE TestAppointmentID = @TestAppointmentID";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                cmd.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
                cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
                cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                cmd.Parameters.AddWithValue("@IsLocked", IsLocked);
                if (RetakeTestApplicationID == -1)
                    cmd.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);

                try
                {
                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    //Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
        }

    }
}
