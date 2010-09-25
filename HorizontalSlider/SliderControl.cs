using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

public enum Function : int
{
    NONE = 0,
    SERVO,
    FREE,
    SET1,
    SET2,
    SET3,
    H,
    L
}


namespace HorizontalSlider
{

    public partial class SliderControl : UserControl
    {
        public SliderControl()
        {
            InitializeComponent();
            hScrollBar1.Minimum = _sliderMinimumRange;
            hScrollBar1.Maximum = _sliderMaximumRange;
        }

        private int _sliderMinimumRange = -800;
        public int sliderMinRange
        {
            get
            {
                return _sliderMinimumRange;
            }
            set
            {
                _sliderMinimumRange = value;
                hScrollBar1.Minimum = _sliderMinimumRange;
            }
        }

        private int _sliderMaximumRange = 800;
        public int sliderMaxRange
        {
            get
            {
                return _sliderMaximumRange;
            }
            set
            {
                _sliderMaximumRange = value;
                hScrollBar1.Maximum = _sliderMaximumRange;
            }
        }
        
        private string _sliderLabel = "CH";
        public string sliderLabel
        {
            get
            {
                return _sliderLabel;
            }
            set
            {
                _sliderLabel = value;
                label1.Text = _sliderLabel;
            }
        }

        // create and initialize
        // instance of enum
        Function _sliderFunction = Function.NONE;
        public int sliderFunction
        {
            get { return (int)_sliderFunction; }
            set 
            { 
                _sliderFunction = (Function)value;
                switch (_sliderFunction)
                {
                    case Function.NONE:
                        textBox1.Visible = false;
                        hScrollBar1.Visible = false;
                        rbFREE.Visible = false;
                        rbSET1.Visible = false;
                        rbSET2.Visible = false;
                        rbSET3.Visible = false;
                        rbHigh.Visible = false;
                        rbLow.Visible = false;
                        break;
                    case Function.SERVO:
                        textBox1.Visible = true;
                        hScrollBar1.Visible = true;
                        rbFREE.Visible = rbSET1.Visible = rbSET2.Visible = rbSET3.Visible = false;
                        rbHigh.Visible = rbLow.Visible = false;
                        break;
                    case Function.FREE:
                        textBox1.Visible = false;
                        hScrollBar1.Visible = false;
                        rbFREE.Visible = rbSET1.Visible = rbSET2.Visible = rbSET3.Visible = true;
                        rbHigh.Visible = rbLow.Visible = false;
                        rbFREE.Select();
                        break;
                    case Function.SET1:
                        textBox1.Visible = false;
                        hScrollBar1.Visible = false;
                        rbFREE.Visible = rbSET1.Visible = rbSET2.Visible = rbSET3.Visible = true;
                        rbHigh.Visible = rbLow.Visible = false;
                        rbSET1.Select();
                        break;
                    case Function.SET2:
                        textBox1.Visible = false;
                        hScrollBar1.Visible = false;
                        rbFREE.Visible = rbSET1.Visible = rbSET2.Visible = rbSET3.Visible = true;
                        rbHigh.Visible = rbLow.Visible = false;
                        rbSET2.Select();
                        break;
                    case Function.SET3:
                        textBox1.Visible = false;
                        hScrollBar1.Visible = false;
                        rbFREE.Visible = rbSET1.Visible = rbSET2.Visible = rbSET3.Visible = true;
                        rbHigh.Visible = rbLow.Visible = false;
                        rbSET3.Select();
                        break;
                    case Function.H:
                        textBox1.Visible = false;
                        hScrollBar1.Visible = false;
                        rbFREE.Visible = rbSET1.Visible = rbSET2.Visible = rbSET3.Visible = false;
                        rbHigh.Visible = rbLow.Visible = true;
                        rbHigh.Select();
                        break;
                    case Function.L:
                        textBox1.Visible = false;
                        hScrollBar1.Visible = false;
                        rbFREE.Visible = rbSET1.Visible = rbSET2.Visible = rbSET3.Visible = false;
                        rbHigh.Visible = rbLow.Visible = true;
                        rbLow.Select();
                        break;
                    default:
                        break;
                }
            }
        }

        private int _sliderValue = 0;
        public int sliderValue
        {
            get
            {
                return _sliderValue;
            }
            set
            {
                _sliderValue = value;
                textBox1.Text = _sliderValue.ToString();
                hScrollBar1.Value = _sliderValue;
            }
        }

        private Color _sliderColor;
        public Color sliderColor
        {
            set
            {
                _sliderColor = value;
                this.BackColor = _sliderColor;
            }
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (Control.MouseButtons == MouseButtons.Left)
            {
                _sliderValue = e.NewValue;//hScrollBar1.Value;
                textBox1.Text = _sliderValue.ToString();
                // hier moet de event getriggerd worden. Ergens anders nog ook?
                this.ChangeSliderControl(sliderFunction, _sliderValue);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            int number;
            if (e.KeyCode == Keys.Enter)
            {
                if (isNumeric(textBox1.Text))
                {
                    number = int.Parse(textBox1.Text);
                    if (number < _sliderMinimumRange)
                        number = sliderValue;
                    if (number > _sliderMaximumRange)
                        number = sliderValue;

                    _sliderValue = number;
                    hScrollBar1.Value = _sliderValue;
                    textBox1.Text = _sliderValue.ToString();
                    this.ChangeSliderControl(sliderFunction, number);
                }
                else
                {
                    _sliderValue = hScrollBar1.Value;
                    textBox1.Text = _sliderValue.ToString();
                }
            }
        }

        private System.Boolean isNumeric(System.Object Expression)
        {
            if(Expression == null || Expression is DateTime)
                return false;

            if(Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
                return true;
   
            try 
            {
                if(Expression is string)
                Double.Parse(Expression as string);
            else
                Double.Parse(Expression.ToString());
                return true;
            } catch {} // just dismiss errors but return false
            return false;
        }

        // A delegate type for hooking up slidercontrol notifications.
        //
        public delegate void SliderControlEventHandler(object sender, SliderControlEventsArgs e);

        // Now, create a public event "SliderControlChanged" 
        // whose type is our SliderControlEventHandler.
        //
        public event SliderControlEventHandler SliderControlChanged;

        private void ChangeSliderControl(int function, int value)
        {
            SliderControlEventsArgs SliderControlEvents = new SliderControlEventsArgs(function, value);

            // Now, raise the event by invoking the delegate. Pass in
            // the objects that initiated the event (this) as wel as
            // AccelerationChangeEventArgs.
            // The call must match the signature of the AccelerationChangeEventHandler.
            if (SliderControlChanged != null)
            {
                SliderControlChanged(this, SliderControlEvents);
            }
        }
    }

    // Class that contains the data for the change slidercontrol events.
    // Derives from System.Eventargs.
    //
    public class SliderControlEventsArgs : EventArgs
    {
        // The slidercontrol event will have two pieces of information--
        // 1) the function and 3) the value of that function.
        private int function;
        private int value;
        
        // Default Constructor
        //
        public SliderControlEventsArgs(int function, int value)
        {
            this.function = function;
            this.value = value;
        }

        // The Function property returns the reference to the slidercontrol function
        // from which this event originated.
        //
        public int Function
        {
            get { return this.function; }
            set { this.function = value; }
        }

        // The Acceleration property returns the reference of the acceleration
        // value from which this event originated.
        //
        public int Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
    }
}
