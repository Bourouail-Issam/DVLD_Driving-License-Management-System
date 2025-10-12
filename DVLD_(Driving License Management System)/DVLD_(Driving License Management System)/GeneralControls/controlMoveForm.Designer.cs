namespace DVLD__Driving_License_Management_System_
{
    partial class controlMoveForm
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(controlMoveForm));
            this.panelMoveForm = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panelMoveForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMoveForm
            // 
            this.panelMoveForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(93)))), ((int)(((byte)(127)))));
            this.panelMoveForm.Controls.Add(this.button1);
            this.panelMoveForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMoveForm.Location = new System.Drawing.Point(0, 0);
            this.panelMoveForm.Name = "panelMoveForm";
            this.panelMoveForm.Size = new System.Drawing.Size(746, 30);
            this.panelMoveForm.TabIndex = 252;
            this.panelMoveForm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelMoveForm_MouseDown);
            this.panelMoveForm.MouseEnter += new System.EventHandler(this.panelMoveForm_MouseEnter);
            this.panelMoveForm.MouseLeave += new System.EventHandler(this.panelMoveForm_MouseLeave);
            this.panelMoveForm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelMoveForm_MouseMove);
            this.panelMoveForm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelMoveForm_MouseUp);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(149)))), ((int)(((byte)(191)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(714, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(20, 18);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // controlMoveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelMoveForm);
            this.Name = "controlMoveForm";
            this.Size = new System.Drawing.Size(746, 30);
            this.panelMoveForm.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panelMoveForm;
    }
}
