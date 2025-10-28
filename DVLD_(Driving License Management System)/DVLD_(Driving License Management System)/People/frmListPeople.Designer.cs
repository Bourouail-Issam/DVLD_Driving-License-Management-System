namespace DVLD__Driving_License_Management_System_.People
{
    partial class frmListPeople
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListPeople));
            this.label3 = new System.Windows.Forms.Label();
            this.dgvPeople = new System.Windows.Forms.DataGridView();
            this.cmsPeople = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbRecords = new System.Windows.Forms.Label();
            this.cbFilter = new System.Windows.Forms.ComboBox();
            this.txtFilterValue = new System.Windows.Forms.TextBox();
            this.tsmShow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAddNewPerson = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSendEmail = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPhooneCall = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddNewPerson = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pbPersonImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPeople)).BeginInit();
            this.cmsPeople.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPersonImage)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Myanmar Text", 12F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(18, 324);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 29);
            this.label3.TabIndex = 105;
            this.label3.Text = "Filter By : ";
            // 
            // dgvPeople
            // 
            this.dgvPeople.AllowUserToAddRows = false;
            this.dgvPeople.AllowUserToDeleteRows = false;
            this.dgvPeople.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPeople.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPeople.BackgroundColor = System.Drawing.Color.LightGray;
            this.dgvPeople.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.PowderBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSlateGray;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPeople.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPeople.ColumnHeadersHeight = 60;
            this.dgvPeople.ContextMenuStrip = this.cmsPeople;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightSlateGray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPeople.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPeople.EnableHeadersVisualStyles = false;
            this.dgvPeople.Location = new System.Drawing.Point(21, 384);
            this.dgvPeople.Name = "dgvPeople";
            this.dgvPeople.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Myanmar Text", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPeople.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPeople.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dgvPeople.RowTemplate.Height = 27;
            this.dgvPeople.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPeople.Size = new System.Drawing.Size(1587, 590);
            this.dgvPeople.TabIndex = 103;
            // 
            // cmsPeople
            // 
            this.cmsPeople.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmShow,
            this.toolStripSeparator1,
            this.tsmAddNewPerson,
            this.tsmEdit,
            this.tsmDelete,
            this.toolStripSeparator2,
            this.tsmSendEmail,
            this.tsmPhooneCall});
            this.cmsPeople.Name = "cmsPeople";
            this.cmsPeople.Size = new System.Drawing.Size(179, 244);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(175, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(175, 6);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Myanmar Text", 36F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(609, 237);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(387, 85);
            this.label1.TabIndex = 102;
            this.label1.Text = "Manage People";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Myanmar Text", 12F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(18, 1019);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 29);
            this.label2.TabIndex = 108;
            this.label2.Text = "RECORDS : ";
            // 
            // lbRecords
            // 
            this.lbRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbRecords.Font = new System.Drawing.Font("Myanmar Text", 12F, System.Drawing.FontStyle.Bold);
            this.lbRecords.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lbRecords.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbRecords.Location = new System.Drawing.Point(112, 1018);
            this.lbRecords.Name = "lbRecords";
            this.lbRecords.Size = new System.Drawing.Size(59, 29);
            this.lbRecords.TabIndex = 111;
            this.lbRecords.Text = "??";
            // 
            // cbFilter
            // 
            this.cbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilter.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cbFilter.FormattingEnabled = true;
            this.cbFilter.Items.AddRange(new object[] {
            "None",
            "Person ID",
            "National No.",
            "First Name",
            "Second Name",
            "Third Name",
            "Last Name",
            "Nationality",
            "Gendor",
            "Phone",
            "Email"});
            this.cbFilter.Location = new System.Drawing.Point(111, 323);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(210, 28);
            this.cbFilter.TabIndex = 113;
            this.cbFilter.SelectedIndexChanged += new System.EventHandler(this.cbFilter_SelectedIndexChanged);
            // 
            // txtFilterValue
            // 
            this.txtFilterValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilterValue.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtFilterValue.Location = new System.Drawing.Point(328, 323);
            this.txtFilterValue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFilterValue.Name = "txtFilterValue";
            this.txtFilterValue.Size = new System.Drawing.Size(256, 27);
            this.txtFilterValue.TabIndex = 112;
            this.txtFilterValue.TextChanged += new System.EventHandler(this.txtFilterValue_TextChanged);
            this.txtFilterValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilterValue_KeyPress);
            // 
            // tsmShow
            // 
            this.tsmShow.Image = global::DVLD__Driving_License_Management_System_.Properties.Resources.PersonDetails_32;
            this.tsmShow.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmShow.Name = "tsmShow";
            this.tsmShow.Size = new System.Drawing.Size(178, 38);
            this.tsmShow.Text = "Show Details";
            this.tsmShow.Click += new System.EventHandler(this.tsmShow_Click);
            // 
            // tsmAddNewPerson
            // 
            this.tsmAddNewPerson.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmAddNewPerson.Name = "tsmAddNewPerson";
            this.tsmAddNewPerson.Size = new System.Drawing.Size(178, 38);
            this.tsmAddNewPerson.Text = "Add New Person";
            this.tsmAddNewPerson.Click += new System.EventHandler(this.tsmAddNewPerson_Click);
            // 
            // tsmEdit
            // 
            this.tsmEdit.Image = global::DVLD__Driving_License_Management_System_.Properties.Resources.edit_32;
            this.tsmEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmEdit.Name = "tsmEdit";
            this.tsmEdit.Size = new System.Drawing.Size(178, 38);
            this.tsmEdit.Text = "Edit";
            this.tsmEdit.Click += new System.EventHandler(this.tsmEdit_Click);
            // 
            // tsmDelete
            // 
            this.tsmDelete.Image = global::DVLD__Driving_License_Management_System_.Properties.Resources.Delete_32;
            this.tsmDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmDelete.Name = "tsmDelete";
            this.tsmDelete.Size = new System.Drawing.Size(178, 38);
            this.tsmDelete.Text = "Delete";
            this.tsmDelete.Click += new System.EventHandler(this.tsmDelete_Click);
            // 
            // tsmSendEmail
            // 
            this.tsmSendEmail.Image = global::DVLD__Driving_License_Management_System_.Properties.Resources.send_email_32;
            this.tsmSendEmail.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmSendEmail.Name = "tsmSendEmail";
            this.tsmSendEmail.Size = new System.Drawing.Size(178, 38);
            this.tsmSendEmail.Text = "Send Email";
            // 
            // tsmPhooneCall
            // 
            this.tsmPhooneCall.Image = global::DVLD__Driving_License_Management_System_.Properties.Resources.call_32;
            this.tsmPhooneCall.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmPhooneCall.Name = "tsmPhooneCall";
            this.tsmPhooneCall.Size = new System.Drawing.Size(178, 38);
            this.tsmPhooneCall.Text = "Phoone Call";
            // 
            // btnAddNewPerson
            // 
            this.btnAddNewPerson.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNewPerson.AutoSize = true;
            this.btnAddNewPerson.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddNewPerson.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.btnAddNewPerson.Image = ((System.Drawing.Image)(resources.GetObject("btnAddNewPerson.Image")));
            this.btnAddNewPerson.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAddNewPerson.Location = new System.Drawing.Point(1553, 309);
            this.btnAddNewPerson.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddNewPerson.Name = "btnAddNewPerson";
            this.btnAddNewPerson.Size = new System.Drawing.Size(55, 51);
            this.btnAddNewPerson.TabIndex = 101;
            this.btnAddNewPerson.UseVisualStyleBackColor = true;
            this.btnAddNewPerson.Click += new System.EventHandler(this.btnAddNewPerson_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F);
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClose.Location = new System.Drawing.Point(1504, 1009);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(104, 39);
            this.btnClose.TabIndex = 100;
            this.btnClose.Text = "Close";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pbPersonImage
            // 
            this.pbPersonImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbPersonImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbPersonImage.Image = global::DVLD__Driving_License_Management_System_.Properties.Resources.networking_People;
            this.pbPersonImage.InitialImage = null;
            this.pbPersonImage.Location = new System.Drawing.Point(634, 30);
            this.pbPersonImage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbPersonImage.Name = "pbPersonImage";
            this.pbPersonImage.Size = new System.Drawing.Size(313, 189);
            this.pbPersonImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPersonImage.TabIndex = 99;
            this.pbPersonImage.TabStop = false;
            // 
            // frmListPeople
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1629, 1061);
            this.Controls.Add(this.cbFilter);
            this.Controls.Add(this.txtFilterValue);
            this.Controls.Add(this.lbRecords);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvPeople);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddNewPerson);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pbPersonImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmListPeople";
            this.Text = "frmPeople";
            this.Load += new System.EventHandler(this.frmPeople_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPeople)).EndInit();
            this.cmsPeople.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPersonImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pbPersonImage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvPeople;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddNewPerson;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbRecords;
        private System.Windows.Forms.ComboBox cbFilter;
        private System.Windows.Forms.TextBox txtFilterValue;
        private System.Windows.Forms.ContextMenuStrip cmsPeople;
        private System.Windows.Forms.ToolStripMenuItem tsmShow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmAddNewPerson;
        private System.Windows.Forms.ToolStripMenuItem tsmEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmSendEmail;
        private System.Windows.Forms.ToolStripMenuItem tsmPhooneCall;
    }
}