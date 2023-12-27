using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AbcMobil.Models
{
    public class StockUI
    {
        public string SeriNo { get; set; }
        public string StokKodu { get; set; }
        public string StokAdi { get; set; }
        public string RafKodu { get; set; }
        public bool IsVisible { get; set; }
        public Color HeaderColor { get; set; }
        public Color ContentColor { get; set; }
        public string MamulKodu { get; set; }
        public string MamulStokAdi { get; set; }
        public string KumasDetayi { get; set; }
        public double Bakiye { get; set; } = 0;
    }
}
