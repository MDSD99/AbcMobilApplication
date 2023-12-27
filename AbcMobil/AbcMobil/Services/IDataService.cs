using AbcMobil.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AbcMobil.Services
{
    public interface IDataService
    {
        //Task<MobileResult> GetShelf(string data);
        //Task<MobileResult> GetTags(string data);
        //Task<MobileResult> PostTags(Stock data,string rafCode);
        //Task<MobileResult> PostTags(IList<Stock> data,string rafCode);
        //---------------------------------------------------------------------------
        Task<MobileResult> GetFullStore();
        Task<MobileResult> GetSimpleStoreData(string serialNumber);
        Task<MobileResult> GetSimpleStoreDataTempTag(string serialNumber);
        Task<MobileResult> GetSimpleStoreDataTempTag4MamulCode(string serialNumber);
        Task<MobileResult> GetStoreData(string serialNumber);
        Task<MobileResult> GetOutsideTag();
        Task<MobileResult> GetFullCellInfo();
        Task<MobileResult> GetFullCell();
        Task<MobileResult> GetCell(string tagNumber);
        Task<MobileResult> PostCellMatching(IList<Stock> productList, string tagNumber);
        Task<MobileResult> PostCellListMatching(IList<Stock> productList, string tagNumber);
        Task<MobileResult> PostCellListTempMatching(IList<Stock> productList, string tagNumber);
        Task<MobileResult> PostWarehouseLabels(IList<Stock> productList);
        Task<MobileResult> PostCellReplace(IList<Stock> productList, string tagNumber);
        Task<MobileResult> PostCellExit(IList<Stock> data);
        Task<MobileResult> PostStoreImport(IList<Stock> productList);
        Task<MobileResult> PostCensusTag(int tagAmount, int ticketAmount);
        Task<MobileResult> PostCensusTagList(IList<Stock> tagList);
        Task<MobileResult> PostCensusTicketList(IList<Stock> tagList);
        Task<MobileResult> PostCensus(int ticketAmount);
        Task<MobileResult> GetCensusReportList(int typee);
        Task<MobileResult> GetCheckedStoreData(IList<Stock> productList);
        Task<Token> GetToken(string username , string password);

    }
}
