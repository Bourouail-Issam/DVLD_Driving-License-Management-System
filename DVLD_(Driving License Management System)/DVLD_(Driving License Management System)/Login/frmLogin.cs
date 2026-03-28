using DVLD__Driving_License_Management_System_.Global_Classes;
using DVLD_BuisnessDVLD_Buisness;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DVLD__Driving_License_Management_System_.Login
{
    /// <summary>
    /// Login form for the DVLD application.
    /// Responsibilities:
    /// - Validate user credentials via username + PBKDF2 password hash
    /// - Support optional "Remember Me" (DPAPI-encrypted Registry storage)
    /// - Enforce account active status before granting access
    /// - Launch the main form upon successful authentication
    /// </summary>
    public partial class frmLogin : Form
    {
        // Enables dragging the borderless form by its panel
        private readonly List<FormMover> _formMovers = new List<FormMover>();

        /// <summary>
        /// Initializes the login form and sets Enter key to trigger the Login button.
        /// </summary>
        public frmLogin()
        {
            InitializeComponent();
            // ✅ Enter key triggers login without needing to click the button
            this.AcceptButton = btnLogin;
        }

        #region Form Events

        /// <summary>
        /// Fires when the form loads.
        /// Initializes the form-drag helper and pre-fills credentials if remembered.
        /// </summary>
        private void frmLogin_Load(object sender, EventArgs e)
        {
            // Initialize form mover ONLY once
            if (_formMovers.Count == 0)
            {
                _formMovers.Add(new FormMover(this, panelMoveForm));
                _formMovers.Add(new FormMover(this, panelSideBar));
            }

            // Load credentials
            LoadStoredCredentials();
        }

        /// <summary>
        /// Fires when the Close button is clicked.
        /// Clears stored credentials if "Remember Me" is unchecked, then closes the form.
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            // Clear registry credentials if the user does not want to be remembered
            if (!cbRememberMe.Checked)
                clsGlobal.SetStoredCredentialFromRegistry(string.Empty, string.Empty);

            this.Close();
        }

        /// <summary>
        /// Fires when the form is fully closed (from any source: button, Alt+F4, taskbar).
        /// Disposes all FormMover instances to unsubscribe mouse events and prevent memory leaks.
        /// </summary>
        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (var mover in _formMovers)
                mover.Dispose(); // ← الآن الوقت الصحيح للتنظيف

            _formMovers.Clear();
        }

        /// <summary>
        /// Fires when the "Show Password" checkbox state changes.
        /// Toggles the password text box between masked and plain-text display.
        /// </summary>
        private void cbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            // '\0' = no masking character → password visible as plain text
            txtPassword.PasswordChar = cbShowPassword.Checked ? '\0' : '*';
        }

        #endregion

        #region Authentication

        /// <summary>
        /// Main login handler. Executes in this order:
        /// 1. Validate that username and password fields are not empty
        /// 2. Retrieve the stored password hash for the given username
        /// 3. Verify the entered password against the stored hash (PBKDF2)
        /// 4. Load the full user object and confirm it exists
        /// 5. Check that the account is active
        /// 6. Persist or clear "Remember Me" credentials
        /// 7. Open the main application form
        /// </summary>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // ── 1. Read and sanitize input ────────────────────────────────────
            string username = txtUserName.Text.Trim(); // Remove accidental leading/trailing spaces
            string password = txtPassword.Text;        // Do NOT trim passwords (spaces may be intentional)


            // ── 2. Guard: username must not be blank ──────────────────────────
            if (string.IsNullOrWhiteSpace(username))
            {
                txtUserName.Focus();
                MessageBox.Show(
                    "Please enter a username.",
                    "Missing Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // ── 3. Guard: password must not be blank ──────────────────────────
            if (string.IsNullOrWhiteSpace(password))
            {
                txtPassword.Focus();
                MessageBox.Show(
                    "Please enter a password.",
                    "Missing Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // ── 4. Retrieve the stored hash for this username ─────────────────
            // Returns null if the username does not exist in the database
            string storedHash = clsUser.GetStoredHashByUsername(username);

            if (string.IsNullOrEmpty(storedHash))
            {
                // ✅ Generic message — do not reveal whether the username or password was wrong
                ShowInvalidCredentials();
                return;
            }

            // ── 5. Verify entered password against the stored PBKDF2 hash ─────
            if (!clsGlobal.VerifyPassword(password, storedHash))
            {
                ShowInvalidCredentials();
                return;
            }

            // ── 6. Load full user object (password already verified above) ────
            clsUser user = clsUser.FindByUsername(username);

            if (user == null)
            {
                // Edge case: hash existed but user record is missing (data integrity issue)
                ShowInvalidCredentials();
                return;
            }

            // ── 7. Enforce account active status ─────────────────────────────
            if (!user.IsActive)
            {
                txtUserName.Focus();
                MessageBox.Show(
                    "Your account is inactive. Please contact the administrator.",
                    "Inactive Account",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // ── 8. Persist or clear "Remember Me" credentials ─────────────────
            // ✅ Credentials are saved AFTER all validations — never before
            // ✅ Password is DPAPI-encrypted inside SetStoredCredentialFromRegistry
            clsGlobal.SetStoredCredentialFromRegistry(
                cbRememberMe.Checked ? username : string.Empty,
                cbRememberMe.Checked ? password : string.Empty);


            // ── 9. Establish the current session ──────────────────────────────
            clsGlobal.CurrentUser = user;

            // ── 10. Scrub sensitive data from memory and UI ───────────────────
            txtPassword.Clear();
            password = string.Empty;
            storedHash = string.Empty;

            // ── 11. Open the main form ────────────────────────────────────────
            this.Hide();
            try
            {
                // ✅ using ensures frmMain is properly disposed after the user closes it
                using (frmMain frm = new frmMain())
                {
                    frm.ShowDialog();
                }
            }
            finally
            {
                // ✅ Always reload credentials and show login form,
                // even if frmMain threw an unhandled exception
                LoadStoredCredentials();
                this.Show();
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Reads stored credentials from the Windows Registry and populates the form fields.
        /// If no credentials are found, fields are cleared and username receives focus.
        /// Called on form load and after returning from the main form.
        /// </summary>
        private void LoadStoredCredentials()
        {
            string username = string.Empty;
            string password = string.Empty;

            if (clsGlobal.GetStoredCredentialFromRegistry(ref username, ref password))
            {
                // ✅ Credentials found — pre-fill and focus the login button
                txtUserName.Text = username;
                txtPassword.Text = password;
                cbRememberMe.Checked = true;
                btnLogin.Focus();
            }
            else
            {
                // No stored credentials — clear everything and focus username
                cbRememberMe.Checked = false;
                txtUserName.Text = string.Empty;
                txtPassword.Text = string.Empty;
                txtUserName.Focus();
            }
        }

        /// <summary>
        /// Displays a generic "Invalid Username or Password" error message.
        /// Using a single generic message prevents attackers from determining
        /// whether the username or the password was incorrect (security best practice).
        /// </summary>
        private void ShowInvalidCredentials()
        {
            txtUserName.Focus();
            MessageBox.Show(
                "Invalid Username or Password.",
                "Authentication Failed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        #endregion

        
    }
}
