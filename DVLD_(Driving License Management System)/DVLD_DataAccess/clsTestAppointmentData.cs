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
