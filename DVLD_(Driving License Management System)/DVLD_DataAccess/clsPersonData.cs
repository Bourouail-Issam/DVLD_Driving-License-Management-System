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
    }
}
