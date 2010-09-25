using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KHR_1HV
{
    public partial class KHR_1HV_Properties : Form
    {
        //===================
        //  CLASS VARIABLES 
        //===================
        private int _tbWidth;
        private int _tbHeight;
        private int _tbGridX;
        private int _tbGridY;
        private bool _cbIcon;
        private bool _cbToolMenu;
        private bool _cbPartsMenu;
        private bool _cbCommunicationSettings;
        private bool _cbCommandMenu;

        //=======================
        //  DEFAULT CONSTRUCTOR
        //=======================
        public KHR_1HV_Properties()
        {
            InitializeComponent();
            tbWidth.Text = _tbWidth.ToString();
            tbHeight.Text = _tbHeight.ToString();
            tbGridX.Text = _tbGridX.ToString();
            tbGridY.Text = _tbGridY.ToString();
            cbIcon.Checked = _cbIcon;
            cbToolMenu.Checked = _cbToolMenu;
            cbPartsMenu.Checked = _cbPartsMenu;
            cbCommunicationSettings.Checked = _cbCommunicationSettings;
            cbCommandMenu.Checked = _cbCommandMenu;
        }
        
        public int sizeWidth
        {
            get 
            {
                _tbWidth = int.Parse(tbWidth.Text);
                return _tbWidth;
            }
            set 
            { 
                _tbWidth = value;
                  tbWidth.Text = _tbWidth.ToString();
            }
        }

        public int sizeHeight
        {
            get
            {
                _tbHeight = int.Parse(tbHeight.Text);
                return _tbHeight;
            }
            set
            {
                _tbHeight = value;
                tbHeight.Text = _tbHeight.ToString();
            }
        }

        public int GridX
        {
            get
            {
                _tbGridX = int.Parse(tbGridX.Text);
                return _tbGridX;
            }
            set
            {
                _tbGridX = value;
                tbGridX.Text = _tbGridX.ToString();
            }
        }

        public int GridY
        {
            get
            {
                _tbGridY = int.Parse(tbGridY.Text);
                return _tbGridY;
            }
            set
            {
                _tbGridY = value;
                tbGridY.Text = _tbGridY.ToString();
            }
        }

        public bool IconDisplay
        {
            get
            {
                _cbIcon = cbIcon.Checked;
                return _cbIcon;
            }
            set
            {
                _cbIcon = value;
                cbIcon.Checked = _cbIcon;
            }
        }

        public bool ToolMenu
        {
            get
            {
                _cbToolMenu = cbToolMenu.Checked;
                return _cbToolMenu;
            }
            set
            {
                _cbToolMenu = value;
                cbToolMenu.Checked = _cbToolMenu;
            }
        }

        public bool PartsMenu
        {
            get
            {
                _cbPartsMenu = cbPartsMenu.Checked;
                return _cbPartsMenu;
            }
            set
            {
                _cbPartsMenu = value;
                cbPartsMenu.Checked = _cbPartsMenu;
            }
        }

        public bool CommSettings
        {
            get
            {
                _cbCommunicationSettings = cbCommunicationSettings.Checked;
                return _cbCommunicationSettings;
            }
            set
            {
                _cbCommunicationSettings = value;
                cbCommunicationSettings.Checked = _cbCommunicationSettings;
            }
        }

        public bool CommandMenu
        {
            get
            {
                _cbCommandMenu = cbCommandMenu.Checked;
                return _cbCommandMenu;
            }
            set
            {
                _cbCommandMenu = value;
                cbCommandMenu.Checked = _cbCommandMenu;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            //check textboxes for values
            if (!isNumeric(tbWidth.Text))
                tbWidth.Text = _tbWidth.ToString();
            if (!isNumeric(tbHeight.Text))
                tbHeight.Text = _tbWidth.ToString();
            if (!isNumeric(tbGridX.Text))
                tbGridX.Text = _tbGridX.ToString();
            if (!isNumeric(tbGridY.Text))
                tbGridY.Text = _tbGridY.ToString();

            if (cbIcon.Checked)
                if (!Roboard.FileAssociation.IsAssociated(".RMF"))
                    Roboard.FileAssociation.Associate(".RMF", "Roboard Motion File", "ext File", "KHR-1HV.ico", "KHR-1HV.exe");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void propertyForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //private void tbWidth_KeyDown(object sender, KeyEventArgs e)
        //{
        //    int number;
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if (isNumeric(tbWidth.Text))
        //        {
        //            number = int.Parse(tbWidth.Text);
        //            if (number < 100)
        //                number = 100;
        //            if (number > 2048)
        //                number = 2048;

        //            tbWidth.Text = number.ToString();
        //        }
        //        else
        //        {
        //            //_sliderValue = hScrollBar1.Value;
        //            //textBox1.Text = _sliderValue.ToString();
        //        }
        //    }

        //}

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

    }
}
