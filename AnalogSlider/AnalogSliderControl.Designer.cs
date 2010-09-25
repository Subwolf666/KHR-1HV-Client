namespace AnalogSlider
{
    partial class AnalogSliderControl
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
            this.lblLabel = new System.Windows.Forms.Label();
            this.Slider = new System.Windows.Forms.HScrollBar();
            this.tbRef = new System.Windows.Forms.TextBox();
            this.btnAuto = new System.Windows.Forms.Button();
            this.tbMeasure = new System.Windows.Forms.TextBox();
            this.tbAmount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblLabel
            // 
            this.lblLabel.AutoSize = true;
            this.lblLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLabel.Location = new System.Drawing.Point(-4, 16);
            this.lblLabel.Name = "lblLabel";
            this.lblLabel.Size = new System.Drawing.Size(20, 15);
            this.lblLabel.TabIndex = 0;
            this.lblLabel.Text = "lbl";
            // 
            // Slider
            // 
            this.Slider.Location = new System.Drawing.Point(78, 14);
            this.Slider.Minimum = -100;
            this.Slider.Name = "Slider";
            this.Slider.Size = new System.Drawing.Size(205, 20);
            this.Slider.TabIndex = 1;
            this.Slider.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Slider_Scroll);
            // 
            // tbRef
            // 
            this.tbRef.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRef.Location = new System.Drawing.Point(22, 14);
            this.tbRef.Name = "tbRef";
            this.tbRef.Size = new System.Drawing.Size(50, 20);
            this.tbRef.TabIndex = 2;
            this.tbRef.Text = "0";
            this.tbRef.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbRef.TextChanged += new System.EventHandler(this.tbRef_TextChanged);
            this.tbRef.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbRef_KeyDown);
            // 
            // btnAuto
            // 
            this.btnAuto.Location = new System.Drawing.Point(282, 14);
            this.btnAuto.Name = "btnAuto";
            this.btnAuto.Size = new System.Drawing.Size(45, 20);
            this.btnAuto.TabIndex = 3;
            this.btnAuto.Text = "AUTO";
            this.btnAuto.UseVisualStyleBackColor = true;
            this.btnAuto.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnAuto_MouseClick);
            // 
            // tbMeasure
            // 
            this.tbMeasure.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMeasure.Location = new System.Drawing.Point(332, 14);
            this.tbMeasure.Name = "tbMeasure";
            this.tbMeasure.Size = new System.Drawing.Size(50, 20);
            this.tbMeasure.TabIndex = 2;
            this.tbMeasure.Text = "0";
            this.tbMeasure.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbAmount
            // 
            this.tbAmount.BackColor = System.Drawing.Color.Yellow;
            this.tbAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbAmount.Location = new System.Drawing.Point(387, 14);
            this.tbAmount.Name = "tbAmount";
            this.tbAmount.Size = new System.Drawing.Size(50, 20);
            this.tbAmount.TabIndex = 2;
            this.tbAmount.Text = "0";
            this.tbAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ref";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(332, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Measure";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(387, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Amount";
            // 
            // AnalogSliderControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.btnAuto);
            this.Controls.Add(this.tbAmount);
            this.Controls.Add(this.tbMeasure);
            this.Controls.Add(this.tbRef);
            this.Controls.Add(this.Slider);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblLabel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AnalogSliderControl";
            this.Size = new System.Drawing.Size(437, 34);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLabel;
        private System.Windows.Forms.HScrollBar Slider;
        private System.Windows.Forms.TextBox tbRef;
        private System.Windows.Forms.Button btnAuto;
        private System.Windows.Forms.TextBox tbMeasure;
        private System.Windows.Forms.TextBox tbAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}
