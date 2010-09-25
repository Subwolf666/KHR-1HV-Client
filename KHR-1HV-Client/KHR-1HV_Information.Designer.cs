namespace KHR_1HV
{
    partial class KHR_1HV_Information
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KHR_1HV_Information));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCPUId = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.cbRCServoLib = new System.Windows.Forms.CheckBox();
            this.cbI2CLib = new System.Windows.Forms.CheckBox();
            this.cbADLib = new System.Windows.Forms.CheckBox();
            this.cbSPILib = new System.Windows.Forms.CheckBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(454, 105);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(35, 172);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "CPU ID:";
            // 
            // lblCPUId
            // 
            this.lblCPUId.AutoSize = true;
            this.lblCPUId.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCPUId.Location = new System.Drawing.Point(140, 172);
            this.lblCPUId.Name = "lblCPUId";
            this.lblCPUId.Size = new System.Drawing.Size(0, 15);
            this.lblCPUId.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(35, 199);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Board version:";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(140, 199);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(0, 15);
            this.lblVersion.TabIndex = 4;
            // 
            // cbRCServoLib
            // 
            this.cbRCServoLib.AutoSize = true;
            this.cbRCServoLib.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRCServoLib.Location = new System.Drawing.Point(38, 226);
            this.cbRCServoLib.Name = "cbRCServoLib";
            this.cbRCServoLib.Size = new System.Drawing.Size(98, 19);
            this.cbRCServoLib.TabIndex = 9;
            this.cbRCServoLib.Text = "RC Servo Lib";
            this.cbRCServoLib.UseVisualStyleBackColor = true;
            // 
            // cbI2CLib
            // 
            this.cbI2CLib.AutoSize = true;
            this.cbI2CLib.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbI2CLib.Location = new System.Drawing.Point(38, 257);
            this.cbI2CLib.Name = "cbI2CLib";
            this.cbI2CLib.Size = new System.Drawing.Size(65, 19);
            this.cbI2CLib.TabIndex = 9;
            this.cbI2CLib.Text = "I2C Lib";
            this.cbI2CLib.UseVisualStyleBackColor = true;
            // 
            // cbADLib
            // 
            this.cbADLib.AutoSize = true;
            this.cbADLib.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbADLib.Location = new System.Drawing.Point(38, 288);
            this.cbADLib.Name = "cbADLib";
            this.cbADLib.Size = new System.Drawing.Size(65, 19);
            this.cbADLib.TabIndex = 9;
            this.cbADLib.Text = "A/D Lib";
            this.cbADLib.UseVisualStyleBackColor = true;
            // 
            // cbSPILib
            // 
            this.cbSPILib.AutoSize = true;
            this.cbSPILib.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSPILib.Location = new System.Drawing.Point(38, 319);
            this.cbSPILib.Name = "cbSPILib";
            this.cbSPILib.Size = new System.Drawing.Size(65, 19);
            this.cbSPILib.TabIndex = 9;
            this.cbSPILib.Text = "SPI Lib";
            this.cbSPILib.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(-62, 51);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(544, 106);
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // KHR_1HV_Information
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(453, 377);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.cbSPILib);
            this.Controls.Add(this.cbADLib);
            this.Controls.Add(this.cbI2CLib);
            this.Controls.Add(this.cbRCServoLib);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCPUId);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "KHR_1HV_Information";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "KHR-1HV About";
            this.Load += new System.EventHandler(this.KHR_1HV_Information_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCPUId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.CheckBox cbRCServoLib;
        private System.Windows.Forms.CheckBox cbI2CLib;
        private System.Windows.Forms.CheckBox cbADLib;
        private System.Windows.Forms.CheckBox cbSPILib;
        private System.Windows.Forms.PictureBox pictureBox2;



    }
}