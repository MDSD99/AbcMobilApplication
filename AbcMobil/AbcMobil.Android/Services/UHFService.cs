using AbcMobil.Helper;
using AbcMobil.Models;
using AbcMobil.Services;
using Com.Handheld.Uhfr;
using Com.Uhf.Api.Cls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbcMobil.Droid.Services
{
    public class UHFService : IUHFService
    {
        public static bool down = false;
        private static UHFRManager rfidManager;
        public static IList<TagInfo> taglist;
        public static IList<TagInfo> epclist;
        public void CensusSerialNumberDelete(string seriNo)
        {
            if (seriNo.Length == 12)
            {
                TagInfo info = epclist.FirstOrDefault(s => s.SeriNo == seriNo);
                if (info != null)
                {
                    epclist.Remove(info);
                }
            }
            else if (seriNo.Length == 13)
            {
                seriNo = seriNo.Substring(0, 8) + seriNo.Substring(9, 4);
                TagInfo info = epclist.FirstOrDefault(s => s.SeriNo == seriNo);
                if (info != null)
                {
                    epclist.Remove(info);
                }
            }
        }

        public bool CloseDown()
        {
            return down;
        }
        public void CloseDownUpdate()
        {
            down = false;
        }
        public void CloseDownUp()
        {
            down = true;
        }
        public static string TextToHex(string metin)
        {
            return ByteToHex(TextToBytes(metin));
        }
        public static string HexToText(string hex)
        {
            return ByteToTexts(HexToByte(hex));
        }
        public static byte[] TextToBytes(string source)
        {
            return System.Text.Encoding.ASCII.GetBytes(source);
        }
        public static string ByteToTexts(byte[] source)
        {
            return System.Text.Encoding.ASCII.GetString(source);
        }
        public static byte[] HexToByte(string hex)
        {
            byte[] byteArray = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
                byteArray[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return byteArray;
        }
        public static string ByteToHex(byte[] data)
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder(data.Length * 2);
            foreach (byte bytee in data)
                builder.AppendFormat("{0:X2}", bytee);
            return builder.ToString();
        }
        #region Eski
        /*
         if (item.Protocol == Reader.SL_TagProtocol.SlTagProtocolNone)
            protocol = 0;
        else if (item.Protocol == Reader.SL_TagProtocol.SlTagProtocolİso180006b)
            protocol = 1;
        else if (item.Protocol == Reader.SL_TagProtocol.SlTagProtocolGen2)
            protocol = 2;
        else if (item.Protocol == Reader.SL_TagProtocol.SlTagProtocolİso180006bUcode)
            protocol = 3;
        else if (item.Protocol == Reader.SL_TagProtocol.SlTagProtocolİpx64)
            protocol = 4;
        else if (item.Protocol == Reader.SL_TagProtocol.SlTagProtocolİpx256)
            protocol = 5;
        //TagInfo info = new TagInfo
        //{
        //    AntennaID = item.AntennaID,
        //    CRC = item.Crc,
        //    RSSI = item.Rssi,
        //    PC = item.Pc,
        //    Frequency = item.Frequency,
        //    ReadCnt = item.ReadCnt,
        //    EmbededData = item.EmbededData,
        //    EmbededDatalen = item.EmbededDatalen,
        //    EpcId = item.EpcId,
        //    Epclen = item.Epclen,
        //    Phase = item.Phase,
        //    protocol = protocol,
        //    Res = item.Res,
        //    TimeStamp = item.TimeStamp,
        //};
         */
        #endregion
        private bool Connection(int read, int write)
        {
            if (epclist == null)
                epclist = new List<TagInfo>();
            if (taglist == null)
                taglist = new List<TagInfo>();
            if (rfidManager == null)
            {
                rfidManager = UHFRManager.Instance;
            }
            rfidManager.SetRegion(Reader.Region_Conf.RgEu3);
            rfidManager.SetPower(read, write);
            //}
            if (rfidManager != null)
                return true;
            else
                return false;
        }
        private string GetRegion(Reader.Region_Conf region)
        {
            string regionn;
            if (region == Reader.Region_Conf.RgEu)
                regionn = "Eu";
            else if (region == Reader.Region_Conf.RgEu2)
                regionn = "Eu2";
            else if (region == Reader.Region_Conf.RgEu3)
                regionn = "Eu3";
            else if (region == Reader.Region_Conf.RgKr)
                regionn = "Kr";
            else if (region == Reader.Region_Conf.RgNa)
                regionn = "Na";
            else if (region == Reader.Region_Conf.RgNone)
                regionn = "None";
            else if (region == Reader.Region_Conf.RgOpen)
                regionn = "Open";
            else if (region == Reader.Region_Conf.RgPrc)
                regionn = "Prc";
            else if (region == Reader.Region_Conf.RgPrc2)
                regionn = "Eu";
            else
                regionn = "";
            return regionn;
        }
        public void TicketMatching(IList<Stock> stockk, string rafCode)
        {
            foreach (Stock item in stockk)
            {
                epclist.Add(new TagInfo
                {
                    ID = stockk.Count + 1,
                    Count = 1,
                    MakeSync = true,
                    RafKodu = item.RafKodu,
                    SeriNo = item.SeriNo,
                    StokAdi = item.StokAdi,
                    StokKodu = item.StokKodu
                });
            }
            taglist.FirstOrDefault(s => s.RafKodu == rafCode).MakeSync = true;

        }
        public void Matching(Stock info)
        {
            int dell = info.SeriNo.IndexOf("-");
            if (dell != -1)
                info.SeriNo = info.SeriNo.Substring(0, 8) + info.SeriNo.Substring(9, 4);
            TagInfo inf = epclist.Where(s => s.SeriNo == info.SeriNo).FirstOrDefault();
            if (inf != null)
            {
                epclist[inf.ID - 1].StokKodu = info.StokKodu;
                epclist[inf.ID - 1].StokAdi = info.StokAdi;
                epclist[inf.ID - 1].RafKodu = info.RafKodu;
                epclist[inf.ID - 1].SeriNo = info.SeriNo;
                epclist[inf.ID - 1].MakeSync = true;
            }
        }
        public TerminalResult SearchingSerialNumber(IList<Stock> SearchingList, int readPower, int writePower)
        {
            //Task<TerminalResult> task = Task.Run(() =>
            //{
            TerminalResult tresult = new TerminalResult();
            tresult.Result = false;
            tresult.ExceptionResult = false;
            tresult.Data = null;
            try
            {
                if (Connection(readPower, writePower))
                {
                    byte[] password = new byte[4] { 0x00, 0x00, 0x00, 0x00 };
                    IList<Reader.TAGINFO> arrList = rfidManager.TagInventoryByTimer(200);
                    if (arrList == null)
                    {
                        tresult.Message = "Tag okunamadı!";
                    }
                    else
                    {
                        if (arrList.Count != 0)
                        {
                            byte[] epc;
                            string serino = "";
                            foreach (Reader.TAGINFO item in arrList)
                            {
                                if (item.Epclen == 12)
                                {
                                    epc = new byte[12];
                                    serino = string.Empty;
                                    for (int i = 0; i < item.Epclen; i++)
                                    {
                                        epc[i] = item.EpcId[i];
                                    }
                                    serino = ConvertHelper.ByteToTexts(epc).Replace('*', ' ').Trim();
                                    if (serino.Length == 12)
                                    {
                                        Stock s = SearchingList.Where(s => s.SeriNo == serino).FirstOrDefault();
                                        if (s != null)
                                        {
                                            tresult.Result = true;
                                            tresult.Message = item.Rssi.ToString();
                                            tresult.Data = s.SeriNo;
                                            return tresult;
                                        }
                                    }
                                }
                            }
                            tresult.Result = false;
                            tresult.Message = "Tag okunamadı...";
                            tresult.Data = null;
                        }
                        else
                            tresult.Message = "Tag Okunamadı!";
                    }
                }
                else
                {
                    tresult.Message = "Cihaz bağlantısı açılamadı!";
                    tresult.ExceptionResult = true;
                }
            }
            catch (Exception ex)
            {
                tresult.ExceptionResult = true;
                tresult.Message = ex.Message;
            }
            return tresult;
            //});
            //return task;
        }
        public TerminalResult WriteSerialNumber(string txtData, int readPower, int writePower)
        {
            TerminalResult tresult = new TerminalResult();
            tresult.Result = false;
            tresult.ExceptionResult = false;
            tresult.Data = null;
            try
            {
                if (Connection(readPower, writePower))
                {
                    byte[] data = ConvertHelper.TextToBytes(txtData.Trim());
                    byte[] password = new byte[4] { 0x00, 0x00, 0x00, 0x00 };
                    Reader.READER_ERR error = rfidManager.WriteTagData((char)1, 2, data, 12, password, 400);
                    if (error == Reader.READER_ERR.MtOkErr)
                    {
                        tresult.Data = txtData.Trim();
                        tresult.Result = true;
                        tresult.Message = "Etiket başarılı bir şekilde yazdırıldı...";
                    }
                    else
                    {
                        tresult.Data = null;
                        tresult.Message = "Etiket yazdırılamadı!";
                    }
                }
                else
                {
                    tresult.Message = "Cihaz bağlantısı açılamadı!";
                    tresult.ExceptionResult = true;
                }
            }
            catch (Exception ex)
            {
                tresult.Message = ex.Message;
                tresult.ExceptionResult = true;
            }
            return tresult;
        }
        public TerminalResult ReadSerialNumber(int readPower, int writePower)
        {
            TerminalResult tresult = new TerminalResult();
            tresult.Result = false;
            tresult.ExceptionResult = false;
            tresult.Data = null;
            try
            {
                if (Connection(readPower, writePower))
                {
                    byte[] data = new byte[12];
                    byte[] password = new byte[4] { 0x00, 0x00, 0x00, 0x000 };
                    Reader.READER_ERR error = rfidManager.GetTagData(1, 2, 6, data, password, 300);
                    if (error == Reader.READER_ERR.MtOkErr)
                    {
                        string veri = ByteToTexts(data).Replace('*', ' ').Trim();
                        if (veri.Length == 12)
                        {
                            tresult.Message = "Etiket başarılı bir şekilde okundu!";
                            tresult.Result = true;
                            tresult.Data = veri.Substring(0, 8) + "-" + veri.Substring(8, 4);
                        }
                        else
                            tresult.Message = "Etiket okunamadı!";
                    }
                    else
                        tresult.Message = "Etiket okunamadı!";
                }
                else
                {
                    tresult.Message = "Cihaz bağlantısı açılamadı!";
                    tresult.ExceptionResult = true;
                }
            }
            catch (Exception ex)
            {
                tresult.Message = ex.Message;
                tresult.ExceptionResult = true;
            }
            return tresult;
        }
        public TerminalResult WriteStockCode(string txtData, int readPower, int writePower)
        {
            TerminalResult tresult = new TerminalResult();
            tresult.Result = false;
            tresult.ExceptionResult = false;
            tresult.Data = null;
            try
            {
                if (Connection(readPower, writePower))
                {
                    byte[] data = ConvertHelper.TextToBytes(txtData.Trim());
                    byte[] password = new byte[4] { 0x00, 0x00, 0x00, 0x00 };
                    Reader.READER_ERR error = rfidManager.WriteTagData((char)2, 2, data, 12, password, 400);
                    if (error == Reader.READER_ERR.MtOkErr)
                    {
                        tresult.Data = txtData.Trim();
                        tresult.Result = true;
                        tresult.Message = "Etiket başarılı bir şekilde yazdırıldı...";
                    }
                    else
                    {
                        tresult.Data = null;
                        tresult.Message = "Etiket yazdırılamadı!";
                    }
                }
                else
                {
                    tresult.Message = "Cihaz bağlantısı açılamadı!";
                    tresult.ExceptionResult = true;
                }
            }
            catch (Exception ex)
            {
                tresult.Message = ex.Message;
                tresult.ExceptionResult = true;
            }
            return tresult;
        }
        public TerminalResult ReadStockCode(int readPower, int writePower)
        {
            TerminalResult tresult = new TerminalResult();
            tresult.Result = false;
            tresult.ExceptionResult = false;
            tresult.Data = null;
            try
            {
                if (Connection(readPower, writePower))
                {
                    byte[] data = new byte[12];
                    byte[] password = new byte[4] { 0x00, 0x00, 0x00, 0x000 };
                    Reader.READER_ERR error = rfidManager.GetTagData(2, 2, 6, data, password, 300);
                    if (error == Reader.READER_ERR.MtOkErr)
                    {
                        tresult.Message = "Etiket başarılı bir şekilde okundu!";
                        tresult.Result = true;
                        tresult.Data = ByteToTexts(data);
                    }
                    else
                        tresult.Message = "Etiket okunamadı!";
                }
                else
                {
                    tresult.Message = "Cihaz bağlantısı açılamadı!";
                    tresult.ExceptionResult = true;
                }
            }
            catch (Exception ex)
            {
                tresult.Message = ex.Message;
                tresult.ExceptionResult = true;
            }
            return tresult;
        }
        public TerminalResult WriteTag(string txtData, int readPower, int writePower)
        {
            TerminalResult tresult = new TerminalResult();
            tresult.Result = false;
            tresult.ExceptionResult = false;
            tresult.Data = null;
            try
            {
                if (Connection(readPower, writePower))
                {
                    txtData = txtData.Trim() + "*******";
                    byte[] data = ConvertHelper.TextToBytes(txtData);
                    byte[] password = new byte[4] { 0x00, 0x00, 0x00, 0x00 };
                    Reader.READER_ERR error = rfidManager.WriteTagData((char)1, 2, data, 12, password, 400);
                    if (error == Reader.READER_ERR.MtOkErr)
                    {
                        tresult.Data = txtData.Trim();
                        tresult.Result = true;
                        tresult.Message = "Etiket başarılı bir şekilde yazdırıldı...";
                    }
                    else
                    {
                        tresult.Data = null;
                        tresult.Message = "Etiket yazdırılamadı!";
                    }
                }
                else
                {
                    tresult.Message = "Cihaz bağlantısı açılamadı!";
                    tresult.ExceptionResult = true;
                }
            }
            catch (Exception ex)
            {
                tresult.Message = ex.Message;
                tresult.ExceptionResult = true;
            }
            return tresult;
        }
        public TerminalResult ReadTagg(int readPower, int writePower)
        {
            TerminalResult tresult = new TerminalResult();
            tresult.Result = false;
            tresult.ExceptionResult = false;
            tresult.Data = null;
            try
            {
                if (Connection(readPower, writePower))
                {
                    byte[] data = new byte[12];
                    byte[] password = new byte[4] { 0x00, 0x00, 0x00, 0x000 };
                    Reader.READER_ERR error = rfidManager.GetTagData(1, 2, 6, data, password, 150);
                    if (error == Reader.READER_ERR.MtOkErr)
                    {
                        string tag = ByteToTexts(data).Replace('*', ' ').Trim();
                        if (tag.Length == 5)
                        {
                            tresult.Message = "Raf başarılı bir şekilde okundu!";
                            tresult.Result = true;
                        }
                        else
                        {
                            tresult.Message = "Etiket okunamadı!";
                            tresult.Result = false;
                        }
                        tresult.Data = tag;
                    }
                    else
                        tresult.Message = "Raf okunamadı!";
                }
                else
                {
                    tresult.Message = "Cihaz bağlantısı açılamadı!";
                    tresult.ExceptionResult = true;
                }
            }
            catch (Exception ex)
            {
                tresult.Message = ex.Message;
                tresult.ExceptionResult = true;
            }
            return tresult;
        }
        public Task<TerminalResult> ReadTag(int readPower, int writePower)
        {
            Task<TerminalResult> task = Task.Run(() =>
            {
                TerminalResult tresult = new TerminalResult();
                tresult.Result = false;
                tresult.ExceptionResult = false;
                tresult.Data = null;
                try
                {
                    if (Connection(readPower, writePower))
                    {
                        byte[] data = new byte[12];
                        byte[] password = new byte[4] { 0x00, 0x00, 0x00, 0x000 };
                        Reader.READER_ERR error = rfidManager.GetTagData(1, 2, 6, data, password, 300);
                        if (error == Reader.READER_ERR.MtOkErr)
                        {
                            string tag = ByteToTexts(data).Replace('*', ' ').Trim();
                            if (tag.Length == 2 || tag.Length == 3)
                            {
                                tresult.Message = "Raf başarılı bir şekilde okundu!";
                                tresult.Result = true;
                            }
                            else
                            {
                                tresult.Message = "Etiket okunamadı!";
                                tresult.Result = false;
                            }
                            tresult.Data = tag;
                        }
                        else
                            tresult.Message = "Raf okunamadı!";
                    }
                    else
                    {
                        tresult.Message = "Cihaz bağlantısı açılamadı!";
                        tresult.ExceptionResult = true;
                    }
                }
                catch (Exception ex)
                {
                    tresult.Message = ex.Message;
                    tresult.ExceptionResult = true;
                }
                return tresult;
            });
            return task;
        }

        public Task<TerminalResult> InventorySerialNumber(short readTimer = 200, int readPower = 33, int writePower = 33)
        {
            Task<TerminalResult> task = Task.Run(() =>
            {
                TerminalResult tresult = new TerminalResult();
                tresult.Result = false;
                tresult.ExceptionResult = false;
                tresult.Data = null;
                tresult.DeviceID = App.device.GetIdentifier();
                try
                {
                    if (Connection(readPower, writePower))
                    {
                        byte[] password = new byte[4] { 0x00, 0x00, 0x00, 0x00 };
                        IList<Reader.TAGINFO> arrList = rfidManager.TagInventoryByTimer(readTimer);


                        if (arrList == null)
                        {
                            tresult.Message = "Tag okunamadı!";

                        }
                        else
                        {
                            if (arrList.Count != 0)
                            {
                                byte[] epc;
                                string serino;
                                foreach (Reader.TAGINFO item in arrList)
                                {

                                    if (item.Epclen == 12)
                                    {
                                        epc = new byte[12];
                                        serino = string.Empty;
                                        for (int i = 0; i < item.Epclen; i++)
                                        {
                                            epc[i] = item.EpcId[i];
                                        }
                                        serino = ConvertHelper.ByteToTexts(epc).Replace('*', ' ').Trim();
                                        if (serino.Length == 12)
                                        {
                                            try
                                            {
                                                bool success = Int64.TryParse(serino, out long number);
                                                if (success)
                                                {
                                                    TagInfo linfo = epclist.FirstOrDefault(s => s.SeriNo == serino);
                                                    if (linfo == null)
                                                    {
                                                        epclist.Add(new TagInfo
                                                        {
                                                            ID = epclist.Count + 1,
                                                            SeriNo = serino,
                                                            Count = 1,
                                                            MakeSync = false,
                                                        });
                                                    }
                                                    else
                                                        epclist[linfo.ID - 1].Count += 1;
                                                }

                                            }
                                            catch
                                            {

                                            }
                                        }
                                    }
                                }
                                tresult.Result = true;
                                tresult.Message = "Tag geldi...";
                                tresult.Data = epclist;
                            }
                            else
                                tresult.Message = "Tag Okunamadı!";
                        }
                    }
                    else
                    {
                        tresult.Message = "Cihaz bağlantısı açılamadı!";
                        tresult.ExceptionResult = true;
                    }
                }
                catch (Exception ex)
                {
                    tresult.Message = ex.Message;
                    tresult.ExceptionResult = true;
                }
                return tresult;
            });
            return task;
        }
        public TerminalResult InventorySerialNumberr(short readTimer = 200, int readPower = 33, int writePower = 33)
        {
            TerminalResult tresult = new TerminalResult();
            tresult.Result = false;
            tresult.ExceptionResult = false;
            tresult.Data = null;
            try
            {
                if (Connection(readPower, writePower))
                {
                    byte[] password = new byte[4] { 0x00, 0x00, 0x00, 0x00 };
                    IList<Reader.TAGINFO> arrList = rfidManager.TagInventoryByTimer(readTimer);
                    if (arrList == null)
                    {
                        tresult.Message = "Tag okunamadı!";
                    }
                    else
                    {
                        if (arrList.Count != 0)
                        {
                            byte[] epc;
                            string serino;
                            foreach (Reader.TAGINFO item in arrList)
                            {
                                if (item.Epclen == 12)
                                {
                                    epc = new byte[12];
                                    serino = string.Empty;
                                    for (int i = 0; i < item.Epclen; i++)
                                    {
                                        epc[i] = item.EpcId[i];
                                    }
                                    serino = ConvertHelper.ByteToTexts(epc).Replace('*', ' ').Trim();
                                    if (serino.Length == 12)
                                    {
                                        try
                                        {
                                            Convert.ToInt64(serino);
                                            TagInfo linfo = epclist.FirstOrDefault(s => s.SeriNo == serino);
                                            if (linfo == null)
                                            {
                                                epclist.Add(new TagInfo
                                                {
                                                    ID = epclist.Count + 1,
                                                    SeriNo = serino,
                                                    Count = 1,
                                                    MakeSync = false,
                                                });
                                            }
                                            else
                                                epclist[linfo.ID - 1].Count += 1;
                                        }
                                        catch
                                        {

                                        }
                                    }
                                }
                            }
                            tresult.Result = true;
                            tresult.Message = "Tag geldi...";
                            tresult.Data = epclist;
                        }
                        else
                            tresult.Message = "Tag Okunamadı!";
                    }
                }
                else
                {
                    tresult.Message = "Cihaz bağlantısı açılamadı!";
                    tresult.ExceptionResult = true;
                }
            }
            catch (Exception ex)
            {
                tresult.Message = ex.Message;
                tresult.ExceptionResult = true;
            }
            return tresult;
        }
        public Task<TerminalResult> InventoryStockCode(short readTimer = 200, int readPower = 33, int writePower = 33)
        {
            Task<TerminalResult> task = Task.Run(() =>
            {
                TerminalResult tresult = new TerminalResult();
                tresult.Result = false;
                tresult.ExceptionResult = false;
                tresult.Data = null;
                try
                {
                    if (Connection(readPower, writePower))
                    {
                        IList<Reader.TAGINFO> arrList = rfidManager.TagEpcTidInventoryByTimer(readTimer);
                        if (arrList == null)
                        {
                            tresult.Message = "Tag okunamadı!";
                        }
                        else
                        {
                            if (arrList.Count != 0)
                            {
                                if (epclist == null)
                                    epclist = new List<TagInfo>();
                                byte[] epc = new byte[12];
                                byte[] data = new byte[24];
                                string serino = string.Empty;
                                string stockKod = string.Empty;
                                foreach (Reader.TAGINFO item in arrList)
                                {
                                    if (item.EmbededDatalen == 24)
                                    {
                                        for (int i = 0; i < 24; i++)
                                        {
                                            epc[i] = item.EpcId[i];
                                        }
                                        serino = ConvertHelper.ByteToTexts(epc).Replace('*', ' ').Trim();
                                        TagInfo linfo = epclist.FirstOrDefault(s => s.SeriNo == serino);
                                        if (linfo == null)
                                        {
                                            TagInfo info = new TagInfo
                                            {
                                                ID = epclist.Count + 1,
                                                SeriNo = serino,
                                                Count = 1
                                            };
                                            epclist.Add(info);
                                        }
                                        else
                                        {
                                            epclist[linfo.ID - 1].Count += 1;
                                        }
                                    }
                                    if (item.Epclen == 12)
                                    {
                                        for (int i = 0; i < 12; i++)
                                        {
                                            epc[i] = item.EpcId[i];
                                        }
                                        serino = ConvertHelper.ByteToTexts(epc).Replace('*', ' ').Trim();
                                        TagInfo linfo = epclist.FirstOrDefault(s => s.SeriNo == serino);
                                        if (linfo == null)
                                        {
                                            TagInfo info = new TagInfo
                                            {
                                                ID = epclist.Count + 1,
                                                SeriNo = serino,
                                                Count = 1
                                            };
                                            epclist.Add(info);
                                        }
                                        else
                                        {
                                            epclist[linfo.ID - 1].Count += 1;
                                        }
                                    }
                                }
                                tresult.Result = true;
                                tresult.Message = "Tag geldi...";
                                tresult.Data = epclist;
                            }
                            else
                                tresult.Message = "Tag Okunamadı!";
                        }
                    }
                    else
                    {
                        tresult.Message = "Cihaz bağlantısı açılamadı!";
                        tresult.ExceptionResult = true;
                    }
                }
                catch (Exception ex)
                {
                    tresult.Message = ex.Message;
                    tresult.ExceptionResult = true;
                }
                return tresult;
            });
            return task;
        }
        public Task<TerminalResult> TagInventory(short readTimer = 200, int readPower = 33, int writePower = 33)
        {
            Task<TerminalResult> task = Task.Run(() =>
            {
                TerminalResult tresult = new TerminalResult();
                tresult.Result = false;
                tresult.ExceptionResult = false;
                tresult.Data = null;
                try
                {
                    if (Connection(readPower, writePower))
                    {
                        byte[] password = new byte[4] { 0x00, 0x00, 0x00, 0x00 };
                        IList<Reader.TAGINFO> arrList = rfidManager.TagInventoryByTimer(readTimer);
                        if (arrList == null)
                        {
                            tresult.Message = "Tag okunamadı!";
                        }
                        else
                        {
                            if (arrList.Count != 0)
                            {
                                byte[] epc;
                                string rafKodu;
                                foreach (Reader.TAGINFO item in arrList)
                                {
                                    if (item.Epclen == 12)
                                    {
                                        epc = new byte[12];
                                        rafKodu = string.Empty;
                                        for (int i = 0; i < item.Epclen; i++)
                                        {
                                            epc[i] = item.EpcId[i];
                                        }
                                        rafKodu = ConvertHelper.ByteToTexts(epc).Replace('*', ' ').Trim();
                                        if (rafKodu.Length == 5)
                                        {
                                            TagInfo linfo = taglist.FirstOrDefault(s => s.RafKodu == rafKodu);
                                            if (linfo == null)
                                                taglist.Add(new TagInfo
                                                {
                                                    ID = taglist.Count + 1,
                                                    RafKodu = rafKodu,
                                                    Count = 1,
                                                    MakeSync = false,
                                                });
                                            else
                                                taglist[linfo.ID - 1].Count += 1;
                                        }
                                    }
                                }
                                if (taglist.Count != 0)
                                {
                                    tresult.Result = true;
                                    tresult.Message = "Tag geldi...";
                                    tresult.Data = taglist;
                                }else
                                    tresult.Message = "Tag Okunamadı!";
                            }
                            else
                                tresult.Message = "Tag Okunamadı!";
                        }
                    }
                    else
                    {
                        tresult.Message = "Cihaz bağlantısı açılamadı!";
                        tresult.ExceptionResult = true;
                    }
                }
                catch (Exception ex)
                {
                    tresult.Message = ex.Message;
                    tresult.ExceptionResult = true;
                }
                return tresult;
            });
            return task;
        }
        public Task<TerminalResult> MultiInventory(short readTimer = 300, int readPower = 33, int writePower = 33)
        {
            Task<TerminalResult> task = Task.Run(() =>
            {
                TerminalResult tresult = new TerminalResult();
                tresult.Result = false;
                tresult.ExceptionResult = false;
                tresult.Data = null;
                try
                {
                    if (Connection(readPower, writePower))
                    {
                        IList<Reader.TAGINFO> arrList = rfidManager.TagEpcTidInventoryByTimer(readTimer);
                        if (arrList == null)
                        {
                            tresult.Message = "Tag okunamadı!";
                        }
                        else
                        {
                            if (arrList.Count != 0)
                            {
                                if (epclist == null)
                                    epclist = new List<TagInfo>();
                                byte[] epc = new byte[12];
                                byte[] embededData = new byte[12];
                                string serino = string.Empty;
                                string stockKod = string.Empty;
                                foreach (Reader.TAGINFO item in arrList)
                                {
                                    serino = string.Empty;
                                    stockKod = string.Empty;
                                    if (item.Epclen == 12)
                                    {
                                        for (int i = 0; i < 12; i++)
                                        {
                                            epc[i] = item.EpcId[i];
                                        }
                                        serino = ConvertHelper.ByteToTexts(epc).Replace('*', ' ').Trim();
                                        TagInfo linfo = epclist.FirstOrDefault(s => s.SeriNo == serino);
                                        if (linfo == null)
                                        {
                                            if (item.EmbededDatalen == 12)
                                            {
                                                for (int i = 0; i < 12; i++)
                                                {
                                                    epc[i] = item.EmbededData[i];
                                                }
                                                stockKod = ConvertHelper.ByteToTexts(epc).Replace('*', ' ').Trim();
                                            }
                                            epclist.Add(new TagInfo
                                            {
                                                ID = epclist.Count + 1,
                                                SeriNo = serino,
                                                StokKodu = stockKod,
                                                Count = 1
                                            });
                                        }
                                        else
                                        {
                                            if (item.EmbededDatalen == 12)
                                            {
                                                for (int i = 0; i < 12; i++)
                                                {
                                                    epc[i] = item.EmbededData[i];
                                                }
                                                stockKod = ConvertHelper.ByteToTexts(epc).Replace('*', ' ').Trim();
                                            }
                                            epclist[linfo.ID - 1].StokKodu = stockKod;
                                            epclist[linfo.ID - 1].Count += 1;
                                        }
                                    }
                                }
                                tresult.Result = true;
                                tresult.Message = "Tag geldi...";
                                tresult.Data = epclist;
                            }
                            else
                                tresult.Message = "Tag Okunamadı!";
                        }
                    }
                    else
                    {
                        tresult.Message = "Cihaz bağlantısı açılamadı!";
                        tresult.ExceptionResult = true;
                    }
                }
                catch (Exception ex)
                {
                    tresult.Message = ex.Message;
                    tresult.ExceptionResult = true;
                }
                return tresult;
            });
            return task;
        }
        public TerminalResult LockSerialNumber(int readPower, int writePower)
        {
            TerminalResult tresult = new TerminalResult();
            tresult.Result = false;
            tresult.ExceptionResult = false;
            tresult.Data = null;
            try
            {
                if (Connection(readPower, writePower))
                {
                    byte[] password = new byte[4] { 0x00, 0x00, 0x00, 0x00 };
                    Reader.READER_ERR error = rfidManager.LockTag(Reader.Lock_Obj.LockObjectBank1, Reader.Lock_Type.Bank1Lock, password, 300);
                    if (error == Reader.READER_ERR.MtOkErr)
                    {

                    }
                    else
                    {

                    }
                }
                else
                {
                    tresult.Message = "Cihaz bağlantısı açılamadı!";
                    tresult.ExceptionResult = true;
                }
            }
            catch (Exception ex)
            {
                tresult.Message = ex.Message;
                tresult.ExceptionResult = true;
            }
            return tresult;
        }
        public TerminalResult LockStockCode(int readPower, int writePower)
        {
            TerminalResult tresult = new TerminalResult();
            tresult.Result = false;
            tresult.ExceptionResult = false;
            tresult.Data = null;
            try
            {
                if (Connection(readPower, writePower))
                {
                    byte[] password = new byte[4] { 0x00, 0x00, 0x00, 0x00 };
                    Reader.READER_ERR error = rfidManager.LockTag(Reader.Lock_Obj.LockObjectAccessPasswd, Reader.Lock_Type.Bank2Lock, password, 300);
                    if (error == Reader.READER_ERR.MtOkErr)
                    {

                    }
                    else
                    {

                    }
                }
                else
                {
                    tresult.Message = "Cihaz bağlantısı açılamadı!";
                    tresult.ExceptionResult = true;
                }
            }
            catch (Exception ex)
            {
                tresult.Message = ex.Message;
                tresult.ExceptionResult = true;
            }
            return tresult;
        }
        public TerminalResult LockPassword(int readPower, int writePower)
        {
            TerminalResult tresult = new TerminalResult();
            tresult.Result = false;
            tresult.ExceptionResult = false;
            tresult.Data = null;
            try
            {
                if (Connection(readPower, writePower))
                {
                    byte[] password = new byte[4] { 0x00, 0x00, 0x00, 0x00 };
                    Reader.READER_ERR error = rfidManager.LockTag(Reader.Lock_Obj.LockObjectAccessPasswd, Reader.Lock_Type.AccessPasswdLock, password, 300);
                    if (error == Reader.READER_ERR.MtOkErr)
                    {

                    }
                    else
                    {

                    }
                }
                else
                {
                    tresult.Message = "Cihaz bağlantısı açılamadı!";
                    tresult.ExceptionResult = true;
                }
            }
            catch (Exception ex)
            {
                tresult.Message = ex.Message;
                tresult.ExceptionResult = true;
            }
            return tresult;
        }
        public TerminalResult UnLockSerialNumber(int readPower, int writePower)
        {
            TerminalResult tresult = new TerminalResult();
            tresult.Result = false;
            tresult.ExceptionResult = false;
            tresult.Data = null;
            try
            {
                if (Connection(readPower, writePower))
                {
                    byte[] password = new byte[4] { 0x00, 0x00, 0x00, 0x00 };
                    Reader.READER_ERR error = rfidManager.LockTag(Reader.Lock_Obj.LockObjectBank1, Reader.Lock_Type.Bank1Unlock, password, 300);
                    if (error == Reader.READER_ERR.MtOkErr)
                    {

                    }
                    else
                    {

                    }
                }
                else
                {
                    tresult.Message = "Cihaz bağlantısı açılamadı!";
                    tresult.ExceptionResult = true;
                }
            }
            catch (Exception ex)
            {
                tresult.Message = ex.Message;
                tresult.ExceptionResult = true;
            }
            return tresult;
        }
        public TerminalResult UnLockStockCode(int readPower, int writePower)
        {
            TerminalResult tresult = new TerminalResult();
            tresult.Result = false;
            tresult.ExceptionResult = false;
            tresult.Data = null;
            try
            {
                if (Connection(readPower, writePower))
                {
                    byte[] password = new byte[4] { 0x00, 0x00, 0x00, 0x00 };
                    Reader.READER_ERR error = rfidManager.LockTag(Reader.Lock_Obj.LockObjectBank2, Reader.Lock_Type.Bank2Unlock, password, 300);
                    if (error == Reader.READER_ERR.MtOkErr)
                    {

                    }
                    else
                    {

                    }
                }
                else
                {
                    tresult.Message = "Cihaz bağlantısı açılamadı!";
                    tresult.ExceptionResult = true;
                }
            }
            catch (Exception ex)
            {
                tresult.Message = ex.Message;
                tresult.ExceptionResult = true;
            }
            return tresult;
        }
        public TerminalResult UnLockPassword(int readPower, int writePower)
        {
            TerminalResult tresult = new TerminalResult();
            tresult.Result = false;
            tresult.ExceptionResult = false;
            tresult.Data = null;
            try
            {
                if (Connection(readPower, writePower))
                {
                    byte[] password = new byte[4] { 0x00, 0x00, 0x00, 0x00 };
                    Reader.READER_ERR error = rfidManager.LockTag(Reader.Lock_Obj.LockObjectAccessPasswd, Reader.Lock_Type.AccessPasswdUnlock, password, 300);
                    if (error == Reader.READER_ERR.MtOkErr)
                    {

                    }
                    else
                    {

                    }
                }
                else
                {
                    tresult.Message = "Cihaz bağlantısı açılamadı!";
                    tresult.ExceptionResult = true;
                }
            }
            catch (Exception ex)
            {
                tresult.Message = ex.Message;
                tresult.ExceptionResult = true;
            }
            return tresult;
        }
        public TerminalResult Clear()
        {
            TerminalResult result = new TerminalResult();
            result.Result = false;
            result.ExceptionResult = false;
            result.Message = "Okuma listesi temizlendi...";
            if (epclist != null)
                epclist.Clear();
            if (taglist != null)
                taglist.Clear();
            return result;
        }
        public TerminalResult Close()
        {
            TerminalResult epcc = new TerminalResult();
            epcc.Data = null;
            epcc.Result = false;
            epcc.ExceptionResult = false;
            try
            {
                if (rfidManager == null)
                {
                    epcc.Message = "Cihaz bağlantısı açık değil!";
                }
                else if (rfidManager.Close())
                {
                    epcc.Result = true;
                    epcc.Message = "Cihazınız başarılı bir şekilde kapatıldı...";
                    rfidManager = null;
                }
                else
                {
                    epcc.Message = "Cihaz kapatılamadı!";
                }
            }
            catch (Exception ex)
            {
                epcc.Message = ex.Message;
                epcc.ExceptionResult = true;
            }
            return epcc;
        }
        public TerminalResult Open()
        {
            TerminalResult epcc = new TerminalResult();
            epcc.Data = null;
            epcc.Result = false;
            epcc.ExceptionResult = false;
            try
            {
                if (rfidManager == null)
                {
                    rfidManager = UHFRManager.Instance;
                    rfidManager.SetRegion(Reader.Region_Conf.RgEu3);
                    rfidManager.SetPower(30, 30);

                    if (rfidManager != null)
                    {
                        epcc.Result = true;
                        epcc.Data = rfidManager;
                        epcc.Message = "Bağlantınız başarılı bir şekilde açıldı...";
                    }
                    else
                    {
                        epcc.Message = "Cihaz bağlantısı açılamadı!";
                    }
                }
                else
                {
                    epcc.Result = true;
                    epcc.Message = "Bağlantınız açık!";
                    epcc.Data = rfidManager;
                }
            }
            catch (Exception ex)
            {
                epcc.Message = ex.Message;
                epcc.ExceptionResult = true;
            }
            return epcc;
        }
        public TerminalResult DeviceInfo()
        {
            TerminalResult tresult = new TerminalResult();
            tresult.Result = false;
            tresult.ExceptionResult = false;
            tresult.Data = null;
            try
            {
                if (Connection(RfidSettings.Instance.TagReadPower, RfidSettings.Instance.TagWritePower))
                {
                    tresult.Data = new DeviceInfo
                    {
                        DeviceInfos = rfidManager.Info,
                        DeviceHardware = rfidManager.Hardware,
                        Version = rfidManager.Dv.ToString(),
                        Temperature = rfidManager.Temperature,
                        Region = GetRegion(rfidManager.Region),
                        ReadPower = rfidManager.GetPower()[0],
                        WritePower = rfidManager.GetPower()[1]
                    };
                    tresult.Result = true;
                    tresult.Message = "Cihaz bilgileri başarılı bir şekilde getirildi!";
                }
                else
                {
                    tresult.Message = "Cihaz bağlantı hatası!";
                    tresult.ExceptionResult = true;
                }
            }
            catch (Exception ex)
            {
                tresult.Message = ex.Message;
                tresult.ExceptionResult = true;
            }
            return tresult;
        }
        public void RemoveEpc(string seriNo)
        {
            if (epclist != null)
            {
                TagInfo info = epclist.Where(s => s.SeriNo == seriNo).Select(s => s).FirstOrDefault();
                if (info != null)
                    epclist.Remove(info);
            }
        }
        public void RemoveTag(string tag)
        {
            if (taglist != null)
            {
                TagInfo info = taglist.Where(s => s.RafKodu == tag).Select(s => s).FirstOrDefault();
                if (info != null)
                    epclist.Remove(info);
            }
        }
        public TerminalResult UsbScan()
        {
            TerminalResult result = new TerminalResult();
            return result;
        }
        public TerminalResult UsbStop()
        {
            TerminalResult result = new TerminalResult();
            return result;
        }

        //public TerminalResult SearchingSerialNumber(IList<Stock> SearchingList)
        //{
        //    TerminalResult tresult = new TerminalResult { Result = false, Data = null, ExceptionResult = false, Message = "" };
        //    try
        //    {
        //        if (Connection())
        //        {
        //            byte[] password = new byte[4] { 0x00, 0x00, 0x00, 0x00 };
        //            IList<Reader.TAGINFO> arrList = rfidManager.GetTagData();
        //            if (arrList == null)
        //            {
        //                tresult.Message = "Tag okunamadı!";
        //            }
        //            else
        //            {
        //                if (arrList.Count != 0)
        //                {
        //                    byte[] epc;
        //                    string serino;
        //                    IList<TagInfo> tagInfos = new List<TagInfo>();
        //                    foreach (Reader.TAGINFO item in arrList)
        //                    {
        //                        if (item.Epclen == 12)
        //                        {
        //                            epc = new byte[12];
        //                            serino = string.Empty;
        //                            for (int i = 0; i < item.Epclen; i++)
        //                            {
        //                                epc[i] = item.EpcId[i];
        //                            }
        //                            serino = ConvertHelper.ByteToTexts(epc).Replace('*', ' ').Trim();
        //                            Stock data = SearchingList.Where(s => s.SeriNo == serino).SingleOrDefault();
        //                            if (data != null)
        //                            {
        //                                tresult.Result = true;
        //                                tresult.Message = item.Rssi.ToString();
        //                                tresult.Data = data;
        //                            }
        //                        }
        //                    }
        //                }
        //                else
        //                    tresult.Message = "Tag Okunamadı!";
        //            }
        //        }
        //        else
        //        {
        //            tresult.ExceptionResult = true;
        //            tresult.Message = "Bağlantı ayarlarınızı kontrol ediniz!";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        tresult.ExceptionResult = true;
        //        tresult.Message = ex.Message;
        //    }
        //    return tresult;
        //}

        #region Yeni Çip
        //public void Clear()
        //{
        //    if(epclist != null)
        //        epclist.Clear();
        //}
        //public Task<TerminalResult> GetHardware()
        //{
        //    Task<TerminalResult> task = Task.Run(() =>
        //    {
        //        TerminalResult epcc = new TerminalResult();
        //        epcc.Result = false;
        //        epcc.Data = null;
        //        try
        //        {
        //            if (rfidManager == null)
        //            {
        //                rfidManager = UHFRManager.Instance;
        //                rfidManager.SetRegion(Reader.Region_Conf.RgEu3);
        //                rfidManager.SetPower(30, 30);
        //                var deviceRegion = rfidManager.Region;
        //                if (rfidManager == null)
        //                {
        //                    epcc.Message = "Cihaz bağlantısı açılamadı!";
        //                    epcc.Result = false;
        //                    epcc.Data = null;
        //                    return epcc;
        //                }
        //            }
        //            epcc.Data = rfidManager.Hardware;
        //            epcc.Result = true;
        //            epcc.Message = "Başarılı bir şekilde getirildi...";
        //        }
        //        catch (Exception ex)
        //        {
        //            epcc.Message = ex.Message;
        //        }
        //        return epcc;
        //    });
        //    return task;
        //}
        //public Task<TerminalResult> GetPower()
        //{
        //    Task<TerminalResult> task = Task.Run(() =>
        //    {
        //        TerminalResult epcc = new TerminalResult();
        //        epcc.Data = null;
        //        epcc.Result = false;
        //        try
        //        {
        //            if (rfidManager == null)
        //            {
        //                rfidManager = UHFRManager.Instance;
        //                rfidManager.SetRegion(Reader.Region_Conf.RgEu3);
        //                rfidManager.SetPower(30, 30);
        //                var deviceRegion = rfidManager.Region;
        //                if (rfidManager == null)
        //                {
        //                    epcc.Message = "Cihaz bağlantısı açılamadı!";
        //                    epcc.Result = false;
        //                    epcc.Data = null;
        //                    return epcc;
        //                }
        //            }
        //            int[] power = rfidManager.GetPower();
        //            if (power != null)
        //            {
        //                epcc.Result = true;
        //                epcc.Data = power;
        //                epcc.Message = "Etiket başarılı bir şekilde yazdırıldı...";
        //            }
        //            else
        //            {
        //                epcc.Message = "Etiket yazdırılamadı!";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            epcc.Message = ex.Message;
        //        }
        //        return epcc;
        //    });
        //    return task;
        //}
        //public Task<TerminalResult> GetRegion()
        //{
        //    throw new NotImplementedException();
        //}
        //public Task<TerminalResult> GetTagData(int mbank, int startaddr, int len, byte[] rdata, byte[] password, short timeout)
        //{
        //    Task<TerminalResult> task = Task.Run(() =>
        //    {
        //        TerminalResult epcc = new TerminalResult();
        //        epcc.Data = null;
        //        epcc.Result = false;
        //        try
        //        {
        //            if (rfidManager == null)
        //            {
        //                rfidManager = UHFRManager.Instance;
        //                rfidManager.SetRegion(Reader.Region_Conf.RgEu3);
        //                rfidManager.SetPower(30, 30);
        //            }
        //            Reader.READER_ERR error = rfidManager.GetTagData(mbank, startaddr, len, rdata, password, timeout);
        //            if (error == Reader.READER_ERR.MtOkErr)
        //            {
        //                epcc.Result = true;
        //                epcc.Message = "Etiket başarılı bir şekilde yazdırıldı...";
        //            }
        //            else
        //            {
        //                epcc.Message = "Etiket yazdırılamadı!";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            epcc.Message = ex.Message;
        //        }
        //        return epcc;
        //    });
        //    return task;
        //}
        //public Task<TerminalResult> LockTag(byte[] accesspasswd, short timeout)
        //{
        //    Task<TerminalResult> task = Task.Run(() =>
        //    {
        //        TerminalResult epcc = new TerminalResult();
        //        epcc.Data = null;
        //        epcc.Result = false;
        //        try
        //        {
        //            if (rfidManager == null)
        //            {
        //                rfidManager = UHFRManager.Instance;
        //                rfidManager.SetRegion(Reader.Region_Conf.RgEu3);
        //                rfidManager.SetPower(30, 30);
        //            }
        //            Reader.READER_ERR error = rfidManager.LockTag(Reader.Lock_Obj.LockObjectAccessPasswd, Reader.Lock_Type.AccessPasswdLock, accesspasswd, timeout);
        //            if (error == Reader.READER_ERR.MtOkErr)
        //            {
        //                epcc.Result = true;
        //                epcc.Message = "Etiket başarılı bir şekilde kitlendi...";
        //            }
        //            else
        //                epcc.Message = "Etiket kilitlenemedi!";
        //        }
        //        catch (Exception ex)
        //        {
        //            epcc.Message = ex.Message;
        //        }
        //        return epcc;
        //    });
        //    return task;
        //}
        //public Task<TerminalResult> UnLockTag(byte[] accesspasswd, short timeout)
        //{
        //    Task<TerminalResult> task = Task.Run(() =>
        //    {
        //        TerminalResult epcc = new TerminalResult();
        //        epcc.Data = null;
        //        epcc.Result = false;
        //        try
        //        {
        //            if (rfidManager == null)
        //            {
        //                rfidManager = UHFRManager.Instance;
        //                rfidManager.SetRegion(Reader.Region_Conf.RgEu3);
        //                rfidManager.SetPower(30, 30);
        //            }
        //            Reader.READER_ERR error = rfidManager.LockTag(Reader.Lock_Obj.LockObjectAccessPasswd, Reader.Lock_Type.AccessPasswdUnlock, accesspasswd, timeout);
        //            if (error == Reader.READER_ERR.MtOkErr)
        //            {
        //                epcc.Result = true;
        //                epcc.Message = "Etiketin başarılı bir şekilde kilidi açıldı...";
        //            }
        //            else
        //                epcc.Message = "Etiket kilidi açılamadı!";
        //        }
        //        catch (Exception ex)
        //        {
        //            epcc.Message = ex.Message;
        //        }
        //        return epcc;
        //    });
        //    return task;
        //}
        //public Task<TerminalResult> StartReadingAsync()
        //{
        //    Task<TerminalResult> task = Task.Run(() =>
        //    {
        //        TerminalResult epcc = new TerminalResult();
        //        epcc.Result = false;
        //        epcc.Data = null;
        //        try
        //        {
        //            if (rfidManager == null)
        //            {
        //                rfidManager = UHFRManager.Instance;
        //                rfidManager.SetRegion(Reader.Region_Conf.RgEu3);
        //                rfidManager.SetPower(30, 30);
        //                var deviceRegion = rfidManager.Region;
        //                if (rfidManager == null)
        //                {
        //                    epcc.Message = "Cihaz bağlantısı açılamadı!";
        //                    epcc.Result = false;
        //                    epcc.Data = null;
        //                    return epcc;
        //                }
        //            }
        //            Reader.READER_ERR error = rfidManager.AsyncStartReading();
        //            if (error == Reader.READER_ERR.MtOkErr)
        //            {
        //                epcc.Result = true;
        //                epcc.Data = null;
        //                epcc.Message = "Başarılı bir şekilde başlatıldı...";
        //            }
        //            else
        //            {
        //                epcc.Result = false;
        //                epcc.Data = null;
        //                epcc.Message = "Okuma işlemi başlatılamadı!";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            epcc.Message = ex.Message;
        //        }
        //        return epcc;
        //    });
        //    return task;
        //}
        //public Task<TerminalResult> StopTagInventory()
        //{
        //    Task<TerminalResult> result = Task.Run(() =>
        //    {
        //        TerminalResult epcc = new TerminalResult();
        //        epcc.Data = null;
        //        epcc.Result = false;
        //        try
        //        {
        //            if (rfidManager == null)
        //            {
        //                epcc.Message = "Cihaz bağlantısı açık değil!";
        //                return epcc;
        //            }
        //            if (!rfidManager.StopTagInventory())
        //            {
        //                epcc.Message = "İstenmeyen bir hata oluştu!";

        //            }
        //            else
        //            {
        //                epcc.Result = true;
        //                epcc.Message = "Kapatıldı...";
        //                epcc.Data = "";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            epcc.Message = ex.Message;
        //        }
        //        return epcc;
        //    });
        //    return result;
        //}
        //public Task<TerminalResult> TagInventoryByTimer(short readTimer)
        //{
        //    Task<TerminalResult> result = Task.Run(() =>
        //    {
        //        TerminalResult epcc = new TerminalResult();
        //        epcc.Data = null;
        //        epcc.Result = false;
        //        try
        //        {
        //            if (rfidManager == null)
        //            {
        //                rfidManager = UHFRManager.Instance;
        //                rfidManager.SetRegion(Reader.Region_Conf.RgEu3);
        //                rfidManager.SetPower(10, 10);
        //                var deviceRegion = rfidManager.Region;
        //                if (rfidManager == null)
        //                {
        //                    epcc.Message = "Cihaz bağlantısı açılamadı!";
        //                    epcc.Result = false;
        //                    epcc.Data = null;
        //                    return epcc;
        //                }
        //            }
        //            IList<Reader.TAGINFO> tag = rfidManager.TagInventoryByTimer(readTimer);
        //            if (tag == null)
        //            {
        //                epcc.Message = "Tag okunamadı!";
        //            }
        //            else
        //            {
        //                if (tag.Count != 0)
        //                {
        //                    if (epclist == null)
        //                        epclist = new List<TagInfo>();
        //                    byte[] epc = new byte[12];
        //                    string serino = string.Empty;
        //                    foreach (Reader.TAGINFO item in tag)
        //                    {
        //                        #region Eski
        //                        /*
        //                         if (item.Protocol == Reader.SL_TagProtocol.SlTagProtocolNone)
        //                            protocol = 0;
        //                        else if (item.Protocol == Reader.SL_TagProtocol.SlTagProtocolİso180006b)
        //                            protocol = 1;
        //                        else if (item.Protocol == Reader.SL_TagProtocol.SlTagProtocolGen2)
        //                            protocol = 2;
        //                        else if (item.Protocol == Reader.SL_TagProtocol.SlTagProtocolİso180006bUcode)
        //                            protocol = 3;
        //                        else if (item.Protocol == Reader.SL_TagProtocol.SlTagProtocolİpx64)
        //                            protocol = 4;
        //                        else if (item.Protocol == Reader.SL_TagProtocol.SlTagProtocolİpx256)
        //                            protocol = 5;
        //                        //TagInfo info = new TagInfo
        //                        //{
        //                        //    AntennaID = item.AntennaID,
        //                        //    CRC = item.Crc,
        //                        //    RSSI = item.Rssi,
        //                        //    PC = item.Pc,
        //                        //    Frequency = item.Frequency,
        //                        //    ReadCnt = item.ReadCnt,
        //                        //    EmbededData = item.EmbededData,
        //                        //    EmbededDatalen = item.EmbededDatalen,
        //                        //    EpcId = item.EpcId,
        //                        //    Epclen = item.Epclen,
        //                        //    Phase = item.Phase,
        //                        //    protocol = protocol,
        //                        //    Res = item.Res,
        //                        //    TimeStamp = item.TimeStamp,
        //                        //};
        //                         */
        //                        #endregion
        //                        if (item.Epclen == 12)
        //                        {
        //                            for (int i = 0; i < 12; i++)
        //                            {
        //                                epc[i] = item.EpcId[i];
        //                            }
        //                            serino = ConvertHelper.ByteToTexts(epc).Replace('*',' ').Trim();
        //                            TagInfo linfo = epclist.FirstOrDefault(s => s.SeriNo == serino);
        //                            if (linfo == null)
        //                            {
        //                                TagInfo info = new TagInfo
        //                                {
        //                                    ID = epclist.Count + 1,
        //                                    SeriNo = serino,
        //                                    Count = 1
        //                                };
        //                                epclist.Add(info);
        //                            }
        //                            else
        //                            {
        //                                epclist[linfo.ID - 1].Count += 1;
        //                            }
        //                        }
        //                    }
        //                    epcc.Result = true;
        //                    epcc.Message = "Tag geldi...";
        //                    epcc.Data = epclist;
        //                }
        //                else
        //                    epcc.Message = "Tag Okunamadı!";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            epcc.Message = ex.Message;
        //            epcc.ExceptionResult = true;
        //        }
        //        return epcc;
        //    });
        //    return result;
        //}
        //public Task<TerminalResult> TagInventoryRealTime()
        //{
        //    Task<TerminalResult> result = Task.Run(() =>
        //      {
        //          TerminalResult epcc= new TerminalResult();
        //          epcc.Result= false;
        //          epcc.Data = null;
        //          try
        //          {
        //              if (rfidManager == null)
        //              {
        //                  rfidManager = UHFRManager.Instance;
        //                  rfidManager.SetRegion(Reader.Region_Conf.RgEu3);
        //                  rfidManager.SetPower(30, 30);
        //                  var deviceRegion = rfidManager.Region;
        //                  if (rfidManager == null)
        //                  {
        //                      epcc.Message = "Cihaz bağlantısı açılamadı!";
        //                      epcc.Result = false;
        //                      epcc.Data = null;
        //                      return epcc;
        //                  }
        //              }
        //              IList<Reader.TAGINFO> tagList = rfidManager.TagInventoryRealTime();
        //              if (tagList == null)
        //              {
        //                  epcc.Message = "Tag okunamadı!";
        //              }
        //              else
        //              {
        //                  if (tagList.Count == 0)
        //                  {
        //                      epcc.Message = "Tag okunamadı!";
        //                  }
        //                  else
        //                  {
        //                      if(epclist==null)
        //                          epclist = new List<TagInfo>();
        //                      byte[] epc = new byte[12];
        //                      string serino = string.Empty;
        //                      foreach (Reader.TAGINFO item in tagList)
        //                      {
        //                          #region Eski
        //                          /*
        //                           if (item.Protocol == Reader.SL_TagProtocol.SlTagProtocolNone)
        //                              protocol = 0;
        //                          else if (item.Protocol == Reader.SL_TagProtocol.SlTagProtocolİso180006b)
        //                              protocol = 1;
        //                          else if (item.Protocol == Reader.SL_TagProtocol.SlTagProtocolGen2)
        //                              protocol = 2;
        //                          else if (item.Protocol == Reader.SL_TagProtocol.SlTagProtocolİso180006bUcode)
        //                              protocol = 3;
        //                          else if (item.Protocol == Reader.SL_TagProtocol.SlTagProtocolİpx64)
        //                              protocol = 4;
        //                          else if (item.Protocol == Reader.SL_TagProtocol.SlTagProtocolİpx256)
        //                              protocol = 5;
        //                          //TagInfo info = new TagInfo
        //                          //{
        //                          //    AntennaID = item.AntennaID,
        //                          //    CRC = item.Crc,
        //                          //    RSSI = item.Rssi,
        //                          //    PC = item.Pc,
        //                          //    Frequency = item.Frequency,
        //                          //    ReadCnt = item.ReadCnt,
        //                          //    EmbededData = item.EmbededData,
        //                          //    EmbededDatalen = item.EmbededDatalen,
        //                          //    EpcId = item.EpcId,
        //                          //    Epclen = item.Epclen,
        //                          //    Phase = item.Phase,
        //                          //    protocol = protocol,
        //                          //    Res = item.Res,
        //                          //    TimeStamp = item.TimeStamp,
        //                          //};
        //                           */
        //                          #endregion
        //                          for (int i = 0; i < 12; i++)
        //                          {
        //                              epc[i] = item.EpcId[i];
        //                          }
        //                          serino = ConvertHelper.ByteToTexts(epc);
        //                          TagInfo linfo = epclist.SingleOrDefault(s => s.SeriNo == serino);
        //                          if (linfo == null)
        //                          {
        //                              epclist.Add(new TagInfo
        //                              {
        //                                  ID = epclist.Count + 1,
        //                                  SeriNo = serino,
        //                                  Count=1
        //                              });
        //                          }
        //                          else
        //                          {
        //                              epclist[linfo.ID - 1].Count += 1;
        //                          }
        //                      }
        //                      #region Eski
        //                      //int protocol = 0;
        //                      //foreach (Reader.TAGINFO item in tagList)
        //                      //{
        //                      //    if (item.Protocol == Reader.SL_TagProtocol.SlTagProtocolNone)
        //                      //        protocol = 0;
        //                      //    else if (item.Protocol == Reader.SL_TagProtocol.SlTagProtocolİso180006b)
        //                      //        protocol = 1;
        //                      //    else if (item.Protocol == Reader.SL_TagProtocol.SlTagProtocolGen2)
        //                      //        protocol = 2;
        //                      //    else if (item.Protocol == Reader.SL_TagProtocol.SlTagProtocolİso180006bUcode)
        //                      //        protocol = 3;
        //                      //    else if (item.Protocol == Reader.SL_TagProtocol.SlTagProtocolİpx64)
        //                      //        protocol = 4;
        //                      //    else if (item.Protocol == Reader.SL_TagProtocol.SlTagProtocolİpx256)
        //                      //        protocol = 5;
        //                      //TagInfo info = new TagInfo
        //                      //{
        //                      //    AntennaID = item.AntennaID,
        //                      //    CRC = item.Crc,
        //                      //    RSSI = item.Rssi,
        //                      //    PC = item.Pc,
        //                      //    Frequency = item.Frequency,
        //                      //    ReadCnt = item.ReadCnt,
        //                      //    EmbededData = item.EmbededData,
        //                      //    EmbededDatalen = item.EmbededDatalen,
        //                      //    EpcId = item.EpcId,
        //                      //    Epclen = item.Epclen,
        //                      //    Phase = item.Phase,
        //                      //    protocol = protocol,
        //                      //    Res = item.Res,
        //                      //    TimeStamp = item.TimeStamp,
        //                      //};
        //                      // tagInfo.Add(info);
        //                      //}
        //                      #endregion
        //                      epcc.Result = true;
        //                      epcc.Message = "Tag geldi...";
        //                      epcc.Data = epclist;
        //                  }
        //              }
        //          }
        //          catch (Exception ex)
        //          {
        //              epcc.Message=ex.Message;
        //          }
        //          return epcc;
        //      });
        //    return result;

        //}
        //public Task<TerminalResult> WriteTagData(char mbank, int startaddress, byte[] data, int datalen, byte[] accesspasswd, short timeout)
        //{
        //    Task<TerminalResult> task = Task.Run(() =>
        //   {
        //       TerminalResult epcc = new TerminalResult();
        //       epcc.Data = null;
        //       epcc.Result = false;
        //       try
        //       {
        //           if (rfidManager == null)
        //           {
        //               rfidManager = UHFRManager.Instance;
        //               rfidManager.SetRegion(Reader.Region_Conf.RgEu3);
        //               rfidManager.SetPower(30, 30);
        //               var deviceRegion = rfidManager.Region;
        //               if (rfidManager == null)
        //               {
        //                   epcc.Message = "Cihaz bağlantısı açılamadı!";
        //                   epcc.Result = false;
        //                   epcc.Data = null;
        //                   return epcc;
        //               }
        //           }
        //           Reader.READER_ERR error = rfidManager.WriteTagData(mbank, startaddress, data, datalen, accesspasswd, timeout);
        //           if (error == Reader.READER_ERR.MtOkErr)
        //           {
        //               epcc.Result = true;
        //               epcc.Data = data;
        //               epcc.Message = "Etiket başarılı bir şekilde yazdırıldı...";
        //           }
        //           else
        //           {
        //               epcc.Message = "Etiket yazdırılamadı!";
        //               epcc.Result = false;
        //               epcc.Data = null;
        //           }

        //       }
        //       catch (Exception ex)
        //       {
        //           epcc.Message = ex.Message;
        //       }
        //       return epcc;
        //   });
        //    return task;
        //}
        //public Task<TerminalResult> WriteTagEPC(byte[] data, byte[] accesspwd, short timeout)
        //{
        //    Task<TerminalResult> result = Task.Run(() =>
        //   {
        //       TerminalResult epcc = new TerminalResult();
        //       epcc.Data = null;
        //       epcc.Result = false;
        //       try
        //       {
        //           if (rfidManager == null)
        //           {
        //               rfidManager = UHFRManager.Instance;
        //               rfidManager.SetRegion(Reader.Region_Conf.RgEu3);
        //               rfidManager.SetPower(30, 30);
        //               var deviceRegion = rfidManager.Region;
        //               if (rfidManager == null)
        //               {
        //                   epcc.Message = "Cihaz bağlantısı açılamadı!";
        //                   epcc.Result = false;
        //                   epcc.Data = null;
        //                   return epcc;
        //               }
        //           }
        //           Reader.READER_ERR error = rfidManager.WriteTagEPC(data, accesspwd, timeout);
        //           if (error == Reader.READER_ERR.MtOkErr)
        //           {
        //               epcc.Result = true;
        //               epcc.Data = data;
        //               epcc.Message = "Etiket başarılı bir şekilde yazdırıldı...";
        //           }
        //           else
        //           {
        //               epcc.Message = "Etiket yazdırılamadı!";
        //               epcc.Result = false;
        //               epcc.Data = null;
        //           }

        //       }
        //       catch (Exception ex)
        //       {
        //           epcc.Message = ex.Message;
        //       }
        //       return epcc;
        //   });
        //    return result;
        //}
        #endregion
    }
}