using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace LabeledTextBox
{
    public partial class LblTextBoxControl : UserControl
    {
        public LblTextBoxControl()
        {
            InitializeComponent();
        }

        private string _labeledTextBoxLabel = string.Empty;
        private string _labeledTextBoxText = string.Empty;

        // our label
        protected Label _label = null;
        // caption of our label
        protected string _LabelText = "";

        public string labelTextBoxLabel
        {
            get
            { return _labeledTextBoxLabel; }

            set
            {
                _labeledTextBoxLabel = value;
                label1.Text = _labeledTextBoxLabel;
            }
        }

        public string labelTextBoxText
        {
            get { return textBox1.Text; }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _labeledTextBoxText = textBox1.Text;
            }
        }
    }
}
