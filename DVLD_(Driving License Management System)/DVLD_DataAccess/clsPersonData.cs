using DVLD_Common;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace DVLD_DataAccess
{
    public class clsPersonData
    {
        #region CRUD Methods

        /// <summary>
        /// Retrieves all persons from the database joined with their nationality country.
        /// Results are ordered alphabetically by first name.
        /// </summary>
        /// <returns>
        /// A <see cref="DataTable"/> containing all person records with columns:
        /// PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName,
        /// Gendor, GendorCaption, DateOfBirth, NationalityCountryID,
        /// CountryName, Phone, Email, ImagePath.
        /// Returns an empty <see cref="DataTable"/> if no records exist or an error occurs.
        /// </returns>
        /// <remarks>
        /// A warning is logged to the Event Log if query execution exceeds 5000ms.
        /// Errors are logged to the Windows Event Log via <see cref="clsLogEvent"/>.
        /// </remarks>
        public static DataTable GetAllPersons()
        {
            DataTable dt = new DataTable();

            const string query = @"SELECT   PersonID, NationalNo,
                                            FirstName, SecondName,
                                            ThirdName, LastName,
                                            Gendor,
                                            CASE
                                                WHEN Gendor = 0 THEN 'Male'
                                                ELSE 'Female'
                                            END AS GendorCaption,
                                            DateOfBirth, NationalityCountryID,
                                            C.CountryName, Phone,
                                            Email, ImagePath
                                   FROM People P
                                   INNER JOIN Countries C ON P.NationalityCountryID = C.CountryID
                                   ORDER BY P.FirstName;";

            var stopwatch = Stopwatch.StartNew();

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
                    stopwatch.Stop();

                    if(stopwatch.ElapsedMilliseconds > 5000)
                    {
                        clsLogEvent.LogEvent(
                            $"Slow query detected in GetAllPersons.\n"              +
                            $"Execution time : {stopwatch.ElapsedMilliseconds}ms\n" +
                            $"Records returned: {dt.Rows.Count}\n"                  +
                            $"Consider query optimization or indexing.",
                            EventLogEntryType.Warning);
                    }

                }
                catch (SqlException ex)
                {
                     clsLogEvent.LogEvent(
                        $"SQL Error in GetAllPersons\n"          +
                        $"Error Number  : {ex.Number}\n"         +
                        $"Error Message : {ex.Message}\n"        +
                        $"Line Number   : {ex.LineNumber}\n"     +
                        $"Server        : {conn.DataSource}\n"   +
                        $"Database      : {conn.Database}\n"     +
                        $"Stack Trace   : {ex.StackTrace}",
                        EventLogEntryType.Error);
                }
                catch (Exception ex) 
                {
                     clsLogEvent.LogEvent(
                        $"General Error in GetAllPersons\n"      +
                        $"Error Message : {ex.Message}\n"        +
                        $"Stack Trace   : {ex.StackTrace}",
                        EventLogEntryType.Error);
                }
            }

            // Return the filled DataTable
            return dt;
        }

        /// <summary>
        /// Retrieves complete person information from the database using their unique PersonID.
        /// </summary>
        /// <param name="PersonID">The unique identifier of the person to retrieve.</param>
        /// <param name="NationalNo">Output: The national identification number of the person.</param>
        /// <param name="FirstName">Output: The first name of the person.</param>
        /// <param name="SecondName">Output: The second name of the person.</param>
        /// <param name="ThirdName">Output: The third name. Returns empty string if null in DB.</param>
        /// <param name="LastName">Output: The last name of the person.</param>
        /// <param name="DateOfBirth">Output: The date of birth of the person.</param>
        /// <param name="Gendor">Output: Gender byte value. 0 = Male, 1 = Female.</param>
        /// <param name="Address">Output: The residential address of the person.</param>
        /// <param name="Phone">Output: The phone number of the person.</param>
        /// <param name="Email">Output: The email address. Returns empty string if null in DB.</param>
        /// <param name="NationalityCountryID">Output: The country ID of the person's nationality.</param>
        /// <param name="ImagePath">Output: File path of profile image. Returns empty string if null in DB.</param>
        /// <returns>
        /// <c>true</c> if the person was found and all output parameters populated successfully.
        /// <c>false</c> if the person was not found or a database error occurred.
        /// </returns>
        /// <remarks>
        /// Nullable database fields (ThirdName, Email, ImagePath) are safely handled
        /// and returned as empty strings rather than null.
        /// Errors are logged to the Windows Event Log via <see cref="clsLogEvent"/>.
        /// </remarks>
        public static bool GetPersonInfoByID(int PersonID, ref string NationalNo,
           ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName,
           ref DateTime DateOfBirth, ref byte Gendor, ref string Address,
           ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            const string query = "SELECT * FROM People WHERE PersonID = @PersonID";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@PersonID", PersonID);
                cmd.CommandTimeout = 30;

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            NationalNo           = (string)reader["NationalNo"];
                            FirstName            = (string)reader["FirstName"];
                            SecondName           = (string)reader["SecondName"];
                            ThirdName            = reader["ThirdName"]  != DBNull.Value ? (string)reader["ThirdName"]  : "";
                            LastName             = (string)reader["LastName"];
                            DateOfBirth          = (DateTime)reader["DateOfBirth"];
                            Gendor               = (byte)reader["Gendor"];
                            Address              = (string)reader["Address"];
                            Phone                = (string)reader["Phone"];
                            Email                = reader["Email"]      != DBNull.Value ? (string)reader["Email"]      : "";
                            NationalityCountryID = (int)reader["NationalityCountryID"];
                            ImagePath            = reader["ImagePath"]  != DBNull.Value ? (string)reader["ImagePath"]  : "";

                            // The record was found
                            isFound = true;
                        }
                    }          
                }
                catch (SqlException ex)
                {
                    isFound = false;
                    clsLogEvent.LogEvent(
                        $"SQL Error in GetPersonInfoByID\n" +
                        $"PersonID: {PersonID}\n"           +
                        $"Error Number: {ex.Number}\n"      +
                        $"Error Message: {ex.Message}\n"    +
                        $"Line Number: {ex.LineNumber}\n"   +
                        $"Server: {conn.DataSource}\n"      +
                        $"Database: {conn.Database}\n"      +
                        $"Stack Trace: {ex.StackTrace}",
                        EventLogEntryType.Error);
                }
                catch (Exception ex)
                {
                    isFound = false;
                    clsLogEvent.LogEvent(
                       $"General Error in GetPersonInfoByID\n" +
                       $"PersonID: {PersonID}\n"               +
                       $"Error Message: {ex.Message}\n"        +
                       $"Stack Trace: {ex.StackTrace}",
                       EventLogEntryType.Error);
                }
            }

            return isFound;
        }


        /// <summary>
        /// Retrieves complete person information from the database using their National Number.
        /// </summary>
        /// <param name="NationalNo">The national identification number to search for.</param>
        /// <param name="PersonID">Output: The unique database identifier of the person.</param>
        /// <param name="FirstName">Output: The first name of the person.</param>
        /// <param name="SecondName">Output: The second name of the person.</param>
        /// <param name="ThirdName">Output: The third name. Returns empty string if null in DB.</param>
        /// <param name="LastName">Output: The last name of the person.</param>
        /// <param name="DateOfBirth">Output: The date of birth of the person.</param>
        /// <param name="Gendor">Output: Gender byte value. 0 = Male, 1 = Female.</param>
        /// <param name="Address">Output: The residential address of the person.</param>
        /// <param name="Phone">Output: The phone number of the person.</param>
        /// <param name="Email">Output: The email address. Returns empty string if null in DB.</param>
        /// <param name="NationalityCountryID">Output: The country ID of the person's nationality.</param>
        /// <param name="ImagePath">Output: File path of profile image. Returns empty string if null in DB.</param>
        /// <returns>
        /// <c>true</c> if the person was found and all output parameters populated successfully.
        /// <c>false</c> if the person was not found or a database error occurred.
        /// </returns>
        /// <remarks>
        /// Nullable database fields (ThirdName, Email, ImagePath) are safely handled
        /// and returned as empty strings rather than null.
        /// Errors are logged to the Windows Event Log via <see cref="clsLogEvent"/>.
        /// </remarks>
        public static bool GetPersonInfoByNationalNo(string NationalNo, ref int PersonID, 
           ref string FirstName, ref string SecondName,ref string ThirdName, ref string LastName, 
           ref DateTime DateOfBirth,ref byte Gendor, ref string Address, ref string Phone,
           ref string Email,ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;
            
            const string query = "SELECT * FROM People WHERE NationalNo = @NationalNo";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@NationalNo", NationalNo);
                cmd.CommandTimeout = 30;

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            PersonID             = (int)reader["PersonID"];
                            FirstName            = (string)reader["FirstName"];
                            SecondName           = (string)reader["SecondName"];
                            ThirdName            = reader["ThirdName"]  != DBNull.Value ? (string)reader["ThirdName"]  : "";
                            LastName             = (string)reader["LastName"];
                            DateOfBirth          = (DateTime)reader["DateOfBirth"];
                            Gendor               = (byte)reader["Gendor"];
                            Address              = (string)reader["Address"];
                            Phone                = (string)reader["Phone"];
                            Email                = reader["Email"]      != DBNull.Value ? (string)reader["Email"]      : "";
                            NationalityCountryID = (int)reader["NationalityCountryID"];
                            ImagePath            = reader["ImagePath"]  != DBNull.Value ? (string)reader["ImagePath"]  : "";

                            // The record was found
                            isFound = true;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    isFound = false;
                    clsLogEvent.LogEvent(
                        $"SQL Error in GetPersonInfoByNationalNo\n"  +
                        $"Error Number  : {ex.Number}\n"             +
                        $"Error Message : {ex.Message}\n"            +
                        $"Line Number   : {ex.LineNumber}\n"         +
                        $"Server        : {conn.DataSource}\n"       +
                        $"Database      : {conn.Database}\n"         +
                        $"Stack Trace   : {ex.StackTrace}",
                        EventLogEntryType.Error);
                }
                catch (Exception ex)
                {
                    isFound = false;
                    clsLogEvent.LogEvent(
                        $"General Error in GetPersonInfoByNationalNo\n" +
                        $"Error Message : {ex.Message}\n"               +
                        $"Stack Trace   : {ex.StackTrace}",
                        EventLogEntryType.Error);
                }
            }

            return isFound;
        }

        /// <summary>
        /// Inserts a new person record into the database.
        /// </summary>
        /// <param name="NationalNo">The national identification number. Must be unique.</param>
        /// <param name="FirstName">The first name of the person.</param>
        /// <param name="SecondName">The second name of the person.</param>
        /// <param name="ThirdName">The third name. Pass empty string or null if not available.</param>
        /// <param name="LastName">The last name of the person.</param>
        /// <param name="DateOfBirth">The date of birth of the person.</param>
        /// <param name="Gendor">Gender byte value. 0 = Male, 1 = Female.</param>
        /// <param name="Address">The residential address of the person.</param>
        /// <param name="Phone">The phone number of the person.</param>
        /// <param name="Email">The email address. Pass empty string or null if not available.</param>
        /// <param name="NationalityCountryID">The country ID of the person's nationality.</param>
        /// <param name="ImagePath">File path of the profile image. Pass empty string or null if not available.</param>
        /// <returns>
        /// The newly generated PersonID if the insert was successful.
        /// Returns <c>-1</c> if the operation failed.
        /// </returns>
        /// <remarks>
        /// Uses SCOPE_IDENTITY() to retrieve the auto-generated PersonID after insert.
        /// Nullable fields (ThirdName, Email, ImagePath) are stored as NULL in the database
        /// when empty or whitespace values are provided.
        /// Successful inserts are logged to the Windows Event Log via <see cref="clsLogEvent"/>.
        /// </remarks>
        public static int AddNewPerson(string NationalNo, string FirstName,
            string SecondName, string ThirdName, string LastName,
            DateTime DateOfBirth,byte Gendor, string Address, string Phone,
            string Email, int NationalityCountryID, string ImagePath)
        {
            //this function will return the new person id if succeeded and -1 if not.
            int PersonID = -1;

            const string query = @"INSERT INTO People
                                        (NationalNo, FirstName, SecondName, ThirdName,
                                         LastName, DateOfBirth, Gendor, Address, Phone,
                                         Email, NationalityCountryID, ImagePath)
                                   VALUES
                                        (@NationalNo, @FirstName, @SecondName, @ThirdName,
                                         @LastName, @DateOfBirth, @Gendor, @Address, @Phone,
                                         @Email, @NationalityCountryID, @ImagePath);
                                   SELECT SCOPE_IDENTITY();";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using(SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@NationalNo",           NationalNo);
                cmd.Parameters.AddWithValue("@FirstName",            FirstName);
                cmd.Parameters.AddWithValue("@SecondName",           SecondName);
                cmd.Parameters.AddWithValue("@ThirdName",            string.IsNullOrWhiteSpace(ThirdName)  ? (object)DBNull.Value : ThirdName);
                cmd.Parameters.AddWithValue("@LastName",             LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth",          DateOfBirth);
                cmd.Parameters.AddWithValue("@Gendor",               Gendor);
                cmd.Parameters.AddWithValue("@Address",              Address);
                cmd.Parameters.AddWithValue("@Phone",                Phone);
                cmd.Parameters.AddWithValue("@Email",                string.IsNullOrWhiteSpace(Email)      ? (object)DBNull.Value : Email);
                cmd.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
                cmd.Parameters.AddWithValue("@ImagePath",            string.IsNullOrWhiteSpace(ImagePath)  ? (object)DBNull.Value : ImagePath);
                cmd.CommandTimeout = 30;

                try
                {
                    conn.Open();
                    object Result = cmd.ExecuteScalar();

                    if (Result != null && int.TryParse(Result.ToString(), out int insertedID))
                    {
                        PersonID = insertedID;

                         clsLogEvent.LogEvent(
                        $"AddNewPerson executed successfully.\n"    +
                        $"New PersonID  : {PersonID}\n",
                        EventLogEntryType.Information);
                    }
                }
                catch (SqlException ex)
                {
                    clsLogEvent.LogEvent(
                        $"SQL Error in AddNewPerson\n"              +
                        $"Error Number  : {ex.Number}\n"            +
                        $"Error Message : {ex.Message}\n"           +
                        $"Line Number   : {ex.LineNumber}\n"        +
                        $"Server        : {conn.DataSource}\n"      +
                        $"Database      : {conn.Database}\n"        +
                        $"Stack Trace   : {ex.StackTrace}",
                        EventLogEntryType.Error);
                }
                catch (Exception ex)
                {
                    clsLogEvent.LogEvent(
                        $"General Error in AddNewPerson\n"          +
                        $"Error Message : {ex.Message}\n"           +
                        $"Stack Trace   : {ex.StackTrace}",
                        EventLogEntryType.Error);
                }
            }        

            return PersonID;
        }


        /// <summary>
        /// Updates an existing person record in the database.
        /// </summary>
        /// <param name="PersonID">The unique identifier of the person to update.</param>
        /// <param name="NationalNo">The updated national identification number.</param>
        /// <param name="FirstName">The updated first name.</param>
        /// <param name="SecondName">The updated second name.</param>
        /// <param name="ThirdName">The updated third name. Pass empty string or null to store NULL.</param>
        /// <param name="LastName">The updated last name.</param>
        /// <param name="DateOfBirth">The updated date of birth.</param>
        /// <param name="Gendor">Updated gender byte. 0 = Male, 1 = Female.</param>
        /// <param name="Address">The updated residential address.</param>
        /// <param name="Phone">The updated phone number.</param>
        /// <param name="Email">The updated email. Pass empty string or null to store NULL.</param>
        /// <param name="NationalityCountryID">The updated country ID of nationality.</param>
        /// <param name="ImagePath">The updated image file path. Pass empty string or null to store NULL.</param>
        /// <returns>
        /// <c>true</c> if the record was updated successfully (at least one row affected).
        /// <c>false</c> if no rows were updated or a database error occurred.
        /// </returns>
        /// <remarks>
        /// Nullable fields (ThirdName, Email, ImagePath) are stored as NULL in the database
        /// when empty or whitespace values are provided.
        /// Successful updates are logged to the Windows Event Log via <see cref="clsLogEvent"/>.
        /// </remarks>
        public static bool UpdatePerson(int PersonID, string NationalNo,
           string FirstName,string SecondName, string ThirdName, string LastName, 
           DateTime DateOfBirth,byte Gendor, string Address, string Phone, 
           string Email,int NationalityCountryID, string ImagePath)
        {
            int rowsAffected = 0;

            const string query = @"UPDATE People
                                   SET NationalNo           = @NationalNo,
                                       FirstName            = @FirstName,
                                       SecondName           = @SecondName,
                                       ThirdName            = @ThirdName,
                                       LastName             = @LastName,
                                       DateOfBirth          = @DateOfBirth,
                                       Gendor               = @Gendor,
                                       Address              = @Address,
                                       Phone                = @Phone,
                                       Email                = @Email,
                                       NationalityCountryID = @NationalityCountryID,
                                       ImagePath            = @ImagePath
                                   WHERE PersonID = @PersonID";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@PersonID",             PersonID);
                cmd.Parameters.AddWithValue("@NationalNo",           NationalNo);
                cmd.Parameters.AddWithValue("@FirstName",            FirstName);
                cmd.Parameters.AddWithValue("@SecondName",           SecondName);
                cmd.Parameters.AddWithValue("@ThirdName",            string.IsNullOrWhiteSpace(ThirdName)  ? (object)DBNull.Value : ThirdName);
                cmd.Parameters.AddWithValue("@LastName",             LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth",          DateOfBirth);
                cmd.Parameters.AddWithValue("@Gendor",               Gendor);
                cmd.Parameters.AddWithValue("@Address",              Address);
                cmd.Parameters.AddWithValue("@Phone",                Phone);
                cmd.Parameters.AddWithValue("@Email",                string.IsNullOrWhiteSpace(Email)      ? (object)DBNull.Value : Email);
                cmd.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
                cmd.Parameters.AddWithValue("@ImagePath",            string.IsNullOrWhiteSpace(ImagePath)  ? (object)DBNull.Value : ImagePath);
                cmd.CommandTimeout = 30;

                try
                {
                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        clsLogEvent.LogEvent(
                            $"UpdatePerson executed successfully.\n" +
                            $"PersonID : {PersonID}\n",
                            EventLogEntryType.Information);
                    }
                }
                catch (SqlException ex)
                {
                    clsLogEvent.LogEvent(
                        $"SQL Error in UpdatePerson\n"              +
                        $"PersonID      : {PersonID}\n"             +
                        $"Error Number  : {ex.Number}\n"            +
                        $"Error Message : {ex.Message}\n"           +
                        $"Line Number   : {ex.LineNumber}\n"        +
                        $"Server        : {conn.DataSource}\n"      +
                        $"Database      : {conn.Database}\n"        +
                        $"Stack Trace   : {ex.StackTrace}",
                        EventLogEntryType.Error);
                }
                catch (Exception ex)
                {
                    clsLogEvent.LogEvent(
                        $"General Error in UpdatePerson\n"          +
                        $"PersonID      : {PersonID}\n"             +
                        $"Error Message : {ex.Message}\n"           +
                        $"Stack Trace   : {ex.StackTrace}",
                        EventLogEntryType.Error);
                }
            }
            
            return (rowsAffected > 0);
        }

        /// <summary>
        /// Permanently deletes a person record from the database using their PersonID.
        /// </summary>
        /// <param name="PersonID">The unique identifier of the person to delete.</param>
        /// <returns>
        /// <c>true</c> if the record was deleted successfully (at least one row affected).
        /// <c>false</c> if no rows were deleted or a database error occurred.
        /// </returns>
        /// <remarks>
        /// WARNING: This operation is irreversible. Ensure no dependent foreign key
        /// records exist (e.g., licenses, applications) before calling this method,
        /// otherwise a SqlException will be thrown due to referential integrity constraints.
        /// Delete operations are always logged to the Windows Event Log via <see cref="clsLogEvent"/>.
        /// </remarks>
        public static bool DeletePerson(int PersonID)
        {
            int rowsAffected = 0;
            const string query = "DELETE FROM People WHERE PersonID = @PersonID";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@PersonID", PersonID);
                cmd.CommandTimeout = 30;

                try
                {
                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        clsLogEvent.LogEvent(
                            $"DeletePerson executed successfully.\n" +
                            $"PersonID      : {PersonID}\n",
                            EventLogEntryType.Information);
                    }
                }
                catch (SqlException ex)
                {
                    clsLogEvent.LogEvent(
                        $"SQL Error in DeletePerson\n"              +
                        $"PersonID      : {PersonID}\n"             +
                        $"Error Number  : {ex.Number}\n"            +
                        $"Error Message : {ex.Message}\n"           +
                        $"Line Number   : {ex.LineNumber}\n"        +
                        $"Server        : {conn.DataSource}\n"      +
                        $"Database      : {conn.Database}\n"        +
                        $"Stack Trace   : {ex.StackTrace}",
                        EventLogEntryType.Error);
                }
                catch (Exception ex)
                {
                    clsLogEvent.LogEvent(
                        $"General Error in DeletePerson\n"          +
                        $"PersonID      : {PersonID}\n"             +
                        $"Error Message : {ex.Message}\n"           +
                        $"Stack Trace   : {ex.StackTrace}",
                        EventLogEntryType.Error);
                }
            }

            return (rowsAffected > 0);
        }

        #endregion

        #region Exist Methods

        /// <summary>
        /// Checks whether a person record exists in the database using their PersonID.
        /// </summary>
        /// <param name="PersonID">The unique identifier of the person to check.</param>
        /// <returns>
        /// <c>true</c> if a person with the specified PersonID exists in the database.
        /// <c>false</c> if no matching record was found or a database error occurred.
        /// </returns>
        /// <remarks>
        /// Uses an optimized query (SELECT Found=1) to avoid loading unnecessary column data.
        /// Errors are logged to the Windows Event Log via <see cref="clsLogEvent"/>.
        /// </remarks>
        public static bool IsPersonExist(int PersonID)
        {
            bool isFound = false;
            const string query = "SELECT Found=1 FROM People WHERE PersonID = @PersonID";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonID;
                cmd.CommandTimeout = 30;

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        isFound = reader.HasRows;
                    }
                }
                catch (SqlException ex)
                {
                    isFound = false;
                    clsLogEvent.LogEvent(
                        $"SQL Error in IsPersonExist (by ID)\n" +
                        $"PersonID: {PersonID}\n" +
                        $"Error Number: {ex.Number}\n" +
                        $"Error Message: {ex.Message}\n" +
                        $"Line Number: {ex.LineNumber}\n" +
                        $"Server: {conn.DataSource}\n" +
                        $"Database: {conn.Database}\n" +
                        $"Stack Trace: {ex.StackTrace}",
                        EventLogEntryType.Error);
                }
                catch (Exception ex)
                {
                    isFound = false;
                    clsLogEvent.LogEvent(
                        $"General Error in IsPersonExist (by ID)\n" +
                        $"PersonID: {PersonID}\n" +
                        $"Error Message: {ex.Message}\n" +
                        $"Stack Trace: {ex.StackTrace}",
                        EventLogEntryType.Error);
                }
            }
            
            return isFound;
        }

        /// <summary>
        /// Checks whether a person record exists in the database using their National Number.
        /// </summary>
        /// <param name="NationalNo">The national identification number to search for.</param>
        /// <returns>
        /// <c>true</c> if a person with the specified NationalNo exists in the database.
        /// <c>false</c> if no matching record was found or a database error occurred.
        /// </returns>
        /// <remarks>
        /// Uses an optimized query (SELECT Found=1) to avoid loading unnecessary column data.
        /// Typically used for duplicate NationalNo validation before insert operations.
        /// Errors are logged to the Windows Event Log via <see cref="clsLogEvent"/>.
        /// </remarks>
        public static bool IsPersonExist(string NationalNo)
        {
            bool isFound = false;
            const string query = "SELECT Found=1 FROM People WHERE NationalNo = @NationalNo";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@NationalNo", NationalNo);
                cmd.CommandTimeout = 30;
                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        isFound = reader.HasRows;
                    }
                }
                catch (SqlException ex)
                {
                    isFound = false;
                    clsLogEvent.LogEvent(
                        $"SQL Error in IsPersonExist (by NationalNo)\n" +
                        $"Error Number: {ex.Number}\n" +
                        $"Error Message: {ex.Message}\n" +
                        $"Line Number: {ex.LineNumber}\n" +
                        $"Server: {conn.DataSource}\n" +
                        $"Database: {conn.Database}\n" +
                        $"Stack Trace: {ex.StackTrace}",
                        EventLogEntryType.Error);
                }
                catch (Exception ex)
                {
                    isFound = false;
                    clsLogEvent.LogEvent(
                        $"General Error in IsPersonExist (by NationalNo)\n" +
                        $"Error Message: {ex.Message}\n" +
                        $"Stack Trace: {ex.StackTrace}",
                        EventLogEntryType.Error);
                }
            }

            return isFound;
        }

        #endregion
    }
}
