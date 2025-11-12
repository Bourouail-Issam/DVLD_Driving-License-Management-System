using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsTestTypeData
    {
        //################################ CRUD Methods ################################

        public static DataTable GetAllTestTypes()
        {

            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM TestTypes order by TestTypeID";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandTimeout = 30;

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

        public static bool GetTestTypeInfoByID(int TestTypeID,
           ref string TestTypeTitle, ref string TestDescription, ref float TestFees)
        {
            bool isFound = false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM TestTypes WHERE TestTypeID = @TestTypeID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add("@TestTypeID", SqlDbType.Int).Value = TestTypeID;
            cmd.CommandTimeout=30;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    TestTypeTitle = (string)reader["TestTypeTitle"];
                    TestDescription = (string)reader["TestTypeDescription"];
                    TestFees = Convert.ToSingle(reader["TestTypeFees"]);

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

        public static bool UpdateTestType(int TestTypeID, string Title, string Description, decimal Fees)
        {

            int rowsAffected = 0;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  TestTypes  
                            set TestTypeTitle = @TestTypeTitle,
                                TestTypeDescription=@TestTypeDescription,
                                TestTypeFees = @TestTypeFees
                                where TestTypeID = @TestTypeID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add("@TestTypeID", SqlDbType.Int).Value = TestTypeID;
            cmd.Parameters.Add("@TestTypeTitle",SqlDbType.NVarChar,100).Value = Title;
            cmd.Parameters.Add("@TestTypeDescription", SqlDbType.NVarChar, 500).Value = Description;
            cmd.Parameters.Add("@TestTypeFees", SqlDbType.SmallMoney).Value = Fees;
            cmd.CommandTimeout = 30;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                conn.Close();
            }

            return (rowsAffected > 0);
        }
    }
}
