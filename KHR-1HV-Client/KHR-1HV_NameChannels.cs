using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LabeledTextBox;

namespace KHR_1HV
{
    public partial class KHR_1HV_NameChannels : System.Windows.Forms.Form
    {
        private LabeledTextBox.LblTextBoxControl[] labeledTextBoxArray; // Declaring array of Label
        private int lblNum = 24;

        public KHR_1HV_NameChannels()
        {
            InitializeComponent();
            ShowLabeledTextBox();
        }

        private void ShowLabeledTextBox()
        {
            int xPos = 0;
            int yPos = 10;
            // assign number of controls 
            labeledTextBoxArray = new LabeledTextBox.LblTextBoxControl[lblNum];
            for (int i = 0; i < lblNum; i++)
            {
                // Initialize one variable
                labeledTextBoxArray[i] = new LabeledTextBox.LblTextBoxControl();
            }

            int n = 0;
            while (n < lblNum)
            {
                labeledTextBoxArray[n].Tag = n + 1;
                labeledTextBoxArray[n].labelTextBoxLabel = "CH" + labeledTextBoxArray[n].Tag.ToString();
                if (yPos > 239) // Eight Labels in one column
                {
                    yPos = 10;
                    xPos = xPos + labeledTextBoxArray[n].Width;
                }
                labeledTextBoxArray[n].Left = xPos;
                labeledTextBoxArray[n].Top = yPos;
                yPos = yPos + labeledTextBoxArray[n].Height;
                this.Controls.Add(labeledTextBoxArray[n]);
                n++;
            }  
        }

        private static string[] _names = new string[24];
        private string[] getNames()
        {
            for (int i = 0; i < lblNum; i++)
            {
                _names[i] = labeledTextBoxArray[i].labelTextBoxText;
            }
            return _names;
        }

        public string[] getChannelName
        {
            get
            {
                return _names;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _names = getNames();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
