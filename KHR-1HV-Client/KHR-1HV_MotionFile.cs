using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Ini;
using Roboard;

namespace KHR_1HV
{
    public class KHR_1HV_MotionFile
    {
        IniFile khr_1hv_motion;

        public KHR_1HV_MotionFile()
        {
            khr_1hv_motion = new IniFile("");
            IniSection section = new IniSection();

            section.Add("Type", "0");
            section.Add("Width", "854");
            section.Add("Height", "480");
            section.Add("Items", "0");
            section.Add("Links", "0");
            section.Add("Start", "-1");
            section.Add("Name", "");
            section.Add("Ctrl", "65535");
            khr_1hv_motion.Add("GraphicalEdit", section);
        }

        public IniFile newMotion()
        {
            return khr_1hv_motion;
        }
    }
}
