using AbcMobil.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AbcMobil.Services
{
    public interface IUHFService
    {
        #region Yeni Çip
        TerminalResult WriteSerialNumber(string txtData, int readPower, int writePower);
        TerminalResult ReadSerialNumber(int readPower, int writePower);
        TerminalResult WriteStockCode(string txtData, int readPower, int writePower);
        TerminalResult ReadStockCode(int readPower, int writePower);
        TerminalResult WriteTag(string txtData, int readPower, int writePower);
        TerminalResult ReadTagg(int readPower, int writePower);
        Task<TerminalResult> ReadTag(int readPower, int writePower);
        TerminalResult InventorySerialNumberr(short readTimer = 200, int readPower=33, int writePower=33);
        Task<TerminalResult> InventorySerialNumber(short readTimer = 200, int readPower = 33, int writePower = 33);
        Task<TerminalResult> InventoryStockCode(short readTimer = 200, int readPower = 33, int writePower = 33);
        Task<TerminalResult> TagInventory(short readTimer = 200, int readPower = 33, int writePower = 33);
        Task<TerminalResult> MultiInventory(short readTimer = 200, int readPower = 33, int writePower = 33);
        TerminalResult SearchingSerialNumber(IList<Stock> SearchingList, int readPower, int writePower);
        TerminalResult LockSerialNumber(int readPower, int writePower);
        TerminalResult LockStockCode(int readPower, int writePower);
        TerminalResult LockPassword(int readPower, int writePower);
        TerminalResult UnLockSerialNumber(int readPower, int writePower);
        TerminalResult UnLockStockCode(int readPower, int writePower);
        TerminalResult UnLockPassword(int readPower, int writePower);
        TerminalResult Clear();
        void CensusSerialNumberDelete(string seriNo);
        bool CloseDown();
        void CloseDownUpdate();
        void CloseDownUp();
        void TicketMatching(IList<Stock> stockk, string rafCode);
        void Matching(Stock info);
        void RemoveEpc(string seriNo);
        void RemoveTag(string tag);
        TerminalResult Close();
        TerminalResult Open();
        TerminalResult DeviceInfo();
        TerminalResult UsbScan();
        TerminalResult UsbStop();
        //Task<TerminalResult> GetHardware();
        //Task<TerminalResult> StartReadingAsync();
        //Task<TerminalResult> StopReadingAsync();
        //Task<TerminalResult> SetInventoryFilter(byte[] fdata, int fbank, int fstartaddr, bool matching);
        //Task<TerminalResult> SetCancleInventoryFilter();
        //Task<TerminalResult> TagInventoryRealTime();
        //Task<TerminalResult> StopTagInventory();
        //Task<TerminalResult> TagInventoryByTimer(short readTimer);
        //Task<TerminalResult> GetTagData(int mbank, int startaddr, int len, byte[] rdata, byte[] password, short timeout);
        //Task<TerminalResult> GetTagDataByFilter(int mbank, int startaddr, int len, byte[] password, short timeout, byte[] fdata, int fbank, int fstartaddr, bool matching);
        //Task<TerminalResult> WriteTagData(char mbank, int startaddress, byte[] data, int datalen, byte[] accesspasswd, short timeout);
        //Task<TerminalResult> WriteTagDataByFilter(char mbank, int startaddress, byte[] data, int datalen, byte[] accesspasswd, short timeout, byte[] fdata, int fbank, int fstartaddr, bool matching);
        //Task<TerminalResult> WriteTagEPC(byte[] data, byte[] accesspwd, short timeout);
        //Task<TerminalResult> WriteTagEPCByFilter(byte[] data, byte[] accesspwd, short timeout, byte[] fdata, int fbank, int fstartaddr, bool matching);
        //Task<TerminalResult> LockTag(byte[] accesspasswd, short timeout);
        //Task<TerminalResult> UnLockTag(byte[] accesspasswd, short timeout);
        //Task<TerminalResult> LockTagByFilter(int lockobject, int locktype, byte[] accesspasswd, short timeout, byte[] fdata, int fbank, int fstartaddr, bool matching);
        //Task<TerminalResult> KillTag(byte[] killpasswd, short timeout);
        //Task<TerminalResult> KillTagByFilter(byte[] killpasswd, short timeout, byte[] fdata, int fbank, int fstartaddr, bool matching);
        //Task<TerminalResult> GetRegion();
        //Task<TerminalResult> GetFrequencyPoints();
        //Task<TerminalResult> SetFrequencyPoints(int[] frequencyPoints);
        //Task<TerminalResult> SetPower(int readPower, int writePower);
        //Task<TerminalResult> GetPower();
        //Task<TerminalResult> SetFastMode();
        //Task<TerminalResult> GetTemperature();
        //Task<TerminalResult> Close();
        #endregion
    }
}
