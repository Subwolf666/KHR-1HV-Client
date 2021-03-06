﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Roboard;
using Roboard.Events;

namespace KHR_1HV
{
    public partial class KHR_1HV_DataTable : Form
    {
        private TimeOut watchDogTimer;
        private bool _Done;

        public KHR_1HV_DataTable()
        {
            InitializeComponent();
        }

        private void KHR_1HV_DataTable_Load(object sender, EventArgs e)
        {
            watchDogTimer = new TimeOut();
            for (int i = 0; i < StaticUtilities.numberOfMotions; i++)
            {
                string[] str = new string[StaticUtilities.numberOfDataTableItems];
                ListViewItem itm;
                for (int j = 0; j < StaticUtilities.numberOfDataTableItems; j++)
                {
                    str[j] = Roboard.DataTable.motionDataTable[i, j];
                }
                itm = new ListViewItem(str);
                this.lvDataTable.Items.Add(itm);
            }
        }

        // Read the motion table from the server.
        //
        private void tsRead_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            _Done = this.readDataTable();

        }

        //
        // Needed for the cursor function.
        private bool readDataTable()
        {
            if (!Roboard.DataTable.Start())
                return false;
            watchDogTimer.Start(10000);
            while ((!Roboard.DataTable.Done) && (!watchDogTimer.Done)) ;
            this.constructDataTable();
            this.Cursor = Cursors.Default;
            return true;
        }

        // Delete a motion
        //
        private void tsDelete_Click(object sender, EventArgs e)
        {
            if (!Roboard.DataTable.Done)
                return;
            KHR_1HV_ToolMenu myDelete = new KHR_1HV_ToolMenu();
            myDelete.Text = "Delete";
            myDelete.Label = "Select the motion or scenario to be deleted";
            myDelete.ShowDialog();
            // Need to rebuild the ListView
            //
            if (myDelete.DialogResult == DialogResult.OK)
                this.constructDataTable();
        }

        // Method
        //
        private void constructDataTable()
        {
            // Empty the ListView DataTable.
            //
            if (this.lvDataTable.Items.Count > 0) // is the ListView already there
                for (int i = 0; i < StaticUtilities.numberOfMotions; i++)
                    this.lvDataTable.Items.RemoveAt(0);

            // Get the motions from MotionDataTable and put it in the ListView
            //
            for (int i = 0; i < StaticUtilities.numberOfMotions; i++)
            {
                string[] str = new string[StaticUtilities.numberOfDataTableItems];
                ListViewItem itm;
                for (int j = 0; j < StaticUtilities.numberOfDataTableItems; j++)
                    str[j] = Roboard.DataTable.motionDataTable[i, j];
                itm = new ListViewItem(str);
                this.lvDataTable.Items.Add(itm);
            }
        }

        // Property
        //
        public bool Done
        {
            get { return _Done; }
        }

        private void KHR_1HV_DataTable_FormClosing(object sender, FormClosingEventArgs e)
        {
            Roboard.DataTable.Stop();
        }
    }  
}
