using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Roboard;
using Roboard.Events;

namespace KHR_1HV
{
    public partial class KHR_1HV_Sensors : Form
    {
        private Roboard.Magnetometer magneto;
        private Roboard.Accelerometer accel;
        private Roboard.Gyroscope gyro;

        private AnalogSlider.AnalogSliderControl[] magnetoSliderArray;
        private AnalogSlider.AnalogSliderControl[] acceleroSliderArray;
        private AnalogSlider.AnalogSliderControl[] gyroSliderArray;

        // Declare an enum
        enum StateEnum { MAGNETO = 0, ACCELERO, GYRO };
        // Create and initialize instance of enum type
        StateEnum State = StateEnum.MAGNETO;

        public KHR_1HV_Sensors()
        {
            InitializeComponent();
        }

        private void KHR_1HV_Sensors_Load(object sender, EventArgs e)
        {
            int xPos = 10;
            int yPos = 15;

            this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);

            // COMPASS - maak hier een functie van
            //
            magnetoSliderArray = new AnalogSlider.AnalogSliderControl[3];
            for (int i = 0; i < 3; i++)
            {
                magnetoSliderArray[i] = new AnalogSlider.AnalogSliderControl();
                magnetoSliderArray[i].Tag = i + 1;
                // label x y z
                magnetoSliderArray[i].Left = xPos;
                magnetoSliderArray[i].Top = yPos;
                yPos += magnetoSliderArray[i].Height + 2;
                groupBox1.Controls.Add(magnetoSliderArray[i]);
                magnetoSliderArray[i].BringToFront();
            }
            magnetoSliderArray[0].Label = "X";
            magnetoSliderArray[1].Label = "Y";
            magnetoSliderArray[2].Label = "Z";
            //
            // Tot hier is de compass functie

            // ACCELERO - maak hier een functie van
            //
            xPos = 10;
            yPos = 15;

            acceleroSliderArray = new AnalogSlider.AnalogSliderControl[3];
            for (int i = 0; i < 3; i++)
            {
                acceleroSliderArray[i] = new AnalogSlider.AnalogSliderControl();
                acceleroSliderArray[i].Tag = i + 1;
                // label x y z
                acceleroSliderArray[i].Left = xPos;
                acceleroSliderArray[i].Top = yPos;
                yPos += acceleroSliderArray[i].Height + 2;
                groupBox2.Controls.Add(acceleroSliderArray[i]);
                //this.Controls.Add(compassSliderArray[i]);
                //compassSliderArray[i].MouseDown += new System.Windows.Forms.MouseEventHandler(KHR_1HV_Analog_MouseDown);
                acceleroSliderArray[i].BringToFront();
            }
            acceleroSliderArray[0].Label = "X";
            acceleroSliderArray[1].Label = "Y";
            acceleroSliderArray[2].Label = "Z";
            //
            // Tot hier is de accelero functie

            // GYRO - maak hier een functie van
            //
            xPos = 10;
            yPos = 15;

            gyroSliderArray = new AnalogSlider.AnalogSliderControl[8];
            for (int i = 0; i < 8; i++)
            {
                gyroSliderArray[i] = new AnalogSlider.AnalogSliderControl();
                gyroSliderArray[i].Tag = i + 1;
                // label x y z
                gyroSliderArray[i].Left = xPos;
                gyroSliderArray[i].Top = yPos;
                yPos += gyroSliderArray[i].Height + 2;
                groupBox3.Controls.Add(gyroSliderArray[i]);
                //this.Controls.Add(compassSliderArray[i]);
                //compassSliderArray[i].MouseDown += new System.Windows.Forms.MouseEventHandler(KHR_1HV_Analog_MouseDown);
                gyroSliderArray[i].BringToFront();
            }
            gyroSliderArray[0].Label = "X";
            gyroSliderArray[1].Label = "X45";
            gyroSliderArray[2].Label = "Y";
            gyroSliderArray[3].Label = "Y45";
            gyroSliderArray[4].Label = "Z";
            gyroSliderArray[5].Label = "Z45";
            gyroSliderArray[6].Label = "IDG";
            gyroSliderArray[7].Label = "ISZ";
            //
            // Tot hier is de gyro functie

