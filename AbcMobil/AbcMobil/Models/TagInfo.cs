using System.Collections.Generic;

namespace AbcMobil.Models
{
    public class TagInfo
    {
        public int ID { get; set; }
        public string SeriNo { get; set; }
        public string StokKodu { get; set; }
        public string StokAdi { get; set; }
        public string RafKodu { get; set; }
        public bool MakeSync { get; set; }
        public int Count { get; set; }
    }
}