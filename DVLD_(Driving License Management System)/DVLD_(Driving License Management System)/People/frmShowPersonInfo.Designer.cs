namespace DVLD__Driving_License_Management_System_.People
{
    partial class frmShowPersonInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShowPersonInfo));
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose2 = new System.Windows.Forms.Button();
            this.ctrPersonCard1 = new DVLD__Driving_License_Management_System_.People.ctrPersonCard();
            this.panelMoveForm = new System.Windows.Forms.Panel();
            this.btnClose1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Myanmar Text", 27.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(382, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(276, 66);
            this.label1.TabIndex = 103;
            this.label1.Text = "Person Details";
            // 
            // btnClose2
            // 
            this.btnClose2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClose2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F);
            this.btnClose2.Image = ((System.Drawing.Image)(resources.GetObject("btnClose2.Image")));
            this.btnClose2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClose2.Location = new System.Drawing.Point(961, 501);
            this.btnClose2.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose2.Name = "btnClose2";
            this.btnClose2.Size = new System.Drawing.Size(104, 39);
            this.btnClose2.TabIndex = 106;
            this.btnClose2.Text = "Close";
            this.btnClose2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose2.UseVisualStyleBackColor = true;
            this.btnClose2.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ctrPersonCard1
            // 
            this.ctrPersonCard1.BackColor = System.Drawing.Color.White;
            this.ctrPersonCard1.Location = new System.Drawing.Point(12, 85);
            this.ctrPersonCard1.Name = "ctrPersonCard1";
            this.ctrPersonCard1.Size = new System.Drawing.Size(1053, 400);
            this.ctrPersonCard1.TabIndex = 104;
            // 
            // panelMoveForm
            // 
            this.panelMoveForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(115)))), ((int)(((byte)(151)))));
            this.panelMoveForm.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMoveForm.Location = new System.Drawing.Point(0, 0);
            this.panelMoveForm.Name = "panelMoveForm";
            this.panelMoveForm.Size = new System.Drawing.Size(1079, 32);
            this.panelMoveForm.TabIndex = 107;
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
            this.btnClose1.Location = new System.Drawing.Point(1039, 0);
            this.btnClose1.Name = "btnClose1";
            this.btnClose1.Size = new System.Drawing.Size(40, 32);
            this.btnClose1.TabIndex = 108;
            this.btnClose1.UseVisualStyleBackColor = false;
            this.btnClose1.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmShowPersonInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1079, 549);
            this.Controls.Add(this.btnClose1);
            this.Controls.Add(this.panelMoveForm);
            this.Controls.Add(this.btnClose2);
            this.Controls.Add(this.ctrPersonCard1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmShowPersonInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmShowPersonInfo";
            this.Load += new System.EventHandler(this.frmShowPersonInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private ctrPersonCard ctrPersonCard1;
        private System.Windows.Forms.Button btnClose2;
        private System.Windows.Forms.Panel panelMoveForm;
        private System.Windows.Forms.Button btnClose1;
    }
}