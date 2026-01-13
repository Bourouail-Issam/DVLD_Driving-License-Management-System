using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsDriverData
    {
        public static bool GetDriverInfoByDriverID(int DriverID,
           ref int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool isFound = false;

            string query = @"SELECT PersonID,CreatedByUserID,CreatedDate
                                FROM Drivers 
                            WHERE DriverID = @DriverID";
            
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@DriverID", SqlDbType.Int).Value = DriverID;

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            PersonID = (int)reader["PersonID"];
                            CreatedByUserID = (int)reader["CreatedByUserID"];
                            CreatedDate = (DateTime)reader["CreatedDate"];

                            // The record was found
                            isFound = true;
                        }
                    }         
                }
                catch (SqlException sqlEx)
                {
                    isFound = false;
                }
                catch (Exception ex)
                {
                    //Console.WriteLine("Error: " + ex.Message);
                    isFound = false;
                }
            }

            return isFound;
        }

        public static bool GetDriverInfoByPersonID(int PersonID, ref int DriverID,
            ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool isFound = false;

            string query = @"SELECT DriverID,CreatedByUserID,CreatedDate
                                FROM Drivers 
                             WHERE PersonID = @PersonID";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonID;

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DriverID = (int)reader["DriverID"];
                            CreatedByUserID = (int)reader["CreatedByUserID"];
                            CreatedDate = (DateTime)reader["CreatedDate"];

                            // The record was found
                            isFound = true;
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    isFound = false;
                }
                catch (Exception ex)
                {
                    //Console.WriteLine("Error: " + ex.Message);
                    isFound = false;
                }
            }

            return isFound;
        }

        public static int AddNewDriver(int PersonID, int CreatedByUserID)
        {
            int DriverID = -1;

            const string query = @"Insert Into Drivers (PersonID,CreatedByUserID,CreatedDate)
                                   Values (@PersonID,@CreatedByUserID,@CreatedDate);          
                                   SELECT SCOPE_IDENTITY();";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@PersonID", PersonID);
                cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                try
                {
                    conn.Open();

                    object result = cmd.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        DriverID = insertedID;
                }
                catch (Exception ex)
                {
                    //Console.WriteLine("Error: " + ex.Message);
                }
            }
            return DriverID;
        }

        public static DataTable GetAllDrivers()
        {
            DataTable dt = new DataTable();

            const string query = "SELECT * FROM Drivers_View order by FullName";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
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
    }
}
