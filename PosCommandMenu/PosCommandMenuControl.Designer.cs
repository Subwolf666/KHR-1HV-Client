namespace PosCommandMenu
{
    partial class PosCommandMenuControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PosCommandMenuControl));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdMnuWrite = new System.Windows.Forms.ToolStripButton();
            this.cmdMnuRead = new System.Windows.Forms.ToolStripButton();
            this.cmdMnuHome = new System.Windows.Forms.ToolStripButton();
            this.cmdMnuTrim = new System.Windows.Forms.ToolStripButton();
            this.cmdMnuSleep = new System.Windows.Forms.ToolStripButton();
            this.cmdMnuSnapshot = new System.Windows.Forms.ToolStripButton();
            this.cmdMnuDecLink = new System.Windows.Forms.ToolStripButton();
            this.cmdMnuIncLink = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdMnuWrite,
            this.cmdMnuRead,
            this.cmdMnuHome,
            this.cmdMnuTrim,
            this.cmdMnuSleep,
            this.cmdMnuSnapshot,
            this.cmdMnuDecLink,
            this.cmdMnuIncLink});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(330, 32);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cmdMnuWrite
            // 
            this.cmdMnuWrite.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdMnuWrite.Image = ((System.Drawing.Image)(resources.GetObject("cmdMnuWrite.Image")));
            this.cmdMnuWrite.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdMnuWrite.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.cmdMnuWrite.Name = "cmdMnuWrite";
            this.cmdMnuWrite.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.cmdMnuWrite.Size = new System.Drawing.Size(39, 29);
            this.cmdMnuWrite.Text = "toolStripButton1";
            this.cmdMnuWrite.ToolTipText = "Write to RB-100";
            // 
            // cmdMnuRead
            // 
            this.cmdMnuRead.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdMnuRead.Image = ((System.Drawing.Image)(resources.GetObject("cmdMnuRead.Image")));
            this.cmdMnuRead.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdMnuRead.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.cmdMnuRead.Name = "cmdMnuRead";
            this.cmdMnuRead.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.cmdMnuRead.Size = new System.Drawing.Size(39, 29);
            this.cmdMnuRead.Text = "toolStripButton2";
            this.cmdMnuRead.ToolTipText = "Read from RB-100";
            // 
            // cmdMnuHome
            // 
            this.cmdMnuHome.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdMnuHome.Image = ((System.Drawing.Image)(resources.GetObject("cmdMnuHome.Image")));
            this.cmdMnuHome.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdMnuHome.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.cmdMnuHome.Name = "cmdMnuHome";
            this.cmdMnuHome.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.cmdMnuHome.Size = new System.Drawing.Size(39, 29);
            this.cmdMnuHome.Text = "toolStripButton1";
            this.cmdMnuHome.ToolTipText = "Home position";
            // 
            // cmdMnuTrim
            // 
            this.cmdMnuTrim.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdMnuTrim.Image = ((System.Drawing.Image)(resources.GetObject("cmdMnuTrim.Image")));
            this.cmdMnuTrim.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdMnuTrim.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.cmdMnuTrim.Name = "cmdMnuTrim";
            this.cmdMnuTrim.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.cmdMnuTrim.Size = new System.Drawing.Size(39, 29);
            this.cmdMnuTrim.Text = "toolStripButton1";
            this.cmdMnuTrim.ToolTipText = "Trim position";
            // 
            // cmdMnuSleep
            // 
            this.cmdMnuSleep.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdMnuSleep.Image = ((System.Drawing.Image)(resources.GetObject("cmdMnuSleep.Image")));
            this.cmdMnuSleep.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdMnuSleep.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.cmdMnuSleep.Name = "cmdMnuSleep";
            this.cmdMnuSleep.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.cmdMnuSleep.Size = new System.Drawing.Size(39, 29);
            this.cmdMnuSleep.Text = "toolStripButton1";
            this.cmdMnuSleep.ToolTipText = "Sleep";
            // 
            // cmdMnuSnapshot
            // 
            this.cmdMnuSnapshot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdMnuSnapshot.Image = ((System.Drawing.Image)(resources.GetObject("cmdMnuSnapshot.Image")));
            this.cmdMnuSnapshot.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdMnuSnapshot.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.cmdMnuSnapshot.Name = "cmdMnuSnapshot";
            this.cmdMnuSnapshot.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.cmdMnuSnapshot.Size = new System.Drawing.Size(39, 29);
            this.cmdMnuSnapshot.Text = "toolStripButton1";
            this.cmdMnuSnapshot.ToolTipText = "Snapshot";
            // 
            // cmdMnuDecLink
            // 
            this.cmdMnuDecLink.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdMnuDecLink.Image = ((System.Drawing.Image)(resources.GetObject("cmdMnuDecLink.Image")));
            this.cmdMnuDecLink.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdMnuDecLink.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.cmdMnuDecLink.Name = "cmdMnuDecLink";
            this.cmdMnuDecLink.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.cmdMnuDecLink.Size = new System.Drawing.Size(39, 29);
            this.cmdMnuDecLink.Text = "toolStripButton1";
            this.cmdMnuDecLink.ToolTipText = "Decrement link";
            // 
            // cmdMnuIncLink
            // 
            this.cmdMnuIncLink.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdMnuIncLink.Image = ((System.Drawing.Image)(resources.GetObject("cmdMnuIncLink.Image")));
            this.cmdMnuIncLink.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdMnuIncLink.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.cmdMnuIncLink.Name = "cmdMnuIncLink";
            this.cmdMnuIncLink.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.cmdMnuIncLink.Size = new System.Drawing.Size(39, 29);
            this.cmdMnuIncLink.Text = "toolStripButton1";
            this.cmdMnuIncLink.ToolTipText = "Increment link";
            // 
            // PosCommandMenuControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.toolStrip1);
            this.Name = "PosCommandMenuControl";
            this.Size = new System.Drawing.Size(330, 32);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton cmdMnuWrite;
        private System.Windows.Forms.ToolStripButton cmdMnuRead;
        private System.Windows.Forms.ToolStripButton cmdMnuHome;
        private System.Windows.Forms.ToolStripButton cmdMnuTrim;
        private System.Windows.Forms.ToolStripButton cmdMnuSleep;
        private System.Windows.Forms.ToolStripButton cmdMnuSnapshot;
        private System.Windows.Forms.ToolStripButton cmdMnuDecLink;
        private System.Windows.Forms.ToolStripButton cmdMnuIncLink;
    }
}
