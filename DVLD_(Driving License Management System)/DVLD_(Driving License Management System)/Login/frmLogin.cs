using DVLD__Driving_License_Management_System_.Global_Classes;
using DVLD__Driving_License_Management_System_.User;
using DVLD_BuisnessDVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DVLD__Driving_License_Management_System_.Login
{
    public partial class frmLogin : Form
    {
        private FormMover _formMover;

        public frmLogin()
        {
            InitializeComponent();
            // Set default button to login when Enter is pressed
            this.AcceptButton = btnLogin;
        }

        /// <summary>
        /// Form load event - retrieves stored credentials if available
        /// </summary>
        private void LoadStoredCredentials()
        {
            string username = string.Empty;
            string password = string.Empty;

            if (clsGlobal.GetStoredCredentialFromRegistry(ref username, ref password))
            {
                // Credentials found - populate fields
                txtUserName.Text = username;
                txtPassword.Text = password;
                cbRememberMe.Checked = true;
                btnLogin.Focus();
            }
            else
            {
                // No credentials found - clear fields
                cbRememberMe.Checked = false;
                txtUserName.Text = string.Empty;
                txtPassword.Text = string.Empty;
                txtUserName.Focus();
            }
        }

        /// <summary>
        /// Form load event
        /// </summary>
        private void frmLogin_Load(object sender, EventArgs e)
        {
            // Initialize form mover ONLY once
            if (_formMover == null)
            {
                _formMover = new FormMover(this, panelMoveForm);
                _formMover = new FormMover(this, panelSideBar);
            }

            // Load credentials
            LoadStoredCredentials();
        }


        /// <summary>
        /// Close button click event - clears credentials if Remember Me is unchecked
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            // Clear stored credentials if user doesn't want to remember
            if (!cbRememberMe.Checked)
            {
                clsGlobal.SetStoredCredentialFromRegistry("", "");
            }
            this.Close();
        }


        /// <summary>
        /// Login button click event - validates credentials and logs in user
        /// </summary>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();  // ✅ Trim whitespace
            string password = txtPassword.Text;

            // ✅ Validate username is not empty
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

            // ✅ Validate password is not empty
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

            // ✅ Attempt to find user by username and password
            clsUser user = clsUser.FindByUsernameAndPassword(username, password);

            if (user == null)
            {
                // ❌ Invalid credentials
                txtUserName.Focus();
                MessageBox.Show(
                    "Invalid Username/Password.",
                    "Authentication Failed",  // ✅ Fixed spelling
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // ✅ Check if user account is active BEFORE saving credentials
            if (!user.IsActive)
            {
                txtUserName.Focus();
                MessageBox.Show(
                    "Your account is not active. Please contact the administrator.",  // ✅ Fixed spelling
                    "Inactive Account",  // ✅ Fixed spelling
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);  // ✅ Changed to Warning
                return;
            }

            // ✅ Save or clear credentials AFTER all validations pass
            if (cbRememberMe.Checked)
            {
                clsGlobal.SetStoredCredentialFromRegistry(username, password);
            }
            else
            {
                clsGlobal.SetStoredCredentialFromRegistry(string.Empty, string.Empty);
            }

            // ✅ Set current user
            clsGlobal.CurrentUser = user;

            // ✅ Clear sensitive data from memory
            txtPassword.Text = string.Empty;
            password = null;

            // ✅ Show main form with proper resource disposal
            this.Hide();
            using (frmMain frm = new frmMain())
            {
                frm.ShowDialog();
            }
            LoadStoredCredentials();
            this.Show();
        }

        /// <summary>
        /// Show password checkbox changed event - toggles password visibility
        /// </summary>
        private void cbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowPassword.Checked)
            {
                // Show password as plain text
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                // Hide password with asterisks
                txtPassword.PasswordChar = '*';
            }
        }
    }
}
