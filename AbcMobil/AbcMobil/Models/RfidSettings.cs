using System;
using System.Collections.Generic;
using System.Text;

namespace AbcMobil.Models
{
    public class RfidSettings
    {
        private static RfidSettings settings;
        public static RfidSettings Instance
        {
            get => settings ?? (settings = new RfidSettings());
        }
        public int CensusReadPower { get; set; } = 33;
        public int TicketCensusReadPower { get; set; } = 33;
        public int SearchReadPower { get; set; } = 33;
        public int MatchingReadPower { get; set; } = 5;
        public int MatchingTagReadPower { get; set; } = 5;
        public int MatchingPower { get; set; } = 5;
        public int TagReadPower { get; set; } = 5;
        public int TagWritePower { get; set; } = 5;
        public int TicketReadPower { get; set; } = 5;
        public int TicketWritePower { get; set; } = 5;
        public int ExitPower { get; set; } = 5;
        public int StoreImportPower { get; set; } = 5;

    }
}
