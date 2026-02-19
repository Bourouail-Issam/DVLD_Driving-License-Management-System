using DVLD_BuisnessDVLD_Buisness;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;


namespace DVLD__Driving_License_Management_System_.Global_Classes
{
    public class clsGlobal
    {
        public static clsUser CurrentUser;

        // Registry configuration constants
        private const string REGISTRY_KEY_PATH = @"SOFTWARE\DVLD_License";
        private const string USERNAME_VALUE_NAME = "Username";
        private const string PASSWORD_VALUE_NAME = "Password";

        #region Registry Methods (Recommended)

        /// <summary>
        /// Saves user credentials to Windows Registry with encrypted password
        /// </summary>
        /// <param name="Username">Username to save (stored as plain text)</param>
        /// <param name="Password">Password to save (will be encrypted)</param>
        /// <returns>True if successful, false otherwise</returns>
        public static bool SetStoredCredentialFromRegistry(string Username, string Password)
        {
            try
            {
                // If username is empty, delete stored credentials
                if (string.IsNullOrEmpty(Username))
                {
                    try
                    {
                        Registry.CurrentUser.DeleteSubKeyTree(REGISTRY_KEY_PATH, false);
                    }
                    catch
                    {
                        // Key doesn't exist - that's fine
                    }
                    return true;
                }

                // Create or open registry key
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(REGISTRY_KEY_PATH))
                {
                    if (key == null)
                    {
                        return false;
                    }

                    // Store username as plain text (not sensitive)
                    key.SetValue(USERNAME_VALUE_NAME, Username, RegistryValueKind.String);

                    // Encrypt and store password
                    string encryptedPassword = EncryptPassword(Password);
                    if (encryptedPassword == null)
                    {
                        return false;
                    }

                    key.SetValue(PASSWORD_VALUE_NAME, encryptedPassword, RegistryValueKind.String);
                    return true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(
                     $"Failed to save credentials: {ex.Message}",
                     "Error",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error);

                return false;
            }
        }

        /// <summary>
        /// Retrieves stored user credentials from Windows Registry
        /// </summary>
        /// <param name="Username">Output parameter for username</param>
        /// <param name="Password">Output parameter for decrypted password</param>
        /// <returns>True if credentials found and retrieved successfully, false otherwise</returns>
        public static bool GetStoredCredentialFromRegistry(ref string Username, ref string Password)
        {
            try
            {
                // Open registry key for reading
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(REGISTRY_KEY_PATH))
                {
                    if (key == null)
                    {
                        return false;
                    }
                    // Read username (stored as plain text)
                    string storedUsername = key.GetValue(USERNAME_VALUE_NAME) as string;
                    // Read encrypted password
                    string encryptedPassword = key.GetValue(PASSWORD_VALUE_NAME) as string;

                    // Validate both values exist before decryption
                    if (string.IsNullOrEmpty(storedUsername) || string.IsNullOrEmpty(encryptedPassword))
                    {
                        return false;
                    }

                    // Decrypt password
                    string decryptedPassword = DecryptPassword(encryptedPassword);
                    if (decryptedPassword == null)
                    {
                        return false;
                    }

                    // Set output parameters
                    Username = storedUsername;
                    Password = decryptedPassword;
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to retrieve credentials: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
        }
        
        #endregion


        #region Encryption/Decryption Methods

        /// <summary>
        /// Encrypts password using Windows Data Protection API (DPAPI)
        /// </summary>
        /// <param name="password">Plain text password</param>
        /// <returns>Base64 encoded encrypted password, or null if encryption fails</returns>
        public static string EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }

            try
            {
                // Convert password to bytes
                byte[] data = Encoding.UTF8.GetBytes(password);

                // Encrypt using DPAPI for current user
                byte[] encrypted = ProtectedData.Protect(
                    data,
                    null,
                    DataProtectionScope.CurrentUser
                );
                // Return as Base64 string
                return Convert.ToBase64String(encrypted);
            }
            catch (CryptographicException ex)
            {
                MessageBox.Show(
                    $"Encryption failed: {ex.Message}",
                    "Encryption Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return null;
            }
        }

        /// <summary>
        /// Decrypts password using Windows Data Protection API (DPAPI)
        /// </summary>
        /// <param name="encryptedPassword">Base64 encoded encrypted password</param>
        /// <returns>Plain text password, or null if decryption fails</returns>
        public static string DecryptPassword(string encryptedPassword)
        {
            if (string.IsNullOrEmpty(encryptedPassword))
            {
                return null;
            }

            try
            {
                // Convert from Base64 to bytes
                byte[] data = Convert.FromBase64String(encryptedPassword);

                // Decrypt using DPAPI for current user
                byte[] decrypted = ProtectedData.Unprotect(
                    data,
                    null,
                    DataProtectionScope.CurrentUser
                );

                // Return as string
                return Encoding.UTF8.GetString(decrypted);
            }
            catch (Exception ex) when (ex is CryptographicException || ex is FormatException)
            {
                MessageBox.Show(
                    $"Decryption failed: {ex.Message}\nPlease re-enter your credentials.",
                    "Decryption Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return null;
            }
        }

        #endregion


        #region Obsolete File-Based Methods

        /// <summary>
        /// [DEPRECATED] Retrieves credentials from text file
        /// Use GetStoredCredentialFromRegistry instead for better security
        /// </summary>
        [Obsolete("This method is deprecated. Use NewMethod instead.")]
        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            //this will get the stored username and password and will return true if found and false if not found.
            try
            {
                //gets the current project's directory
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();

                // Path for the file that contains the credential.
                string filePath = currentDirectory + "\\data.txt";

                // Check if the file exists before attempting to read it
                if (File.Exists(filePath))
                {
                    // Create a StreamReader to read from the file
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        // Read data line by line until the end of the file
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(line); // Output each line of data to the console
                            string[] result = line.Split(new string[] { "#//#" }, StringSplitOptions.RemoveEmptyEntries);

                            Username = result[0];
                            Password = result[1];
                        }
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

        }

        /// <summary>
        /// [DEPRECATED] Saves credentials to text file
        /// Use SetStoredCredentialFromRegistry instead for better security
        /// </summary>
        [Obsolete("This method is deprecated. Use NewMethod instead.")]
        public static bool RememberUsernameAndPassword(string Username, string Password)
        {

            try
            {
                //this will get the current project directory folder.
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();


                // Define the path to the text file where you want to save the data
                string filePath = currentDirectory + "\\data.txt";

                //incase the username is empty, delete the file
                if (Username == "")
                {
                    if (File.Exists(filePath))
                        File.Delete(filePath);

                    return true;
                }

                // concatonate username and passwrod withe seperator.
                string dataToSave = Username + "#//#" + Password;

                // Create a StreamWriter to write to the file
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write the data to the file
                    writer.WriteLine(dataToSave);

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
        }

        #endregion
    }
}
