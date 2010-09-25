namespace KHR_1HV
{
    partial class KHR_1HV_DataTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KHR_1HV_DataTable));
            this.tsDataTable = new System.Windows.Forms.ToolStrip();
            this.tsRead = new System.Windows.Forms.ToolStripButton();
            this.tsDelete = new System.Windows.Forms.ToolStripButton();
            this.lvDataTable = new System.Windows.Forms.ListView();
            this.lvNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvControl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tsDataTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsDataTable
            // 
            this.tsDataTable.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsDataTable.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsRead,
            this.tsDelete});
            this.tsDataTable.Location = new System.Drawing.Point(0, 0);
            this.tsDataTable.Name = "tsDataTable";
            this.tsDataTable.Size = new System.Drawing.Size(624, 27);
            this.tsDataTable.TabIndex = 0;
            this.tsDataTable.Text = "toolStrip1";
            // 
            // tsRead
            // 
            this.tsRead.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsRead.Image = ((System.Drawing.Image)(resources.GetObject("tsRead.Image")));
            this.tsRead.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsRead.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tsRead.Name = "tsRead";
            this.tsRead.Size = new System.Drawing.Size(36, 24);
            this.tsRead.Text = "Read";
            this.tsRead.ToolTipText = "Read DataTable";
            this.tsRead.Click += new System.EventHandler(this.tsRead_Click);
            // 
            // tsDelete
            // 
            this.tsDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsDelete.Image")));
            this.tsDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsDelete.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tsDelete.Name = "tsDelete";
            this.tsDelete.Size = new System.Drawing.Size(36, 24);
            this.tsDelete.Text = "Delete";
            this.tsDelete.ToolTipText = "Delete Motion";
            this.tsDelete.Click += new System.EventHandler(this.tsDelete_Click);
            // 
            // lvDataTable
            // 
            this.lvDataTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvDataTable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvNo,
            this.lvName,
            this.lvCount,
            this.lvDate,
            this.lvControl});
            this.lvDataTable.FullRowSelect = true;
            this.lvDataTable.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvDataTable.Location = new System.Drawing.Point(0, 27);
            this.lvDataTable.MultiSelect = false;
            this.lvDataTable.Name = "lvDataTable";
            this.lvDataTable.Size = new System.Drawing.Size(624, 367);
            this.lvDataTable.TabIndex = 2;
            this.lvDataTable.UseCompatibleStateImageBehavior = false;
            this.lvDataTable.View = System.Windows.Forms.View.Details;
            // 
            // lvNo
            // 
            this.lvNo.Text = "No";
            // 
            // lvName
            // 
            this.lvName.Text = "Name";
            this.lvName.Width = 382;
            // 
            // lvCount
            // 
            this.lvCount.Text = "Count";
            // 
            // lvDate
            // 
            this.lvDate.Text = "Date";
            // 
            // lvControl
            // 
            this.lvControl.Text = "Control";
            // 
            // KHR_1HV_DataTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 394);
            this.Controls.Add(this.lvDataTable);
            this.Controls.Add(this.tsDataTable);
            this.Name = "KHR_1HV_DataTable";
            this.Text = "KHR_1HV_DataTable";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KHR_1HV_DataTable_FormClosing);
            this.Load += new System.EventHandler(this.KHR_1HV_DataTable_Load);
            this.tsDataTable.ResumeLayout(false);
            this.tsDataTable.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsDataTable;
        private System.Windows.Forms.ToolStripButton tsRead;
        private System.Windows.Forms.ToolStripButton tsDelete;
        private System.Windows.Forms.ListView lvDataTable;
        private System.Windows.Forms.ColumnHeader lvNo;
        private System.Windows.Forms.ColumnHeader lvName;
        private System.Windows.Forms.ColumnHeader lvCount;
        private System.Windows.Forms.ColumnHeader lvDate;
        private System.Windows.Forms.ColumnHeader lvControl;

    }
}