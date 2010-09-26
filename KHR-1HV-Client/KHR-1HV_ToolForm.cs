using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Roboard;
using Roboard.Events;
using Ini;

namespace KHR_1HV
{
    public partial class KHR_1HV_ToolForm : Form
    {
        private int selectedMotionIndex;
        private KHR_1HV_ToolForm toolForm;
        private IniFile motionData;

        public KHR_1HV_ToolForm()
        {
            InitializeComponent();
            selectedMotionIndex = -1;
        }

        private void KHR_1HV_ToolForm_Load(object sender, EventArgs e)
        {
            toolForm = (KHR_1HV_ToolForm)sender;

            for (int i = 0; i < StaticUtilities.numberOfMotions; i++)
            {
                comboBox1.Items.Add(string.Format("{0}   {1}", Roboard.DataTable.motionDataTable[i, 0], Roboard.DataTable.motionDataTable[i, 3]));
            }
            if (selectedMotionIndex >= 0)
                comboBox1.SelectedIndex = selectedMotionIndex;
            else
                comboBox1.SelectedIndex = -1;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            selectedMotionIndex = comboBox1.SelectedIndex;
            if (selectedMotionIndex != -1)
            {
                string message = string.Format("OK to {0}?",toolForm.Text);
                if (MessageBox.Show(message, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    switch (toolForm.Text)
                    {
                        case "Delete":
                            NetworkFile deleteMotion = new NetworkFile();
                            deleteMotion.SelectedMotionIndex = this.selectedMotionIndex;
                            if (deleteMotion.Delete())
                            {
                                Roboard.DataTable.motionDataTable[selectedMotionIndex, 1] = string.Empty;  // name
                                Roboard.DataTable.motionDataTable[selectedMotionIndex, 2] = string.Format("{0}", 0); // count
                                Roboard.DataTable.motionDataTable[selectedMotionIndex, 3] = string.Format("--/--/---- --:--"); // date
                                Roboard.DataTable.motionDataTable[selectedMotionIndex, 4] = string.Format("{0}", 65535); // control
                                this.DialogResult = DialogResult.OK;
                            }
                            else
                                this.DialogResult = DialogResult.Abort;
                            break;
                        case "Play":
                            NetworkFile playMotion = new NetworkFile();
                            playMotion.SelectedMotionIndex = this.selectedMotionIndex;
                            playMotion.Play();
                            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                            break;
                        case "Read":
                            NetworkFile readMotion = new NetworkFile();
                            readMotion.SelectedMotionIndex = this.selectedMotionIndex;
                            if (readMotion.Read())
                            {
                                this.motionData = readMotion.MotionData;
                                this.DialogResult = DialogResult.OK;
                            }
                            else
                                this.DialogResult = DialogResult.Abort;
                            break;
                        case "Write":
                            this.Cursor = Cursors.WaitCursor;
                            NetworkFile writeMotion = new NetworkFile();
                            writeMotion.SelectedMotionIndex = this.selectedMotionIndex;
                            writeMotion.MotionData = this.motionData;
                            if (!writeMotion.Write())
                                return;

                            // and adjust my own dataTable
                            // by inserting the written motion
                            //
                            string currentDate = DateTime.Now.ToString("dd/MM/yyyy");
                            string currentTime = DateTime.Now.ToString("t");

                            Roboard.DataTable.motionDataTable[selectedMotionIndex, 1] = motionData[Roboard.StaticUtilities.SectionGraphicalEdit][Roboard.StaticUtilities.GraphicalEditName];
                            Roboard.DataTable.motionDataTable[selectedMotionIndex, 2] = string.Format("{0}", 0);
                            Roboard.DataTable.motionDataTable[selectedMotionIndex, 3] = string.Format("{0} {1}", currentDate, currentTime);
                            Roboard.DataTable.motionDataTable[selectedMotionIndex, 4] = motionData[Roboard.StaticUtilities.SectionGraphicalEdit][Roboard.StaticUtilities.GraphicalEditCtrl];
                            this.DialogResult = DialogResult.OK;
                            break;
                        default:
                            this.DialogResult = DialogResult.Cancel;
                            break;
                    }
//                    this.DialogResult = DialogResult.OK;
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                }
            }
            else
            {
                selectedMotionIndex = -1;
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        // Property
        //
        public string Label
        {
            set
            {
                this.label1.Text = value;
            }
        }

        // Property
        //
        public int SelectedMotionIndex
        {
            get
            {
                return this.selectedMotionIndex;
            }
            set
            {
                this.selectedMotionIndex = value;
            }
        }

        // Property
        //
        public IniFile Motion
        {
            get
            {
                return this.motionData;
            }
            set
            {
                this.motionData = value;
            }
        }
    }
}
