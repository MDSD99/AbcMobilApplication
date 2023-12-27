using AbcMobil.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AbcMobil.ViewModels;

namespace AbcMobil.Services
{
    public class DataService : IDataService
    {
        private readonly string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Ik1laG1ldCIsIkpvYiI6IkhhbmRoZWxkVGVybWluYWwiLCJQYXNzd29yZCI6IjE5MDUiLCJDb21wYW55IjoiUWVudFNvZnQiLCJSb2xlIjoiQWRtaW4iLCJuYmYiOjE2NDY0NzQ5MzksImV4cCI6MTY3ODAxMDkzOSwiaWF0IjoxNjQ2NDc0OTM5fQ.SfYKpQeTWPdq3kdpRdknd4avs2svhh6CRJ546l89imQ";
        //private readonly string Url = "http://192.168.0.189:2121/swagger/index.html";
        //private readonly string Url = "http://192.168.1.35:9503"; //ismail test
        private readonly string Url = "http://192.168.0.190:7657"; // abc sunucu
        //public async Task<MobileResult> GetShelf(string data)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        MobileResult mobileResult;
        //        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        //        HttpResponseMessage responseMessage = await client.GetAsync(new Uri($"{Url}/Hareket/{data}"));
        //        string content = await responseMessage.Content.ReadAsStringAsync();
        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            mobileResult = JsonConvert.DeserializeObject<MobileResult>(content);
        //            if (mobileResult != null)
        //                mobileResult.Data = JsonConvert.DeserializeObject<IList<Stock>>(mobileResult.Data.ToString());
        //        }
        //        else
        //            mobileResult = new MobileResult { Data = null, ExceptionResult = true, Message = content, Result = false };
        //        return mobileResult;
        //    }
        //}
        //public async Task<MobileResult> GetTags(string data)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        if (data != "-")
        //        {
        //            int dell = data.IndexOf('-');
        //            if (dell == -1)
        //                data = data.Substring(0, 8) + "-" + data.Substring(8, 4);
        //        }
        //        MobileResult mobileResult;
        //        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        //        HttpResponseMessage responseMessage = await client.GetAsync(new Uri($"{Url}/SeriTanim/{data}"));
        //        string content = await responseMessage.Content.ReadAsStringAsync();
        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            mobileResult = JsonConvert.DeserializeObject<MobileResult>(content);
        //            if (mobileResult != null)
        //                mobileResult.Data = JsonConvert.DeserializeObject<IList<Stock>>(mobileResult.Data.ToString());//,new JsonSerializerSettings
        //                                                                                                              //});
        //        }
        //        else
        //            mobileResult = new MobileResult { Data = null, ExceptionResult = true, Message = "İnternet bağlantınızı kontrol ediniz!", Result = false };
        //        return mobileResult;
        //    }
        //}
        //public async Task<MobileResult> PostTags(Stock data, string rafCode)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        MobileResult mobileResult;
        //        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        //        HttpResponseMessage responseMessage = await client.PostAsync(new Uri($"{Url}/Hareket/{rafCode}"), new StringContent(JsonConvert.SerializeObject(data)));
        //        string content = await responseMessage.Content.ReadAsStringAsync();
        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            mobileResult = JsonConvert.DeserializeObject<MobileResult>(content);
        //            if (mobileResult != null)
        //                mobileResult.Data = JsonConvert.DeserializeObject<IList<Stock>>(mobileResult.Data.ToString());
        //        }
        //        else
        //            mobileResult = new MobileResult { Data = null, ExceptionResult = true, Message = content, Result = false };
        //        return mobileResult;
        //    }
        //}
        //public async Task<MobileResult> PostTags(IList<Stock> data, string rafCode)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        HttpContent content1 = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        //        HttpRequestMessage request = new HttpRequestMessage
        //        {
        //            Method = HttpMethod.Post,
        //            RequestUri = new Uri($"{Url}/Hareket/list?rafcode={rafCode}"),
        //        };
        //        MobileResult mobileResult;
        //        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        //        //http://localhost/Hareket/list?rafcode=1

