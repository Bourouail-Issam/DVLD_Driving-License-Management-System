using DVLD_BuisnessDVLD_Buisness;
using DVLD_Common;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace DVLD__Driving_License_Management_System_.Global_Classes
{
    /// <summary>
    /// Global utility class for the DVLD application.
    /// Provides centralized services for:
    /// - Current user session management
    /// - Password hashing and verification (PBKDF2 + SHA256)
    /// - Credential storage using Windows Registry (DPAPI encrypted)
    /// </summary>
    public class clsGlobal
    {
        // ── Session ───────────────────────────────────────────────────────────
        /// <summary>
        /// Holds the currently authenticated user for the session.
        /// Set after successful login, cleared on logout.
        /// </summary>
        public static clsUser CurrentUser;

        // ── Cryptography Constants ────────────────────────────────────────────
        /// <summary>Size of the random salt in bytes (NIST recommendation: minimum 16)</summary>
        private const int SaltSize = 16;

        /// <summary>Size of the derived hash in bytes (SHA-256 = 32 bytes)</summary>
        private const int HashSize = 32;

        /// <summary>
        /// PBKDF2 iteration count.
        /// OWASP 2023 recommendation: minimum 600,000 for SHA-256.
        /// Using 100,000 as a balanced default — increase for higher security environments.
        /// </summary>
        private const int Iteration = 100000;

        // ── Registry Constants ────────────────────────────────────────────────
        /// <summary>Registry path under HKCU where credentials are stored</summary>
        private const string REGISTRY_KEY_PATH = @"SOFTWARE\DVLD_License";
        private const string USERNAME_VALUE_NAME = "Username";
        private const string PASSWORD_VALUE_NAME = "Password";

        #region Registry Methods

        /// <summary>
        /// Saves user credentials to the Windows Registry.
        /// The password is encrypted using DPAPI (DataProtectionScope.CurrentUser)
        /// before storage, making it unreadable on other machines or user accounts.
        /// Pass empty strings to delete any previously stored credentials.
        /// </summary>
        /// <param name="Username">Username to store (plain text — not sensitive)</param>
        /// <param name="Password">Plain-text password (will be DPAPI-encrypted before saving)</param>
        /// <returns>True if the operation succeeded; false otherwise</returns>
        public static bool SetStoredCredentialFromRegistry(string Username, string Password)
        {
            try
            {
                // If username is empty, remove stored credentials entirely
                if (string.IsNullOrEmpty(Username))
                {
                    try
                    {
                        Registry.CurrentUser.DeleteSubKeyTree(REGISTRY_KEY_PATH, false);
                    }
                    catch(Exception ex)
                    {
                        clsLogEvent.LogEvent(
                            $"Error in SetStoredCredentialFromRegistry : Failed to Delete SubKeyTree" +
                            $"Error Message : {ex.Message}"                                              +
                            $"Stack Trace : {ex.StackTrace}" ,
                            EventLogEntryType.Error
                            );
                    }
                    return true;
                }

                // Create the registry key (or open if already exists)
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(REGISTRY_KEY_PATH))
                {
                    if (key == null)
                    {
                        // Registry access denied or unavailable
                        clsLogEvent.LogEvent(
                            "SetStoredCredentialFromRegistry: Failed to create registry key.",
                            EventLogEntryType.Error);

                        return false;
                    }

                    // Store username as plain text (not sensitive)
                    key.SetValue(USERNAME_VALUE_NAME, Username, RegistryValueKind.String);

                    // Encrypt the password with DPAPI before storing
                    string encryptedPassword = EncryptPassword(Password);
                    if (encryptedPassword == null)
                    {
                        // Encryption failed — logged inside EncryptPassword
                        return false;
                    }

                    key.SetValue(PASSWORD_VALUE_NAME, encryptedPassword, RegistryValueKind.String);
                    return true;
                }
            }
            catch(Exception ex)
            {
                clsLogEvent.LogEvent(
                   $"Unexpected error in SetStoredCredentialFromRegistry\n" +
                   $"Error Message : {ex.Message}\n" +
                   $"Stack Trace   : {ex.StackTrace}",
                   EventLogEntryType.Error);

                return false;
            }
        }

        /// <summary>
        /// Retrieves and decrypts user credentials previously saved to the Windows Registry.
        /// Uses DPAPI to decrypt the password — only works on the same machine and user account.
        /// </summary>
        /// <param name="Username">Output: the stored username (plain text)</param>
        /// <param name="Password">Output: the decrypted plain-text password</param>
        /// <returns>True if credentials were found and decrypted successfully; false otherwise</returns>
        public static bool GetStoredCredentialFromRegistry(ref string Username, ref string Password)
        {
            try
            {
                // Open registry key in read-only mode
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(REGISTRY_KEY_PATH))
                {
                    // Key doesn't exist → no credentials saved
                    if (key == null)
                        return false;

                    string storedUsername = key.GetValue(USERNAME_VALUE_NAME) as string;
                    string encryptedPassword = key.GetValue(PASSWORD_VALUE_NAME) as string;

                    // Both values must exist to proceed
                    if (string.IsNullOrEmpty(storedUsername) || string.IsNullOrEmpty(encryptedPassword))
                        return false;


                    // Decrypt the stored password using DPAPI
                    string decryptedPassword = DecryptPassword(encryptedPassword);
                    if (decryptedPassword == null)
                        return false; // Decryption failure is logged inside DecryptPassword

                    // Assign output parameters only after full success
                    Username = storedUsername;
                    Password = decryptedPassword;
                    return true;
                }
            }
            catch (Exception ex)
            {
                clsLogEvent.LogEvent(
                    $"Unexpected error in GetStoredCredentialFromRegistry\n" +
                    $"Error Message : {ex.Message}\n" +
                    $"Stack Trace   : {ex.StackTrace}",
                    EventLogEntryType.Error);

                return false;
            }
        }

        #endregion


        #region  Password Hashing (PBKDF2 / SHA-256)

        /// <summary>
        /// Generates a cryptographically secure random salt using RNGCryptoServiceProvider.
        /// </summary>
        /// <returns>A byte array of length <see cref="SaltSize"/>, or null if generation fails</returns>
        private static byte[] GenerateSalt()
        {
            byte[] salt = new byte[SaltSize];
            try
            {
                // RandomNumberGenerator is the recommended CSPRNG in .NET
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                    return salt;
                }
            }
            catch (Exception ex)
            {
                clsLogEvent.LogEvent(
                     $"General Error in GenerateSalt \n" +
                     $"Error Message : {ex.Message}" +
                     $"Stack Trace : {ex.StackTrace}",
                     EventLogEntryType.Error
                    );
                return null;
            }
        }

        /// <summary>
        /// Hashes a plain-text password using PBKDF2 with SHA-256 and a random salt.
        /// Output format: Base64( Salt[16] + Hash[32] ) → always 64 characters.
        /// </summary>
        /// <param name="PlainText">The plain-text password to hash</param>
        /// <returns>
        /// A 64-character Base64 string containing salt + hash,
        /// or null if input is empty or an internal error occurs
        /// </returns>
        public static string HashPassword(string PlainText)
        {
            // ✅ IsNullOrWhiteSpace covers null, empty, and whitespace-only strings
            if (string.IsNullOrWhiteSpace(PlainText))
                return null;

            // Generate a fresh random salt for every password
            byte[] salt = GenerateSalt();

            if (salt == null)
                return null;

            using (var PDKFD2 = new Rfc2898DeriveBytes(PlainText, salt, Iteration, HashAlgorithmName.SHA256))
            {
                byte[] hash = PDKFD2.GetBytes(HashSize);

                // Combine salt + hash into one array for compact storage
                byte[] combined = new byte[HashSize + SaltSize];  // 16 + 32 = 48 bytes
                Array.Copy(salt, 0, combined, 0, SaltSize); // bytes  0–15  = salt
                Array.Copy(hash, 0, combined, SaltSize, HashSize); // bytes 16–47  = hash

                // Encode to Base64 → 64 characters
                return Convert.ToBase64String(combined);
            }
        }

        /// <summary>
        /// Constant-time byte array comparison to prevent timing-based side-channel attacks.
        /// Always iterates over the full length of the longer array,
        /// regardless of where the first difference is found.
        /// </summary>
        /// <param name="a">First byte array</param>
        /// <param name="b">Second byte array</param>
        /// <returns>True if both arrays are identical in length and content</returns>
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            if (a == null || b == null) 
                return false;

            // XOR of lengths: non-zero immediately if lengths differ (but we still iterate fully)
            uint diff = (uint)(a.Length ^ b.Length);

            // Always iterate max length to prevent timing leaks
            int maxLen = Math.Max(a.Length, b.Length);
            for (int i = 0; i < maxLen; i++)
            {
                // Pad shorter array with 0 to keep constant loop count
                byte byteA = i < a.Length ? a[i] : (byte)0;
                byte byteB = i < b.Length ? b[i] : (byte)0;
                diff |= (uint)(byteA ^ byteB);
            }
            // diff == 0 means arrays are identical
            return diff == 0;
        }

        /// <summary>
        /// Verifies a plain-text password against a stored PBKDF2 hash.
        /// Extracts the original salt from the stored hash, recomputes the hash,
        /// then uses constant-time comparison to prevent timing attacks.
        /// </summary>
        /// <param name="password">The plain-text password entered by the user</param>
        /// <param name="storedHash">The Base64-encoded hash previously created by <see cref="HashPassword"/></param>
        /// <returns>True if the password matches; false otherwise</returns>
        public static bool VerifyPassword(string password, string storedHash)
        {
            if (string.IsNullOrEmpty(password))
                return false; 

            if (string.IsNullOrEmpty(storedHash))
                return false; 

            try
            {
                // Decode Base64 → 48 bytes (16 salt + 32 hash)
                byte[] combined = Convert.FromBase64String(storedHash);

                // ✅ Validate expected length before array operations
                if (combined.Length != SaltSize + HashSize)
                {
                    clsLogEvent.LogEvent(
                        $"VerifyPassword: Invalid storedHash length ({combined.Length} bytes).",
                        EventLogEntryType.Warning);
                    return false;
                }
                // Extract the original salt (first 16 bytes)
                byte[] salt = new byte[SaltSize];
                Array.Copy(combined, 0, salt, 0, SaltSize);

                // Extract the stored hash (next 32 bytes)
                byte[] storedPasswordHash = new byte[HashSize];
                Array.Copy(combined, SaltSize, storedPasswordHash, 0, HashSize);

                // Recompute hash using the same salt and iteration count
                using (var PDKFD2 = new Rfc2898DeriveBytes(password, salt, Iteration, HashAlgorithmName.SHA256))
                {
                    byte[] computeHash = PDKFD2.GetBytes(HashSize);

                    // ✅ Constant-time comparison to prevent timing attacks
                    return SlowEquals(computeHash, storedPasswordHash);
                }
            }
            catch (FormatException ex)
            {
                // storedHash was not valid Base64
                clsLogEvent.LogEvent(
                    $"VerifyPassword: storedHash is not valid Base64.\n" +
                    $"Error Message : {ex.Message}\n" +
                    $"Stack Trace   : {ex.StackTrace}",
                    EventLogEntryType.Error);
                return false;
            }
            catch (CryptographicException ex)
            {
                clsLogEvent.LogEvent(
                 $"VerifyPassword: Cryptographic failure.\n" +
                 $"Error Message : {ex.Message}\n" +
                 $"Stack Trace   : {ex.StackTrace}",
                 EventLogEntryType.Error);
                return false;
            }
            catch (Exception ex)
            {
                clsLogEvent.LogEvent(
                   $"VerifyPassword: Unexpected error.\n" +
                   $"Error Message : {ex.Message}\n" +
                   $"Stack Trace   : {ex.StackTrace}",
                   EventLogEntryType.Error);
                return false;
            }
        }

        #endregion


        #region Encryption / Decryption (DPAPI)

        /// <summary>
        /// Encrypts a plain-text password using the Windows Data Protection API (DPAPI).
        /// The result is bound to the current Windows user account and machine,
        /// making it unreadable if moved to another machine or user profile.
        /// </summary>
        /// <param name="password">The plain-text password to encrypt</param>
        /// <returns>A Base64-encoded encrypted string, or null if encryption fails</returns>
        public static string EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return null;

            try
            {
                // Convert password to bytes
                byte[] data = Encoding.UTF8.GetBytes(password);

                // DPAPI — CurrentUser scope: only this Windows user can decrypt
                byte[] encrypted = ProtectedData.Protect(
                    data,
                    null,
                    DataProtectionScope.CurrentUser
                );

                return Convert.ToBase64String(encrypted);
            }
            catch (CryptographicException ex)
            {
                clsLogEvent.LogEvent(
                    $"EncryptPassword: DPAPI encryption failed.\n" +
                    $"Error Message : {ex.Message}\n" +
                    $"Stack Trace   : {ex.StackTrace}",
                    EventLogEntryType.Error);
                return null;
            }
            catch (Exception ex)
            {
                clsLogEvent.LogEvent(
                   $"EncryptPassword: Unexpected error.\n" +
                   $"Error Message : {ex.Message}\n" +
                   $"Stack Trace   : {ex.StackTrace}",
                   EventLogEntryType.Error);
                return null;
            }
        }

        /// <summary>
        /// Decrypts a DPAPI-encrypted password previously created by <see cref="EncryptPassword"/>.
        /// Decryption will only succeed on the same machine and Windows user account.
        /// </summary>
        /// <param name="encryptedPassword">Base64-encoded encrypted password</param>
        /// <returns>The plain-text password, or null if decryption fails</returns>
        public static string DecryptPassword(string encryptedPassword)
        {
            if (string.IsNullOrEmpty(encryptedPassword))
                return null;

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
                clsLogEvent.LogEvent(
                     $"DecryptPassword: Decryption failed (credentials may have been created on a different machine/user).\n" +
                     $"Error Message : {ex.Message}\n" +
                     $"Stack Trace   : {ex.StackTrace}",
                     EventLogEntryType.Warning);
                return null;
            }
        }

        #endregion


        #region Obsolete Methods (File-Based — Do Not Use)
        /// <summary>
        /// [OBSOLETE] Reads credentials from a plain-text file on disk.
        /// SECURITY RISK: passwords are stored unencrypted.
        /// Use <see cref="GetStoredCredentialFromRegistry"/> instead.
        /// </summary>
        [Obsolete("Insecure — stores passwords in plain text. Use GetStoredCredentialFromRegistry instead.", error: true)]
        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            //this will get the stored username and password and will return true if found and false if not found.
            try
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "data.txt");

                if (!File.Exists(filePath))
                    return false;

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(new[] { "#//#" }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length >= 2)
                        {
                            Username = parts[0];
                            Password = parts[1];
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                clsLogEvent.LogEvent(
                     $"GetStoredCredential (Obsolete): {ex.Message}",
                     EventLogEntryType.Warning);
                return false;
            }

        }

        /// <summary>
        /// [OBSOLETE] Saves credentials to a plain-text file on disk.
        /// SECURITY RISK: passwords are stored unencrypted.
        /// Use <see cref="SetStoredCredentialFromRegistry"/> instead.
        /// </summary>
        [Obsolete("Insecure — stores passwords in plain text. Use SetStoredCredentialFromRegistry instead.", error: true)]
        public static bool RememberUsernameAndPassword(string Username, string Password)
        {

            try
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "data.txt");

                //incase the username is empty, delete the file
                if (Username == "")
                {
                    if (File.Exists(filePath))
                        File.Delete(filePath);

                    return true;
                }

                // Create a StreamWriter to write to the file
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write the data to the file
                    writer.WriteLine($"{Username}#//#{Password}"); ;
                }
                return true;
            }
            catch (Exception ex)
            {
                clsLogEvent.LogEvent(
                       $"RememberUsernameAndPassword (Obsolete): {ex.Message}",
                       EventLogEntryType.Warning);
                return false;
            }
        }

        #endregion
    }
}
