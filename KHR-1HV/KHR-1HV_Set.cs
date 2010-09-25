using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KHR_1HV
{
    public partial class KHR_1HV_Set : Form
    {
        public KHR_1HV_Set()
        {
            InitializeComponent();
        }

        private void KHR_1HV_Set_Load(object sender, EventArgs e)
        {
            sbSetLoopCounter.Minimum = 0;
            sbSetLoopCounter.Maximum = 264;
        }

        private void sbSetLoopCounter_ValueChanged(object sender, EventArgs e)
        {
            rbSetLoopCounter.Checked = true;
            tbSetLoopCounter.Text = sbSetLoopCounter.Value.ToString();
        }

        private void sbSetLoopCounter_Scroll(object sender, ScrollEventArgs e)
        {
            rbSetLoopCounter.Checked = true;
        }

        private string previousNumber;
        private void tbSetLoopCounter_KeyDown(object sender, KeyEventArgs e)
        {
            int number;
            if (e.KeyCode == Keys.Enter)
            {
                if (isNumeric(tbSetLoopCounter.Text))
                {
                    number = int.Parse(tbSetLoopCounter.Text);
                    if (number < 0)
                        number = 0;
                    if (number > 255)
                        number = 255;
                }
                else
                {
                    number = Convert.ToInt32(previousNumber);
                }
                tbSetLoopCounter.Text = number.ToString();
                sbSetLoopCounter.Value = number;
                previousNumber = number.ToString();
            }
        }

        private System.Boolean isNumeric(System.Object Expression)
        {
            if (Expression == null || Expression is DateTime)
                return false;

            if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
                return true;

            try
            {
                if (Expression is string)
                    Double.Parse(Expression as string);
                else
                    Double.Parse(Expression.ToString());
                return true;
            }
            catch { } // just dismiss errors but return false
            return false;
        }

        //==============================
        //  PROPERTIES
        //==============================
        public int SetFunction
        {
            get { return 1; }
            set { }
        }
        private int _LoopCounter;
        public int LoopCounter
        {
            get { return _LoopCounter; }
            set { _LoopCounter = value; }
        }
    }
}
