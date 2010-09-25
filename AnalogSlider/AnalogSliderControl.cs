using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AnalogSlider
{
    public partial class AnalogSliderControl : UserControl
    {
        public AnalogSliderControl()
        {
            InitializeComponent();
            Slider.Minimum = _sliderMinimumRange;
            Slider.Maximum = _sliderMaximumRange;
        }

        // 
        //
        private int _sliderMinimumRange = -2048;
        public int sliderMinimumRange
        {
            get { return _sliderMinimumRange; }
            set
            {
                _sliderMinimumRange = value;
                Slider.Minimum = _sliderMinimumRange;
            }
        }
        //
        //
        private int _sliderMaximumRange = 2048;
        public int sliderMaximumRange
        {
            get
            { return _sliderMaximumRange; }
            set
            {
                _sliderMaximumRange = value;
                Slider.Maximum = _sliderMaximumRange;
            }
        }
        // Label of the slider
        //
        private string _sliderLabel = "Label";
        public string Label
        {
            get { return _sliderLabel; }
            set
            {
                _sliderLabel = value;
                lblLabel.Text = _sliderLabel;
            }
        }
        // Reference value of the slider
        // read/write
        private int _sliderRefValue = 0;
        public int Reference
        {
            get { return _sliderRefValue; }
            set
            {
                _sliderRefValue = value;
                tbRef.Text = _sliderRefValue.ToString();
                Slider.Value = _sliderRefValue;
            }
        }
        // Slider value
        // read/write
        private void Slider_Scroll(object sender, ScrollEventArgs e)
        {
            _sliderRefValue = Slider.Value;
            tbRef.Text = _sliderRefValue.ToString();
            tbAmount.Text = (_MeasureValue - _sliderRefValue).ToString();
        }

        // Sensordata
        // read/write (write niet vanuit deze usercontrol,alleen
        // bij het berekenen).
        private int _MeasureValue;
        public int Measure
        {
            get { return _MeasureValue; }
            set
            {
                _MeasureValue = value;
                tbMeasure.Text = _MeasureValue.ToString();
                tbAmount.Text = (_MeasureValue - _sliderRefValue).ToString();
            }
        }

        private void tbRef_KeyDown(object sender, KeyEventArgs e)
        {
            int number;
            if (e.KeyCode == Keys.Enter)
            {
                if (isNumeric(tbRef.Text))
                {
                    number = int.Parse(tbRef.Text);
                    if (number < _sliderMinimumRange)
                        number = Reference;
                    if (number > _sliderMaximumRange)
                        number = Reference;

                    _sliderRefValue = number;
                    Slider.Value = _sliderRefValue;
                    tbRef.Text = _sliderRefValue.ToString();
                    tbAmount.Text = (_MeasureValue - _sliderRefValue).ToString();
                }
                else
                {
                    _sliderRefValue = Slider.Value;
                    tbRef.Text = _sliderRefValue.ToString();
                }
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

        private void btnAuto_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                tbAmount.Text = (0).ToString();
                tbRef.Text = tbMeasure.Text;
                _sliderRefValue = int.Parse(tbMeasure.Text);
                Slider.Value = _sliderRefValue;
            }
        }

        private void tbRef_TextChanged(object sender, EventArgs e)
        {
            //tbAmount.Text = (_MeasureValue - _sliderRefValue).ToString();
        }
    }
}
