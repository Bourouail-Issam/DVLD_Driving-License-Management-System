using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsPersonData
    {
        public static DataTable GetAllPersons()
        {
            DataTable dt = new DataTable();

            string query = @"select   PersonID ,NationalNo ,
                                      FirstName ,SecondName ,
                                      ThirdName ,LastName ,
                                      Gendor,
                                      Case
                                      	when Gendor = 0 Then 'Male'
                                      	else 'Female'
                                      End as GendorCaption ,
                                      DateOfBirth ,NationalityCountryID,
                                      C.CountryName,Phone,
                                      Email,ImagePath
                              
                                 From People P inner join Countries C 
                                     		on P.NationalityCountryID = C.CountryID 
                                 Order by P.FirstName;";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                // ⏱ Set the maximum time (in seconds) to execute the SQL command
                cmd.CommandTimeout = 30;

                try
                {
                    // Open database connection
                    conn.Open();

                    // Execute the query and read the result
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        // Load data into the DataTable if rows exist
                        if (rd.HasRows)
                            dt.Load(rd);
                    }
                }        
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                }
            }

            // Return the filled DataTable
            return dt;
        }

        public static bool GetPersonInfoByID(int PersonID, ref string NationalNo, 
            ref string FirstName, ref string SecondName,ref string ThirdName, ref string LastName, 
            ref DateTime DateOfBirth, ref byte Gendor, ref string Address, 
            ref string Phone, ref string Email,ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    NationalNo = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];

                    //ThirdName: allows null in database so we should handle null
                    if (reader["ThirdName"] != DBNull.Value)
                        ThirdName = (string)reader["ThirdName"];        
                    else
                        ThirdName = "";

                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (byte)reader["Gendor"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];


                    //Email: allows null in database so we should handle null
                    if (reader["Email"] != DBNull.Value)
                        Email = (string)reader["Email"];
                    else
                        Email = "";

                    NationalityCountryID = (int)reader["NationalityCountryID"];

                    //ImagePath: allows null in database so we should handle null
                    if (reader["ImagePath"] != DBNull.Value)
                        ImagePath = (string)reader["ImagePath"];
                    else
                        ImagePath = "";

                    // The record was found
                    isFound = true;

                }

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
    }
}