            // start only the magnetometer because it's on the first tab.
            //
            magneto = new Magnetometer();
            magneto.MagneticfieldChange += new MagneticFieldChangeEventHandler(magneto_magneticfieldChange);
            magneto.Start();
        }

        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (State == StateEnum.MAGNETO)
            {
                magneto.MagneticfieldChange -= new MagneticFieldChangeEventHandler(magneto_magneticfieldChange);
                magneto.Stop();
            }
            else if (State == StateEnum.ACCELERO)
            {
                accel.AccelerationChange -= new AccelerationChangeEventHandler(accel_AccelerationChange);
                accel.Stop();
            }
            else if (State == StateEnum.GYRO)
            {
                gyro.Stop();
                gyro.GyroscopeChange -= new GyroscopeChangeEventHandler(gyro_GyroscopeChange);
            }
        }

        void magneto_magneticfieldChange(object sender, MagneticfieldChangeEventArgs e)
        {
            Magnetometer attached = (Magnetometer)sender;
            if (State == StateEnum.MAGNETO)
            {
                for (int index = 0; index < StaticUtilities.numberOfMagnetoMeterAxis; index++)
                {
                    magnetoText(index, Convert.ToInt32(e.MagneticField[index]));
                }
            }
        }

        void accel_AccelerationChange(object sender, AccelerationChangeEventArgs e)
        {
            Accelerometer attached = (Accelerometer)sender;
            if (State == StateEnum.ACCELERO)
            {
                for (int index = 0; index < StaticUtilities.numberOfAcceleroMeterAxis; index++)
                {
                    accelerationText(index, Convert.ToInt32(e.Acceleration[index]));
                }
            }
        }

        void gyro_GyroscopeChange(object sender, GyroscopeChangeEventArgs e)
        {
            Gyroscope attached = (Gyroscope)sender;
            if (State == StateEnum.GYRO)
            {
                for (int index = 0; index < StaticUtilities.numberOfGyroscopeAxis; index++)
                {
                    try
                    {
                        gyroscopeText(index, Convert.ToInt32(e.Gyroscope[index]));
                    }
                    catch { }
                }
            }
        }
        
        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPageIndex == 0)
            {
                State = StateEnum.MAGNETO;
                if (accel != null)
                {
                    accel.AccelerationChange -= new AccelerationChangeEventHandler(accel_AccelerationChange);
                    accel.Stop();
                    accel = null;
                }
                if (gyro != null)
                {
                    gyro.GyroscopeChange -= new GyroscopeChangeEventHandler(gyro_GyroscopeChange);
                    gyro.Stop();
                    gyro = null;
                }
                magneto = new Magnetometer();
                magneto.MagneticfieldChange += new MagneticFieldChangeEventHandler(magneto_magneticfieldChange);
                magneto.Start();
            }
            else if (e.TabPageIndex == 1)
            {
                State = StateEnum.ACCELERO;
                if (magneto != null)
                {
                    magneto.MagneticfieldChange -= new MagneticFieldChangeEventHandler(magneto_magneticfieldChange);
                    magneto.Stop();
                    magneto = null;
                }
                if (gyro != null)
                {
                    gyro.GyroscopeChange -= new GyroscopeChangeEventHandler(gyro_GyroscopeChange);
                    gyro.Stop();
                    gyro = null;
                }
                accel = new Accelerometer();
                accel.AccelerationChange += new AccelerationChangeEventHandler(accel_AccelerationChange);
                accel.Start();
            }
            else if (e.TabPageIndex == 2)
            {
                State = StateEnum.GYRO;
                if (magneto != null)
                {
                    magneto.MagneticfieldChange -= new MagneticFieldChangeEventHandler(magneto_magneticfieldChange);
                    magneto.Stop();
                    magneto = null;
                }
                if (accel != null)
                {
                    accel.AccelerationChange -= new AccelerationChangeEventHandler(accel_AccelerationChange);
                    accel.Stop();
                    accel = null;
                }
                gyro = new Gyroscope();
                gyro.GyroscopeChange += new GyroscopeChangeEventHandler(gyro_GyroscopeChange);
                gyro.Start();
            }
        }

        private delegate void magnetoTextDelegate(int index, int magnetofield);
        private void magnetoText(int index, int magnetofield)
        {
            if (this.magnetoSliderArray[index].InvokeRequired)
            {
                this.magnetoSliderArray[index].BeginInvoke(new magnetoTextDelegate(magnetoText), new object[] { index, magnetofield });
            }
            else
            {
                this.magnetoSliderArray[index].Measure = magnetofield;
            }
        }

        private delegate void accelerationTextDelegate(int index, int acceleration);
        private void accelerationText(int index, int acceleration)
        {
            if (this.acceleroSliderArray[index].InvokeRequired)
            {
                this.acceleroSliderArray[index].BeginInvoke(new accelerationTextDelegate(accelerationText), new object[] { index, acceleration });
            }
            else
            {
                this.acceleroSliderArray[index].Measure = acceleration;
            }
        }

        private delegate void gyroscopeTextDelegate(int index, int gyroscope);
        private void gyroscopeText(int index, int gyroscope)
        {
            if (this.gyroSliderArray[index].InvokeRequired)
            {
                this.gyroSliderArray[index].BeginInvoke(new gyroscopeTextDelegate(gyroscopeText), new object[] { index, gyroscope });
            }
            else
            {
                this.gyroSliderArray[index].Measure = gyroscope;
            }
        }
    }
}
