using System;
using System.Collections.Generic;
using System.Text;

namespace AbcMobil.Models
{
    public class MobileResult
    {
        public string Message { get; set; }
        public object Data { get; set; }
        public bool Result { get; set; }
        public bool ExceptionResult { get; set; }
    }
}
