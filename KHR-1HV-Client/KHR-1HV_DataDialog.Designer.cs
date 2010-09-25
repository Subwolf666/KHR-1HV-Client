namespace KHR_1HV
{
    partial class KHR_1HV_DataDialog
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
            this.lblDataName = new System.Windows.Forms.Label();
            this.tbDataName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbRemoteControlCode = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCapture = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblDataName
            // 
            this.lblDataName.AutoSize = true;
            this.lblDataName.Location = new System.Drawing.Point(12, 9);
            this.lblDataName.Name = "lblDataName";
            this.lblDataName.Size = new System.Drawing.Size(61, 13);
            this.lblDataName.TabIndex = 0;
            this.lblDataName.Text = "Data Name";
            // 
            // tbDataName
            // 
            this.tbDataName.Location = new System.Drawing.Point(12, 25);
            this.tbDataName.Name = "tbDataName";
            this.tbDataName.Size = new System.Drawing.Size(216, 20);
            this.tbDataName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Remote Control Code";
            // 
            // tbRemoteControlCode
            // 
            this.tbRemoteControlCode.Location = new System.Drawing.Point(12, 80);
            this.tbRemoteControlCode.Name = "tbRemoteControlCode";
            this.tbRemoteControlCode.Size = new System.Drawing.Size(125, 20);
            this.tbRemoteControlCode.TabIndex = 3;
            this.tbRemoteControlCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbRemoteControlCode_KeyDown);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(29, 153);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(132, 153);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCapture
            // 
            this.btnCapture.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnCapture.Location = new System.Drawing.Point(153, 78);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(75, 23);
            this.btnCapture.TabIndex = 7;
            this.btnCapture.Text = "Capture";
            this.btnCapture.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.CheckedChanged += new System.EventHandler(this.btnCapture_CheckedChanged);
            // 
            // KHR_1HV_DataDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 188);
            this.Controls.Add(this.btnCapture);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tbRemoteControlCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbDataName);
            this.Controls.Add(this.lblDataName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "KHR_1HV_DataDialog";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "DataDialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KHR_1HV_DataDialog_FormClosing);
            this.Load += new System.EventHandler(this.KHR_1HV_DataDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDataName;
        private System.Windows.Forms.TextBox tbDataName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbRemoteControlCode;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox btnCapture;
    }
}