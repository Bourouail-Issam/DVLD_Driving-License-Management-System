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



        //################################ CRUD Methods ################################

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
           ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName,
           ref DateTime DateOfBirth, ref byte Gendor, ref string Address,
           ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
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


        public static int AddNewPerson(string NationalNo, string FirstName,
            string SecondName, string ThirdName, string LastName,
            DateTime DateOfBirth,byte Gendor, string Address, string Phone,
            string Email, int NationalityCountryID, string ImagePath)
        {
            //this function will return the new person id if succeeded and -1 if not.
            int PersonID = -1;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"insert into People(NationalNo,FirstName,SecondName,ThirdName,
                                                LastName,DateOfBirth,Gendor,Address,Phone,Email,
                                                NationalityCountryID,ImagePath)

                                     Values(@NationalNo,@FirstName,@SecondName,@ThirdName,@LastName,
                                            @DateOfBirth,@Gendor,@Address,@Phone,@Email,
                                            @NationalityCountryID,@ImagePath)
                                       SELECT SCOPE_IDENTITY()";


            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@NationalNo", NationalNo);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@SecondName", SecondName);

            if (!string.IsNullOrWhiteSpace(ThirdName))
                cmd.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                cmd.Parameters.AddWithValue("@ThirdName", DBNull.Value);

            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            cmd.Parameters.AddWithValue("@Gendor", Gendor);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@Phone", Phone);

            if (!string.IsNullOrWhiteSpace(Email))
                cmd.Parameters.AddWithValue("@Email", Email);
            else
                cmd.Parameters.AddWithValue("@Email", DBNull.Value);

            cmd.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (!string.IsNullOrWhiteSpace(ImagePath))
                cmd.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                cmd.Parameters.AddWithValue("@ImagePath", DBNull.Value);

            try
            {
                conn.Open();
                object Result = cmd.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int insertedID))
                    PersonID = insertedID;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();
            }

            return PersonID;
        }


        public static bool UpdatePerson(int PersonID, string NationalNo,
           string FirstName,string SecondName, string ThirdName, string LastName, 
           DateTime DateOfBirth,byte Gendor, string Address, string Phone, 
           string Email,int NationalityCountryID, string ImagePath)
        {
            int rowsAffected = 0;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"update People
                                   set NationalNo=@NationalNo,
                                       FirstName=@FirstName,
                                       SecondName=@SecondName,
                                       ThirdName=@ThirdName,
                                       LastName=@LastName,
                                       DateOfBirth=@DateOfBirth,
                                       Gendor=@Gendor,
                                       Address=@Address,
                                       Phone=@Phone,
                                       Email=@Email,
                                       NationalityCountryID=@NationalityCountryID,
                                       ImagePath=@ImagePath
                                    where PersonID=@PersonID";


            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@PersonID", PersonID);
            cmd.Parameters.AddWithValue("@NationalNo", NationalNo);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@SecondName", SecondName);

            if (String.IsNullOrWhiteSpace(ThirdName))
                cmd.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                cmd.Parameters.AddWithValue("@ThirdName", DBNull.Value);

            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            cmd.Parameters.AddWithValue("@Gendor", Gendor);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@Phone", Phone);

            if (!String.IsNullOrWhiteSpace(Email))
                cmd.Parameters.AddWithValue("@Email", Email);
            else
                cmd.Parameters.AddWithValue("@Email", DBNull.Value);

            cmd.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (!String.IsNullOrWhiteSpace(ImagePath))
                cmd.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                cmd.Parameters.AddWithValue("@ImagePath", DBNull.Value);

            cmd.CommandTimeout = 30;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return (rowsAffected > 0);
        }


        public static bool DeletePerson(int PersonID)
        {
            int rowsAffected = 0;
            SqlConnection conn = new SqlConnection(DataAccessSettings.stringConnection);
            string query = "delete from People where PersonID=@PersonID";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@PersonID", PersonID);
            cmd.CommandTimeout = 30;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return (rowsAffected > 0);
        }


        // ############################## Exist Methods ##############################

        public static bool IsPersonExist(string NationalNo)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM People WHERE NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

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