        //        HttpResponseMessage responseMessage = await client.PostAsync(request.RequestUri, content1);
        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            string content = await responseMessage.Content.ReadAsStringAsync();
        //            mobileResult = JsonConvert.DeserializeObject<MobileResult>(content);
        //            if (mobileResult != null)
        //                mobileResult.Data = JsonConvert.DeserializeObject<IList<Stock>>(mobileResult.Data.ToString());
        //        }
        //        else
        //            mobileResult = new MobileResult { Data = null, ExceptionResult = true, Message = "Lütfen internet bağlatınızı kontrol ediniz!", Result = false };
        //        return mobileResult;
        //    }
        //}


        //--------------------------------------------------------------------------------------------

        public async Task<Token> GetToken(string username, string password)
        {
            Token token = null;
            
            using (HttpClient client = new HttpClient())
            {
                var user = new User
                {
                    UserName = username,
                    Password = password
                };
                var jsonUser = JsonConvert.SerializeObject(user);
                HttpContent httpContent = new StringContent(jsonUser);
                httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(mediaType: "application/json");

                try
                {
                    HttpResponseMessage responseMessage = await client.PostAsync(requestUri: new Uri($"{Url}/api/Authenticate/login"), httpContent);
                    string content = await responseMessage.Content.ReadAsStringAsync();
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        token = JsonConvert.DeserializeObject<Token>(content);
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return token;
        }
        public async Task<MobileResult> GetFullStore()
        {
            using (HttpClient client = new HttpClient())
            {
                MobileResult mobileResult;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", MainViewModel.UserToken);
                HttpResponseMessage responseMessage = await client.GetAsync(new Uri($"{Url}/FullStore"));
                string content = await responseMessage.Content.ReadAsStringAsync();
                if (responseMessage.IsSuccessStatusCode)
                {
                    mobileResult = JsonConvert.DeserializeObject<MobileResult>(content);
                    if (mobileResult != null)
                        mobileResult.Data = JsonConvert.DeserializeObject<IList<Stock>>(mobileResult.Data.ToString());
                }
                else
                    mobileResult = new MobileResult { Data = null, ExceptionResult = true, Message = "Bağlantılarınızı kontrol ediniz!", Result = false };
                return mobileResult;
            }
        }
        public async Task<MobileResult> GetSimpleStoreData(string serialNumber)
        {
            using (HttpClient client = new HttpClient())
            {
                MobileResult mobileResult;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", MainViewModel.UserToken);
                HttpResponseMessage responseMessage = await client.GetAsync(new Uri($"{Url}/SimpleStoreData/{serialNumber}"));
                string content = await responseMessage.Content.ReadAsStringAsync();
                if (responseMessage.IsSuccessStatusCode)
                {
                    mobileResult = JsonConvert.DeserializeObject<MobileResult>(content);
                    if (mobileResult != null)
                    {
                        if (mobileResult.Data != null)
                            mobileResult.Data = JsonConvert.DeserializeObject<Stock>(mobileResult.Data.ToString());
                        if (mobileResult.ExceptionResult)
                            mobileResult = new MobileResult { Data = serialNumber, ExceptionResult = true, Message = mobileResult.Message, Result = false };
                    }
                }
                else
                    mobileResult = new MobileResult { Data = serialNumber, ExceptionResult = true, Message = "Bağlantılarınızı kontrol ediniz!", Result = false };
                return mobileResult;
            }
        }
        public async Task<MobileResult> GetSimpleStoreDataTempTag(string serialNumber)
        {
            using (HttpClient client = new HttpClient())
            {
                MobileResult mobileResult;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", MainViewModel.UserToken);
                HttpResponseMessage responseMessage = await client.GetAsync(new Uri($"{Url}/SimpleStoreDataTempTag/{serialNumber}"));
                string content = await responseMessage.Content.ReadAsStringAsync();
                if (responseMessage.IsSuccessStatusCode)
                {
                    mobileResult = JsonConvert.DeserializeObject<MobileResult>(content);
                    if (mobileResult != null)
                    {
                        if (mobileResult.Data != null)
                            mobileResult.Data = JsonConvert.DeserializeObject<Stock>(mobileResult.Data.ToString());
                        if (mobileResult.ExceptionResult)
                            mobileResult = new MobileResult { Data = serialNumber, ExceptionResult = true, Message = mobileResult.Message, Result = false };
                    }
                }
                else
                    mobileResult = new MobileResult { Data = serialNumber, ExceptionResult = true, Message = "Bağlantılarınızı kontrol ediniz!", Result = false };
                return mobileResult;
            }
        }
        public async Task<MobileResult> GetSimpleStoreDataTempTag4MamulCode(string serialNumber)
        {
            using (HttpClient client = new HttpClient())
            {
                MobileResult mobileResult;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", MainViewModel.UserToken);
                HttpResponseMessage responseMessage = await client.GetAsync(new Uri($"{Url}/SimpleStoreDataTempTag4MamulCode/{serialNumber}"));
                string content = await responseMessage.Content.ReadAsStringAsync();
                if (responseMessage.IsSuccessStatusCode)
                {
                    mobileResult = JsonConvert.DeserializeObject<MobileResult>(content);
                    if (mobileResult != null)
                    {
                        if (mobileResult.Data != null)
                            mobileResult.Data = JsonConvert.DeserializeObject<Stock>(mobileResult.Data.ToString());
                        if (mobileResult.ExceptionResult)
                            mobileResult = new MobileResult { Data = serialNumber, ExceptionResult = true, Message = mobileResult.Message, Result = false };
                    }
                }
                else
                    mobileResult = new MobileResult { Data = serialNumber, ExceptionResult = true, Message = "Bağlantılarınızı kontrol ediniz!", Result = false };
                return mobileResult;
            }
        }
        public async Task<MobileResult> GetStoreData(string serialNumber)
        {
            using (HttpClient client = new HttpClient())
            {
                MobileResult mobileResult;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", MainViewModel.UserToken);
                HttpResponseMessage responseMessage = await client.GetAsync(new Uri($"{Url}/StoreData/{serialNumber}"));
                string content = await responseMessage.Content.ReadAsStringAsync();
                if (responseMessage.IsSuccessStatusCode)
                {
                    mobileResult = JsonConvert.DeserializeObject<MobileResult>(content);
                    if (mobileResult != null)
                    {
                        if (mobileResult.Data != null)
                            mobileResult.Data = JsonConvert.DeserializeObject<Stock>(mobileResult.Data.ToString());
                        if (mobileResult.ExceptionResult)
                            mobileResult = new MobileResult { Data = serialNumber, ExceptionResult = true, Message = mobileResult.Message, Result = false };
                    }
                }
                else
                    mobileResult = new MobileResult { Data = serialNumber, ExceptionResult = true, Message = "Bağlantılarınızı kontrol ediniz!", Result = false };
                return mobileResult;
            }
        }

        public async Task<MobileResult> GetOutsideTag()
        {
            using (HttpClient client = new HttpClient())
            {
                MobileResult mobileResult;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", MainViewModel.UserToken);
                HttpResponseMessage responseMessage = await client.GetAsync(new Uri($"{Url}/OutsideTag"));
                string content = await responseMessage.Content.ReadAsStringAsync();
                if (responseMessage.IsSuccessStatusCode)
                {
                    mobileResult = JsonConvert.DeserializeObject<MobileResult>(content);
                    if (mobileResult != null)
                        mobileResult.Data = JsonConvert.DeserializeObject<IList<Stock>>(mobileResult.Data.ToString());
                }
                else
                    mobileResult = new MobileResult { Data = null, ExceptionResult = true, Message = "Bağlantılarınızı kontrol ediniz!", Result = false };
                return mobileResult;
            }
        }
        public async Task<MobileResult> GetFullCellInfo()
        {
            using (HttpClient client = new HttpClient())
            {
                MobileResult mobileResult;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", MainViewModel.UserToken);
                HttpResponseMessage responseMessage = await client.GetAsync(new Uri($"{Url}/FullCellInfo"));
                string content = await responseMessage.Content.ReadAsStringAsync();
                if (responseMessage.IsSuccessStatusCode)
                {
                    mobileResult = JsonConvert.DeserializeObject<MobileResult>(content);
                    if (mobileResult != null)
                        mobileResult.Data = JsonConvert.DeserializeObject<IList<Stock>>(mobileResult.Data.ToString());
                }
                else
                    mobileResult = new MobileResult { Data = null, ExceptionResult = true, Message = "Bağlantılarınızı kontrol ediniz!", Result = false };
                return mobileResult;
            }
        }
        public async Task<MobileResult> GetFullCell()
        {
            using (HttpClient client = new HttpClient())
            {
                MobileResult mobileResult;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", MainViewModel.UserToken);
                HttpResponseMessage responseMessage = await client.GetAsync(new Uri($"{Url}/FullCell"));
                string content = await responseMessage.Content.ReadAsStringAsync();
                if (responseMessage.IsSuccessStatusCode)
                {
                    mobileResult = JsonConvert.DeserializeObject<MobileResult>(content);
                    if (mobileResult != null)
                        mobileResult.Data = JsonConvert.DeserializeObject<IList<Stock>>(mobileResult.Data.ToString());
                }
                else
                    mobileResult = new MobileResult { Data = null, ExceptionResult = true, Message = "Bağlantılarınızı kontrol ediniz!", Result = false };
                return mobileResult;
            }
        }
        public async Task<MobileResult> GetCell(string tagNumber)
        {
            using (HttpClient client = new HttpClient())
            {
                MobileResult mobileResult;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", MainViewModel.UserToken);
                HttpResponseMessage responseMessage = await client.GetAsync(new Uri($"{Url}/Cell/{tagNumber}"));
                string content = await responseMessage.Content.ReadAsStringAsync();
                if (responseMessage.IsSuccessStatusCode)
                {
                    mobileResult = JsonConvert.DeserializeObject<MobileResult>(content);
                    if (mobileResult != null)
                        try
                        {
                            mobileResult.Data = JsonConvert.DeserializeObject<IList<Stock>>(mobileResult.Data.ToString());
                        }
                        catch (Exception ex)
                        {
                            mobileResult = new MobileResult { Data = null, ExceptionResult = true, Message = $"Bağlantılarınızı kontrol ediniz! - {ex.Message}", Result = false };
                        }
                       
                }
                else
                    mobileResult = new MobileResult { Data = null, ExceptionResult = true, Message = "Bağlantılarınızı kontrol ediniz!", Result = false };
                return mobileResult;
            }
        }
        public async Task<MobileResult> PostCellMatching(IList<Stock> productList, string tagNumber)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpContent content = new StringContent(JsonConvert.SerializeObject(productList), Encoding.UTF8, "application/json");
                MobileResult mobileResult;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", MainViewModel.UserToken);
                HttpResponseMessage responseMessage = await client.PostAsync(new Uri($"{Url}/CellMatching/{tagNumber}"), content);
                if (responseMessage.IsSuccessStatusCode)
                {
                    string contentMessage = await responseMessage.Content.ReadAsStringAsync();
                    mobileResult = JsonConvert.DeserializeObject<MobileResult>(contentMessage);
                    if (mobileResult != null)
                        mobileResult.Data = JsonConvert.DeserializeObject<IList<Stock>>(mobileResult.Data.ToString());
                }
                else
                    mobileResult = new MobileResult { Data = null, ExceptionResult = true, Message = "Lütfen internet bağlatınızı kontrol ediniz!", Result = false };
                return mobileResult;
            }
        }
        public async Task<MobileResult> PostCellListMatching(IList<Stock> productList, string tagNumber)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpContent content = new StringContent(JsonConvert.SerializeObject(productList), Encoding.UTF8, "application/json");
                MobileResult mobileResult;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", MainViewModel.UserToken);
                HttpResponseMessage responseMessage = await client.PostAsync(new Uri($"{Url}/CellListMatching/{tagNumber}"), content);
                if (responseMessage.IsSuccessStatusCode)
                {
                    string contentMessage = await responseMessage.Content.ReadAsStringAsync();
                    mobileResult = JsonConvert.DeserializeObject<MobileResult>(contentMessage);
                    if (mobileResult != null)
                        mobileResult.Data = JsonConvert.DeserializeObject<IList<Stock>>(mobileResult.Data.ToString());
                }
                else
                    mobileResult = new MobileResult { Data = null, ExceptionResult = true, Message = "Lütfen internet bağlatınızı kontrol ediniz!", Result = false };
                return mobileResult;
            }
        }
        public async Task<MobileResult> PostCellListTempMatching(IList<Stock> productList, string tagNumber)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpContent content = new StringContent(JsonConvert.SerializeObject(productList), Encoding.UTF8, "application/json");
                MobileResult mobileResult;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", MainViewModel.UserToken);
                HttpResponseMessage responseMessage = await client.PostAsync(new Uri($"{Url}/CellListTempMatching/{tagNumber}"), content);
                if (responseMessage.IsSuccessStatusCode)
                {
                    string contentMessage = await responseMessage.Content.ReadAsStringAsync();
                    mobileResult = JsonConvert.DeserializeObject<MobileResult>(contentMessage);
                    if (mobileResult != null)
                        mobileResult.Data = JsonConvert.DeserializeObject<IList<Stock>>(mobileResult.Data.ToString());
                }
                else
                    mobileResult = new MobileResult { Data = null, ExceptionResult = true, Message = "Lütfen internet bağlatınızı kontrol ediniz!", Result = false };
                return mobileResult;
            }
        }
        public async Task<MobileResult> PostCellReplace(IList<Stock> productList, string tagNumber)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpContent content = new StringContent(JsonConvert.SerializeObject(productList), Encoding.UTF8, "application/json");
                MobileResult mobileResult;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", MainViewModel.UserToken);
                HttpResponseMessage responseMessage = await client.PostAsync(new Uri($"{Url}/CellReplace/{tagNumber}"), content);
                if (responseMessage.IsSuccessStatusCode)
                {
                    string contentMessage = await responseMessage.Content.ReadAsStringAsync();
                    mobileResult = JsonConvert.DeserializeObject<MobileResult>(contentMessage);
                    if (mobileResult != null)
                        mobileResult.Data = JsonConvert.DeserializeObject<IList<Stock>>(mobileResult.Data.ToString());
                }
                else
                    mobileResult = new MobileResult { Data = null, ExceptionResult = true, Message = "Lütfen internet bağlatınızı kontrol ediniz!", Result = false };
                return mobileResult;
            }
        }
        public async Task<MobileResult> PostCellExit(IList<Stock> productList)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpContent content = new StringContent(JsonConvert.SerializeObject(productList), Encoding.UTF8, "application/json");
                MobileResult mobileResult;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", MainViewModel.UserToken);
                HttpResponseMessage responseMessage = await client.PostAsync(new Uri($"{Url}/CellExit"), content);
                if (responseMessage.IsSuccessStatusCode)
                {
                    string contentMessage = await responseMessage.Content.ReadAsStringAsync();
                    mobileResult = JsonConvert.DeserializeObject<MobileResult>(contentMessage);
                    if (mobileResult != null)
                        if (mobileResult.Result == true)
                            mobileResult.Data = JsonConvert.DeserializeObject<IList<Stock>>(mobileResult.Data.ToString());
                }
                else
                    mobileResult = new MobileResult { Data = null, ExceptionResult = true, Message = "Lütfen internet bağlatınızı kontrol ediniz!", Result = false };
                return mobileResult;
            }
        }
        public async Task<MobileResult> PostStoreImport(IList<Stock> productList)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpContent content = new StringContent(JsonConvert.SerializeObject(productList), Encoding.UTF8, "application/json");
                MobileResult mobileResult;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", MainViewModel.UserToken);
                HttpResponseMessage responseMessage = await client.PostAsync(new Uri($"{Url}/StoreImport"), content);
                if (responseMessage.IsSuccessStatusCode)
                {
                    string contentMessage = await responseMessage.Content.ReadAsStringAsync();
                    mobileResult = JsonConvert.DeserializeObject<MobileResult>(contentMessage);
                    if (mobileResult != null)
                        if (mobileResult.Result == true)
                            mobileResult.Data = JsonConvert.DeserializeObject<IList<Stock>>(mobileResult.Data.ToString());
                }
                else
                    mobileResult = new MobileResult { Data = null, ExceptionResult = true, Message = "Lütfen internet bağlatınızı kontrol ediniz!", Result = false };
                return mobileResult;
            }
        }
        public async Task<MobileResult> PostCensusTag(int tagAmount, int ticketAmount)
        {
            using (HttpClient client = new HttpClient())
            {
                MobileResult mobileResult;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", MainViewModel.UserToken);
                HttpResponseMessage responseMessage = await client.GetAsync(new Uri($"{Url}/CensusTag/{tagAmount}/{ticketAmount}"));
                if (responseMessage.IsSuccessStatusCode)
                {
                    string contentMessage = await responseMessage.Content.ReadAsStringAsync();
                    mobileResult = JsonConvert.DeserializeObject<MobileResult>(contentMessage);
                    if (mobileResult != null)
                        mobileResult.Data = JsonConvert.DeserializeObject<int>(mobileResult.Data.ToString());
                }
                else
                    mobileResult = new MobileResult { Data = null, ExceptionResult = true, Message = "Lütfen internet bağlatınızı kontrol ediniz!", Result = false };
                return mobileResult;
            }
        }
        public async Task<MobileResult> PostCensusTagList(IList<Stock> tagList)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpContent content = new StringContent(JsonConvert.SerializeObject(tagList), Encoding.UTF8, "application/json");
                MobileResult mobileResult;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", MainViewModel.UserToken);
                HttpResponseMessage responseMessage = await client.PostAsync(new Uri($"{Url}/CensusTagList"), content);
                if (responseMessage.IsSuccessStatusCode)
                {
                    string contentMessage = await responseMessage.Content.ReadAsStringAsync();
                    mobileResult = JsonConvert.DeserializeObject<MobileResult>(contentMessage);
                    if (mobileResult != null)
                        if (mobileResult.Result == true)
                            mobileResult.Data = JsonConvert.DeserializeObject<int>(mobileResult.Data.ToString());
                }
                else
                    mobileResult = new MobileResult { Data = null, ExceptionResult = true, Message = "Lütfen internet bağlatınızı kontrol ediniz!", Result = false };
                return mobileResult;
            }
        }
        public async Task<MobileResult> PostCensusTicketList(IList<Stock> tagList)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpContent content = new StringContent(JsonConvert.SerializeObject(tagList), Encoding.UTF8, "application/json");
                MobileResult mobileResult;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", MainViewModel.UserToken);
                HttpResponseMessage responseMessage = await client.PostAsync(new Uri($"{Url}/CensusTicketList"), content);
                if (responseMessage.IsSuccessStatusCode)
                {
                    string contentMessage = await responseMessage.Content.ReadAsStringAsync();
                    mobileResult = JsonConvert.DeserializeObject<MobileResult>(contentMessage);
                    if (mobileResult != null)
                        if (mobileResult.Result == true)
                            mobileResult.Data = JsonConvert.DeserializeObject<int>(mobileResult.Data.ToString());
                }
                else
                    mobileResult = new MobileResult { Data = null, ExceptionResult = true, Message = "Lütfen internet bağlatınızı kontrol ediniz!", Result = false };
                return mobileResult;
            }
        }
        public async Task<MobileResult> PostCensus(int ticketAmount)
        {
            using (HttpClient client = new HttpClient())
            {
                MobileResult mobileResult;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", MainViewModel.UserToken);
                HttpResponseMessage responseMessage = await client.GetAsync(new Uri($"{Url}/Census/{ticketAmount}"));
                if (responseMessage.IsSuccessStatusCode)
                {
                    string contentMessage = await responseMessage.Content.ReadAsStringAsync();
                    mobileResult = JsonConvert.DeserializeObject<MobileResult>(contentMessage);
                    if (mobileResult != null)
                        mobileResult.Data = JsonConvert.DeserializeObject<int>(mobileResult.Data.ToString());
                }
                else
                    mobileResult = new MobileResult { Data = null, ExceptionResult = true, Message = "Lütfen internet bağlatınızı kontrol ediniz!", Result = false };
                return mobileResult;
            }
        }
        public async Task<MobileResult> GetCensusReportList(int typee)
        {
            using (HttpClient client = new HttpClient())
            {
                MobileResult mobileResult;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", MainViewModel.UserToken);
                HttpResponseMessage responseMessage = await client.GetAsync(new Uri($"{Url}/CensusReportList/{typee}"));
                string content = await responseMessage.Content.ReadAsStringAsync();
                if (responseMessage.IsSuccessStatusCode)
                {
                    mobileResult = JsonConvert.DeserializeObject<MobileResult>(content);
                    if (mobileResult != null)
                        mobileResult.Data = JsonConvert.DeserializeObject<IList<Stock>>(mobileResult.Data.ToString());
                }
                else
                    mobileResult = new MobileResult { Data = null, ExceptionResult = true, Message = "Bağlantılarınızı kontrol ediniz!", Result = false };
                return mobileResult;
            }
        }

        public async Task<MobileResult> PostWarehouseLabels(IList<Stock> productList)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpContent content = new StringContent(JsonConvert.SerializeObject(productList), Encoding.UTF8, "application/json");
                MobileResult mobileResult;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", MainViewModel.UserToken);
                HttpResponseMessage responseMessage = await client.PostAsync(new Uri($"{Url}/AddWarehouseLabels/"), content);
                if (responseMessage.IsSuccessStatusCode)
                {
                    string contentMessage = await responseMessage.Content.ReadAsStringAsync();
                    mobileResult = JsonConvert.DeserializeObject<MobileResult>(contentMessage);
                    if (mobileResult != null)
                        mobileResult.Data = JsonConvert.DeserializeObject<IList<Stock>>(mobileResult.Data.ToString());
                }
                else
                    mobileResult = new MobileResult { Data = null, ExceptionResult = true, Message = "Lütfen internet bağlatınızı kontrol ediniz!", Result = false };
                return mobileResult;
            }
        }

        public async Task<MobileResult> GetCheckedStoreData(IList<Stock> productList)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpContent content = new StringContent(JsonConvert.SerializeObject(productList), Encoding.UTF8, "application/json");
                MobileResult mobileResult;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", MainViewModel.UserToken);
                HttpResponseMessage responseMessage = await client.GetAsync(new Uri($"{Url}/CheckStoreData"));
                if (responseMessage.IsSuccessStatusCode)
                {
                    string contentMessage = await responseMessage.Content.ReadAsStringAsync();
                    mobileResult = JsonConvert.DeserializeObject<MobileResult>(contentMessage);
                    if (mobileResult != null)
                        mobileResult.Data = JsonConvert.DeserializeObject<IList<Stock>>(mobileResult.Data.ToString());
                }
                else
                    mobileResult = new MobileResult { Data = null, ExceptionResult = true, Message = "Lütfen internet bağlatınızı kontrol ediniz!", Result = false };
                return mobileResult;
            }
        }
        //http://localhost/Terminal/FullStore => Depodaki tüm ürünler
        //http://localhost/Terminal/SimpleStoreData/20220309-0489 => Depodaki tek ürün
        //http://localhost/Terminal/OutsideTag => Depoda yerleşmemiş ürünler
        //http://localhost/Terminal/FullCellInfo Tüm tag listesini alınır.
        //http://localhost/Terminal/FullCell Tüm yerleşen listesini getirir.
        //http://localhost/Terminal/Cell/S1003 Belirli tagtaki ürünleri getirir.
        //http://localhost/Terminal/CellMatching/S1003 Tekli raf eşleştirme
        //http://localhost/Terminal/CellListMatching/S1003 çoklu raf eşleştirme
        //http://localhost/Terminal/CellReplace/S1003 Tag yer değiştirme
        //http://localhost/Terminal/CellExit Tag çıkartmak
        //http://localhost/Terminal/CensusTag/5/10 Tag Sayım Yazdırma
        //http://localhost/Terminal/Census/10 Etiket Sayım Yazdırma
        //http://localhost/Terminal/CensusReportList/0 Sayım Rapor Listesi
        //http://localhost/Terminal/StoreImport Depoya Ekleme
        //http://localhost/Terminal/PostCensusTagList okunan tüm tagların smgRFSayim tablosuna eklenmesi sağlandı.
    }
}
