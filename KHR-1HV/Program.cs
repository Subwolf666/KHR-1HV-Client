using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KHR_1HV
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new KHR_1HV_Main());            
        }
    }
}