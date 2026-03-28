using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using DVLD_Common;
using System.Diagnostics;


namespace DVLD_DataAccess
{
    public class clsUserData
    {
        //################################ CRUD Methods ################################
        public static bool GetUserInfoByUsername(
            string UserName, ref string Password,
            ref int UserID, ref int PersonID, ref bool IsActive
            )
        {
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Users WHERE Username = @Username;";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 20).Value = UserName;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {            
                    UserID = (int)reader["UserID"];
                    PersonID = (int)reader["PersonID"];
                    IsActive = (bool)reader["IsActive"];
                    Password = (string)reader["Password"];

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
                conn.Close();
            }

            return isFound;
        }

        public static bool GetUserInfoByUserID(
            ref string UserName, ref string Password,
            int UserID, ref int PersonID, ref bool IsActive)
        {
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Users WHERE UserID = @UserID";


            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;


            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    PersonID = (int)reader["PersonID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];

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
                conn.Close();
            }

            return isFound;
        }

        public static bool GetUserInfoByPersonID(int PersonID, ref int UserID, ref string UserName,
         ref string Password, ref bool IsActive)
        {
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Users WHERE PersonID = @PersonID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonID;
            cmd.CommandTimeout = 30;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    UserID = (int)reader["UserID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];

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
                conn.Close();
            }

            return isFound;
        }


        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"
                             SELECT 
                                U.UserID, 
                                U.PersonID, 
                             
                                (P.FirstName + ' ' + 
                                 P.SecondName + ' ' + 
                                 ISNULL(P.ThirdName, '') + ' ' + 
                                 P.LastName
                             	 ) AS FullName, 
                             
                                U.UserName, 
                                U.IsActive
                             FROM 
                                Users U INNER JOIN  People P
                                ON U.PersonID = P.PersonID";

            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                    dt.Load(reader);
        
                reader.Close();
            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }


        public static int AddNewUser(int PersonID, string UserName,
             string Password, bool IsActive)
        {
            //this function will return the new person id if succeeded and -1 if not.
            int UserID = -1;

            SqlConnection coonn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Users (PersonID,UserName,Password,IsActive)
                             VALUES (@PersonID, @UserName,@Password,@IsActive);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, coonn);

            cmd.Parameters.AddWithValue("@PersonID", PersonID);
            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.CommandTimeout = 30;
            try
            {
                coonn.Open();

                object result = cmd.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    UserID = insertedID;

            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                coonn.Close();
            }

            return UserID;
        }

        public static bool DeleteUser(int UserID)
        {

            int rowsAffected = 0;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete Users 
                                where UserID = @UserID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.CommandTimeout=30;
            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return (rowsAffected > 0);

        }

        public static bool UpdateUser(int UserID, int PersonID, string UserName,
             string Password, bool IsActive)
        {

            int rowsAffected = 0;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  Users  
                            set
                                PersonID = @PersonID,
                                UserName = @UserName,
                                Password = @Password,
                                IsActive = @IsActive
                                where UserID = @UserID";

            SqlCommand cmf = new SqlCommand(query, conn);

            cmf.Parameters.AddWithValue("@UserName", UserName);
            cmf.Parameters.AddWithValue("@Password", Password);
            cmf.Parameters.AddWithValue("@IsActive", IsActive);
            cmf.Parameters.AddWithValue("@UserID", UserID);


            try
            {
                conn.Open();
                rowsAffected = cmf.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
            }

            finally
            {
                conn.Close();
            }

            return (rowsAffected > 0);
        }

        
        public static string GetstoredHashByUsername(string Username)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("GetStoredPasswordHashByID",conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", Username);

                try
                {
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            if (dr["Password"] == DBNull.Value)
                            {
                                return null;
                            }

                            string HashStorePassword = dr["Password"].ToString();
                                                           
                            if (string.IsNullOrEmpty(HashStorePassword))
                            {
                                return null;
                            }
                            return HashStorePassword;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                catch(SqlException sqlEx)
                {
                    clsLogEvent.LogEvent(
                        $"SQL Error in GetStoredPasswordHashByID Procedure \n"  +
                        $"Error Number: {sqlEx.Number}\n"                       +
                        $"Error Message: {sqlEx.Message}\n"                     +
                        $"Line Number: {sqlEx.LineNumber}\n"                    +
                        $"Server: {conn.DataSource}\n"                          +
                        $"Database: {conn.Database}\n"                          +
                        $"Stack Trace: {sqlEx.StackTrace}",
                        EventLogEntryType.Error
                        );
                    return null;
                }
                catch (Exception ex)
                {
                    clsLogEvent.LogEvent(
                       $"General Error in GetStoredPasswordHashByID\n" +
                       $"Error Message: {ex.Message}\n" +
                       $"Stack Trace: {ex.StackTrace}",
                       EventLogEntryType.Error);
                    return null;
                }
            }
        }

        //################################ Exist Methods ################################
        public static bool IsUserExistForPersonID(int PersonID)
        {
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Users WHERE PersonID = @PersonID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@PersonID", PersonID);
            cmd.CommandTimeout = 30;
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

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
                conn.Close();
            }

            return isFound;
        }

        public static bool IsUserExist(string UserName)
        {
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Users WHERE UserName = @UserName";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.CommandTimeout= 30; 

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

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
                conn.Close();
            }

            return isFound;
        }
    }
}
