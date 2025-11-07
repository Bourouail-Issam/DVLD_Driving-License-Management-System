namespace DVLD__Driving_License_Management_System_.User
{
    partial class frmChangePassword
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelMoveForm = new System.Windows.Forms.Panel();
            this.btnClose1 = new System.Windows.Forms.Button();
            this.clsUserCard1 = new DVLD__Driving_License_Management_System_.User.Controls.clsUserCard();
            this.txtCurrentPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pbGendor = new System.Windows.Forms.PictureBox();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelMoveForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGendor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMoveForm
            // 
            this.panelMoveForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(115)))), ((int)(((byte)(151)))));
            this.panelMoveForm.Controls.Add(this.btnClose1);
            this.panelMoveForm.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMoveForm.Location = new System.Drawing.Point(0, 0);
            this.panelMoveForm.Name = "panelMoveForm";
            this.panelMoveForm.Size = new System.Drawing.Size(957, 32);
            this.panelMoveForm.TabIndex = 116;
            // 
            // btnClose1
            // 
            this.btnClose1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(115)))), ((int)(((byte)(151)))));
            this.btnClose1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose1.FlatAppearance.BorderSize = 0;
            this.btnClose1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnClose1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.btnClose1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose1.Image = global::DVLD__Driving_License_Management_System_.Properties.Resources.delete;
            this.btnClose1.Location = new System.Drawing.Point(917, 0);
            this.btnClose1.Name = "btnClose1";
            this.btnClose1.Size = new System.Drawing.Size(40, 32);
            this.btnClose1.TabIndex = 116;
            this.btnClose1.UseVisualStyleBackColor = false;
            this.btnClose1.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // clsUserCard1
            // 
            this.clsUserCard1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.clsUserCard1.Location = new System.Drawing.Point(0, 38);
            this.clsUserCard1.Name = "clsUserCard1";
            this.clsUserCard1.Size = new System.Drawing.Size(955, 512);
            this.clsUserCard1.TabIndex = 117;
            // 
            // txtCurrentPassword
            // 
            this.txtCurrentPassword.BackColor = System.Drawing.Color.White;
            this.txtCurrentPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCurrentPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.5F);
            this.txtCurrentPassword.ForeColor = System.Drawing.Color.Gray;
            this.txtCurrentPassword.Location = new System.Drawing.Point(227, 554);
            this.txtCurrentPassword.Margin = new System.Windows.Forms.Padding(2);
            this.txtCurrentPassword.MaxLength = 50;
            this.txtCurrentPassword.Multiline = true;
            this.txtCurrentPassword.Name = "txtCurrentPassword";
            this.txtCurrentPassword.Size = new System.Drawing.Size(190, 28);
            this.txtCurrentPassword.TabIndex = 149;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(63, 558);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(161, 20);
            this.label5.TabIndex = 147;
            this.label5.Text = "Current Password :";
            // 
            // pbGendor
            // 
            this.pbGendor.Image = global::DVLD__Driving_License_Management_System_.Properties.Resources.password;
            this.pbGendor.Location = new System.Drawing.Point(22, 555);
            this.pbGendor.Name = "pbGendor";
            this.pbGendor.Size = new System.Drawing.Size(31, 26);
            this.pbGendor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbGendor.TabIndex = 148;
            this.pbGendor.TabStop = false;
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.BackColor = System.Drawing.Color.White;
            this.txtNewPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNewPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.5F);
            this.txtNewPassword.ForeColor = System.Drawing.Color.Gray;
            this.txtNewPassword.Location = new System.Drawing.Point(204, 610);
            this.txtNewPassword.Margin = new System.Windows.Forms.Padding(2);
            this.txtNewPassword.MaxLength = 50;
            this.txtNewPassword.Multiline = true;
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.Size = new System.Drawing.Size(190, 28);
            this.txtNewPassword.TabIndex = 152;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(63, 614);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 20);
            this.label1.TabIndex = 150;
            this.label1.Text = "New Password :";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD__Driving_License_Management_System_.Properties.Resources.password;
            this.pictureBox1.Location = new System.Drawing.Point(22, 611);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(31, 26);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 151;
            this.pictureBox1.TabStop = false;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.White;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.5F);
            this.textBox2.ForeColor = System.Drawing.Color.Gray;
            this.textBox2.Location = new System.Drawing.Point(229, 666);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2.MaxLength = 50;
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(190, 28);
            this.textBox2.TabIndex = 155;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(63, 670);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 20);
            this.label2.TabIndex = 153;
            this.label2.Text = "Confirm Password :";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DVLD__Driving_License_Management_System_.Properties.Resources.password;
            this.pictureBox2.Location = new System.Drawing.Point(22, 667);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(31, 26);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 154;
            this.pictureBox2.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(811, 670);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(122, 33);
            this.btnSave.TabIndex = 156;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(690, 670);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(103, 33);
            this.btnClose.TabIndex = 157;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 722);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.txtNewPassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtCurrentPassword);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pbGendor);
            this.Controls.Add(this.clsUserCard1);
            this.Controls.Add(this.panelMoveForm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmChangePassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmChangePassword";
            this.Load += new System.EventHandler(this.frmChangePassword_Load);
            this.panelMoveForm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbGendor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelMoveForm;
        private System.Windows.Forms.Button btnClose1;
        private Controls.clsUserCard clsUserCard1;
        private System.Windows.Forms.TextBox txtCurrentPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pbGendor;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
    }
}