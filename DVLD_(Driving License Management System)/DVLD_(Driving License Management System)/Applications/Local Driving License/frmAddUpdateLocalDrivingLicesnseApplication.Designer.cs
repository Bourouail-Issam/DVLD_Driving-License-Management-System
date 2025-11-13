namespace DVLD__Driving_License_Management_System_.Applications.Local_Driving_License
{
    partial class frmAddUpdateLocalDrivingLicesnseApplication
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tcApplicationInfo = new System.Windows.Forms.TabControl();
            this.tpPersonalInfo = new System.Windows.Forms.TabPage();
            this.btnApplicationInfoNext = new System.Windows.Forms.Button();
            this.ctrlPersonCardWithFilter1 = new DVLD__Driving_License_Management_System_.People.Controls.ctrlPersonCardWithFilter();
            this.tpLoginInfo = new System.Windows.Forms.TabPage();
            this.lblFees = new System.Windows.Forms.Label();
            this.lblCreatedByUser = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pbGendor = new System.Windows.Forms.PictureBox();
            this.cbLicenseClass = new System.Windows.Forms.ComboBox();
            this.lblApplicationDate = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblLocalDrivingLicebseApplicationID = new System.Windows.Forms.Label();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose1 = new System.Windows.Forms.Button();
            this.panelMoveForm = new System.Windows.Forms.Panel();
            this.tcApplicationInfo.SuspendLayout();
            this.tpPersonalInfo.SuspendLayout();
            this.tpLoginInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGendor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.panelMoveForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(16, 52);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(952, 39);
            this.lblTitle.TabIndex = 253;
            this.lblTitle.Text = "Local Driving License Application";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.No;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(846, 605);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(122, 33);
            this.btnSave.TabIndex = 255;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(725, 605);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(103, 33);
            this.btnClose.TabIndex = 256;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tcApplicationInfo
            // 
            this.tcApplicationInfo.Controls.Add(this.tpPersonalInfo);
            this.tcApplicationInfo.Controls.Add(this.tpLoginInfo);
            this.tcApplicationInfo.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.tcApplicationInfo.Location = new System.Drawing.Point(10, 91);
            this.tcApplicationInfo.Name = "tcApplicationInfo";
            this.tcApplicationInfo.SelectedIndex = 0;
            this.tcApplicationInfo.Size = new System.Drawing.Size(963, 497);
            this.tcApplicationInfo.TabIndex = 254;
            // 
            // tpPersonalInfo
            // 
            this.tpPersonalInfo.BackColor = System.Drawing.Color.Gainsboro;
            this.tpPersonalInfo.Controls.Add(this.btnApplicationInfoNext);
            this.tpPersonalInfo.Controls.Add(this.ctrlPersonCardWithFilter1);
            this.tpPersonalInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tpPersonalInfo.Location = new System.Drawing.Point(4, 29);
            this.tpPersonalInfo.Name = "tpPersonalInfo";
            this.tpPersonalInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpPersonalInfo.Size = new System.Drawing.Size(955, 464);
            this.tpPersonalInfo.TabIndex = 0;
            this.tpPersonalInfo.Text = "Personal Info";
            // 
            // btnApplicationInfoNext
            // 
            this.btnApplicationInfoNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplicationInfoNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnApplicationInfoNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F);
            this.btnApplicationInfoNext.Image = global::DVLD__Driving_License_Management_System_.Properties.Resources.next_btn_32;
            this.btnApplicationInfoNext.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnApplicationInfoNext.Location = new System.Drawing.Point(844, 418);
            this.btnApplicationInfoNext.Margin = new System.Windows.Forms.Padding(4);
            this.btnApplicationInfoNext.Name = "btnApplicationInfoNext";
            this.btnApplicationInfoNext.Size = new System.Drawing.Size(104, 39);
            this.btnApplicationInfoNext.TabIndex = 101;
            this.btnApplicationInfoNext.Text = "Next";
            this.btnApplicationInfoNext.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnApplicationInfoNext.UseVisualStyleBackColor = true;
            // 
            // ctrlPersonCardWithFilter1
            // 
            this.ctrlPersonCardWithFilter1.AutoSize = true;
            this.ctrlPersonCardWithFilter1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ctrlPersonCardWithFilter1.BackColor = System.Drawing.Color.Gainsboro;
            this.ctrlPersonCardWithFilter1.FilterEnabled = true;
            this.ctrlPersonCardWithFilter1.Location = new System.Drawing.Point(3, 3);
            this.ctrlPersonCardWithFilter1.Name = "ctrlPersonCardWithFilter1";
            this.ctrlPersonCardWithFilter1.Size = new System.Drawing.Size(949, 404);
            this.ctrlPersonCardWithFilter1.TabIndex = 0;
            // 
            // tpLoginInfo
            // 
            this.tpLoginInfo.BackColor = System.Drawing.Color.Gainsboro;
            this.tpLoginInfo.Controls.Add(this.lblFees);
            this.tpLoginInfo.Controls.Add(this.lblCreatedByUser);
            this.tpLoginInfo.Controls.Add(this.pictureBox2);
            this.tpLoginInfo.Controls.Add(this.label6);
            this.tpLoginInfo.Controls.Add(this.label5);
            this.tpLoginInfo.Controls.Add(this.pbGendor);
            this.tpLoginInfo.Controls.Add(this.cbLicenseClass);
            this.tpLoginInfo.Controls.Add(this.lblApplicationDate);
            this.tpLoginInfo.Controls.Add(this.pictureBox1);
            this.tpLoginInfo.Controls.Add(this.label4);
            this.tpLoginInfo.Controls.Add(this.lblLocalDrivingLicebseApplicationID);
            this.tpLoginInfo.Controls.Add(this.pictureBox8);
            this.tpLoginInfo.Controls.Add(this.pictureBox3);
            this.tpLoginInfo.Controls.Add(this.label2);
            this.tpLoginInfo.Controls.Add(this.label1);
            this.tpLoginInfo.Location = new System.Drawing.Point(4, 29);
            this.tpLoginInfo.Name = "tpLoginInfo";
            this.tpLoginInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpLoginInfo.Size = new System.Drawing.Size(955, 464);
            this.tpLoginInfo.TabIndex = 1;
            this.tpLoginInfo.Text = "Application Info";
            // 
            // lblFees
            // 
            this.lblFees.AutoSize = true;
            this.lblFees.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFees.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblFees.Location = new System.Drawing.Point(142, 236);
            this.lblFees.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFees.Name = "lblFees";
            this.lblFees.Size = new System.Drawing.Size(49, 20);
            this.lblFees.TabIndex = 290;
            this.lblFees.Text = "[$$$]";
            // 
            // lblCreatedByUser
            // 
            this.lblCreatedByUser.AutoSize = true;
            this.lblCreatedByUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreatedByUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblCreatedByUser.Location = new System.Drawing.Point(191, 296);
            this.lblCreatedByUser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCreatedByUser.Name = "lblCreatedByUser";
            this.lblCreatedByUser.Size = new System.Drawing.Size(59, 20);
            this.lblCreatedByUser.TabIndex = 289;
            this.lblCreatedByUser.Text = "[????]";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DVLD__Driving_License_Management_System_.Properties.Resources.user_24;
            this.pictureBox2.Location = new System.Drawing.Point(44, 293);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(31, 26);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 288;
            this.pictureBox2.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(85, 296);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 20);
            this.label6.TabIndex = 287;
            this.label6.Text = "Created By :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(85, 236);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 20);
            this.label5.TabIndex = 284;
            this.label5.Text = "Fees : ";
            // 
            // pbGendor
            // 
            this.pbGendor.Image = global::DVLD__Driving_License_Management_System_.Properties.Resources.Fees_48;
            this.pbGendor.Location = new System.Drawing.Point(44, 233);
            this.pbGendor.Name = "pbGendor";
            this.pbGendor.Size = new System.Drawing.Size(31, 26);
            this.pbGendor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbGendor.TabIndex = 285;
            this.pbGendor.TabStop = false;
            // 
            // cbLicenseClass
            // 
            this.cbLicenseClass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbLicenseClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.cbLicenseClass.FormattingEnabled = true;
            this.cbLicenseClass.Location = new System.Drawing.Point(222, 172);
            this.cbLicenseClass.Name = "cbLicenseClass";
            this.cbLicenseClass.Size = new System.Drawing.Size(227, 28);
            this.cbLicenseClass.TabIndex = 152;
            // 
            // lblApplicationDate
            // 
            this.lblApplicationDate.AutoSize = true;
            this.lblApplicationDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplicationDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblApplicationDate.Location = new System.Drawing.Point(234, 116);
            this.lblApplicationDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblApplicationDate.Name = "lblApplicationDate";
            this.lblApplicationDate.Size = new System.Drawing.Size(109, 20);
            this.lblApplicationDate.TabIndex = 151;
            this.lblApplicationDate.Text = "[??/??/????]";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD__Driving_License_Management_System_.Properties.Resources.calendar_24;
            this.pictureBox1.Location = new System.Drawing.Point(44, 113);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(31, 26);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 150;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(85, 116);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 20);
            this.label4.TabIndex = 149;
            this.label4.Text = "Application Date :";
            // 
            // lblLocalDrivingLicebseApplicationID
            // 
            this.lblLocalDrivingLicebseApplicationID.AutoSize = true;
            this.lblLocalDrivingLicebseApplicationID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalDrivingLicebseApplicationID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblLocalDrivingLicebseApplicationID.Location = new System.Drawing.Point(246, 56);
            this.lblLocalDrivingLicebseApplicationID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLocalDrivingLicebseApplicationID.Name = "lblLocalDrivingLicebseApplicationID";
            this.lblLocalDrivingLicebseApplicationID.Size = new System.Drawing.Size(59, 20);
            this.lblLocalDrivingLicebseApplicationID.TabIndex = 144;
            this.lblLocalDrivingLicebseApplicationID.Text = "[????]";
            // 
            // pictureBox8
            // 
            this.pictureBox8.Image = global::DVLD__Driving_License_Management_System_.Properties.Resources.badgeID_24;
            this.pictureBox8.Location = new System.Drawing.Point(44, 53);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(31, 26);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox8.TabIndex = 141;
            this.pictureBox8.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::DVLD__Driving_License_Management_System_.Properties.Resources.Description_48;
            this.pictureBox3.Location = new System.Drawing.Point(44, 173);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(31, 26);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 139;
            this.pictureBox3.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(85, 176);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 20);
            this.label2.TabIndex = 137;
            this.label2.Text = "License Class :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(85, 56);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 20);
            this.label1.TabIndex = 136;
            this.label1.Text = "D.L.Application ID :";
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
            this.btnClose1.Location = new System.Drawing.Point(944, 0);
            this.btnClose1.Name = "btnClose1";
            this.btnClose1.Size = new System.Drawing.Size(40, 32);
            this.btnClose1.TabIndex = 252;
            this.btnClose1.UseVisualStyleBackColor = false;
            this.btnClose1.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelMoveForm
            // 
            this.panelMoveForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(115)))), ((int)(((byte)(151)))));
            this.panelMoveForm.Controls.Add(this.btnClose1);
            this.panelMoveForm.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMoveForm.Location = new System.Drawing.Point(0, 0);
            this.panelMoveForm.Name = "panelMoveForm";
            this.panelMoveForm.Size = new System.Drawing.Size(984, 32);
            this.panelMoveForm.TabIndex = 257;
            // 
            // frmAddUpdateLocalDrivingLicesnseApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 644);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tcApplicationInfo);
            this.Controls.Add(this.panelMoveForm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmAddUpdateLocalDrivingLicesnseApplication";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmAddUpdateLocalDrivingLicesnseApplication";
            this.Load += new System.EventHandler(this.frmAddUpdateLocalDrivingLicesnseApplication_Load);
            this.tcApplicationInfo.ResumeLayout(false);
            this.tpPersonalInfo.ResumeLayout(false);
            this.tpPersonalInfo.PerformLayout();
            this.tpLoginInfo.ResumeLayout(false);
            this.tpLoginInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGendor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.panelMoveForm.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TabControl tcApplicationInfo;
        private System.Windows.Forms.TabPage tpPersonalInfo;
        private System.Windows.Forms.Button btnApplicationInfoNext;
        private People.Controls.ctrlPersonCardWithFilter ctrlPersonCardWithFilter1;
        private System.Windows.Forms.TabPage tpLoginInfo;
        private System.Windows.Forms.ComboBox cbLicenseClass;
        private System.Windows.Forms.Label lblApplicationDate;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblLocalDrivingLicebseApplicationID;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFees;
        private System.Windows.Forms.Label lblCreatedByUser;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pbGendor;
        private System.Windows.Forms.Button btnClose1;
        private System.Windows.Forms.Panel panelMoveForm;
    }
}