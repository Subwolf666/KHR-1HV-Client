namespace HorizontalSlider
{
    partial class SliderControl
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
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.rbFREE = new System.Windows.Forms.RadioButton();
            this.rbSET1 = new System.Windows.Forms.RadioButton();
            this.rbSET2 = new System.Windows.Forms.RadioButton();
            this.rbSET3 = new System.Windows.Forms.RadioButton();
            this.rbHigh = new System.Windows.Forms.RadioButton();
            this.rbLow = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Location = new System.Drawing.Point(120, 3);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(205, 24);
            this.hScrollBar1.TabIndex = 0;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(75, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(40, 24);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "0";
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // rbFREE
            // 
            this.rbFREE.AutoSize = true;
            this.rbFREE.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbFREE.Location = new System.Drawing.Point(120, 6);
            this.rbFREE.Name = "rbFREE";
            this.rbFREE.Size = new System.Drawing.Size(43, 20);
            this.rbFREE.TabIndex = 3;
            this.rbFREE.TabStop = true;
            this.rbFREE.Text = "FR";
            this.rbFREE.UseVisualStyleBackColor = true;
            // 
            // rbSET1
            // 
            this.rbSET1.AutoSize = true;
            this.rbSET1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSET1.Location = new System.Drawing.Point(175, 6);
            this.rbSET1.Name = "rbSET1";
            this.rbSET1.Size = new System.Drawing.Size(42, 20);
            this.rbSET1.TabIndex = 4;
            this.rbSET1.TabStop = true;
            this.rbSET1.Text = "S1";
            this.rbSET1.UseVisualStyleBackColor = true;
            // 
            // rbSET2
            // 
            this.rbSET2.AutoSize = true;
            this.rbSET2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSET2.Location = new System.Drawing.Point(229, 6);
            this.rbSET2.Name = "rbSET2";
            this.rbSET2.Size = new System.Drawing.Size(42, 20);
            this.rbSET2.TabIndex = 5;
            this.rbSET2.TabStop = true;
            this.rbSET2.Text = "S2";
            this.rbSET2.UseVisualStyleBackColor = true;
            // 
            // rbSET3
            // 
            this.rbSET3.AutoSize = true;
            this.rbSET3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSET3.Location = new System.Drawing.Point(286, 6);
            this.rbSET3.Name = "rbSET3";
            this.rbSET3.Size = new System.Drawing.Size(42, 20);
            this.rbSET3.TabIndex = 6;
            this.rbSET3.TabStop = true;
            this.rbSET3.Text = "S3";
            this.rbSET3.UseVisualStyleBackColor = true;
            // 
            // rbHigh
            // 
            this.rbHigh.AutoSize = true;
            this.rbHigh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.rbHigh.Location = new System.Drawing.Point(141, 6);
            this.rbHigh.Name = "rbHigh";
            this.rbHigh.Size = new System.Drawing.Size(71, 20);
            this.rbHigh.TabIndex = 7;
            this.rbHigh.TabStop = true;
            this.rbHigh.Text = "+5V (H)";
            this.rbHigh.UseVisualStyleBackColor = true;
            // 
            // rbLow
            // 
            this.rbLow.AutoSize = true;
            this.rbLow.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.rbLow.Location = new System.Drawing.Point(233, 6);
            this.rbLow.Name = "rbLow";
            this.rbLow.Size = new System.Drawing.Size(62, 20);
            this.rbLow.TabIndex = 8;
            this.rbLow.TabStop = true;
            this.rbLow.Text = "0V (L)";
            this.rbLow.UseVisualStyleBackColor = true;
            // 
            // SliderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.rbLow);
            this.Controls.Add(this.rbHigh);
            this.Controls.Add(this.rbSET3);
            this.Controls.Add(this.rbSET2);
            this.Controls.Add(this.rbSET1);
            this.Controls.Add(this.rbFREE);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hScrollBar1);
            this.Name = "SliderControl";
            this.Size = new System.Drawing.Size(330, 32);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RadioButton rbFREE;
        private System.Windows.Forms.RadioButton rbSET1;
        private System.Windows.Forms.RadioButton rbSET2;
        private System.Windows.Forms.RadioButton rbSET3;
        private System.Windows.Forms.RadioButton rbHigh;
        private System.Windows.Forms.RadioButton rbLow;
    }
}
