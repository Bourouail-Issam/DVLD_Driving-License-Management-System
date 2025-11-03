using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;


namespace DVLD_DataAccess
{
    public class clsUserData
    {
        //################################ CRUD Methods ################################
        public static bool GetUserInfoByUsernameAndPassword(
            string UserName, string Password,
            ref int UserID, ref int PersonID, ref bool IsActive
            )
        {
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Users WHERE Username = @Username and Password=@Password;";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 20).Value = UserName;
            cmd.Parameters.Add("@Password", SqlDbType.NVarChar,20).Value = Password;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {            
                    UserID = (int)reader["UserID"];
                    PersonID = (int)reader["PersonID"];
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
    }
}
