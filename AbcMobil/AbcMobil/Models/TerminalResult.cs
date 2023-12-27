using System;
using System.Collections.Generic;
using System.Text;

namespace AbcMobil.Models
{
    public class TerminalResult
    {
        public bool Result { get; set; }
        public bool ExceptionResult { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public string DeviceID { get; set; }
    }
}
