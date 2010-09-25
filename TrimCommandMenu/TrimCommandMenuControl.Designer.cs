namespace TrimCommandMenu
{
    partial class TrimCommandMenuControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrimCommandMenuControl));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsTrimSave = new System.Windows.Forms.ToolStripButton();
            this.tsTrimOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsTrimSave,
            this.tsTrimOpen});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(328, 32);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsTrimSave
            // 
            this.tsTrimSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsTrimSave.Image = ((System.Drawing.Image)(resources.GetObject("tsTrimSave.Image")));
            this.tsTrimSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsTrimSave.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tsTrimSave.Name = "tsTrimSave";
            this.tsTrimSave.Size = new System.Drawing.Size(36, 29);
            this.tsTrimSave.Text = "toolStripButton1";
            this.tsTrimSave.ToolTipText = "Save trim file";
            this.tsTrimSave.Click += new System.EventHandler(this.tsTrimSave_Click);
            // 
            // tsTrimOpen
            // 
            this.tsTrimOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsTrimOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsTrimOpen.Image")));
            this.tsTrimOpen.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsTrimOpen.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tsTrimOpen.Name = "tsTrimOpen";
            this.tsTrimOpen.Size = new System.Drawing.Size(36, 29);
            this.tsTrimOpen.Text = "toolStripButton1";
            this.tsTrimOpen.ToolTipText = "Open trim file";
            this.tsTrimOpen.Click += new System.EventHandler(this.tsTrimOpen_Click);
            // 
            // TrimCommandMenuControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.toolStrip1);
            this.Name = "TrimCommandMenuControl";
            this.Size = new System.Drawing.Size(328, 30);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsTrimOpen;
        private System.Windows.Forms.ToolStripButton tsTrimSave;
    }
}
