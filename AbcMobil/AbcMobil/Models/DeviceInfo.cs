using System;
using System.Collections.Generic;
using System.Text;

namespace AbcMobil.Models
{
    public class DeviceInfo
    {
        public string DeviceInfos { get; set; }
        public string DeviceHardware { get; set; }
        public string Version { get; set; }
        public int Temperature { get; set; }
        public string Region { get; set; }
        public int ReadPower { get; set; }
        public int WritePower { get; set; }
    }
}
