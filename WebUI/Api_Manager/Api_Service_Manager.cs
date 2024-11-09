using Azure;
using Core.CustomExceptions;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Xml.Linq;
using WebUI.Models;

namespace WebUI.Api_Manager
{
    public class Api_Service_Manager
    {
        public readonly HttpClient _client;

        public Api_Service_Manager(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _client = httpClient;
        }

        public async Task<CurrencyApiResponse> GetExchangeRate()
        {

            try
            {
                var response = await _client.GetAsync("http://hasanadiguzel.com.tr/api/kurgetir");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(message: BusinessExceptionMessage.General);
                }

                string responseData = await response.Content.ReadAsStringAsync();

                var currencyRates = JsonConvert.DeserializeObject<CurrencyApiResponse>(responseData);

                return currencyRates;
            }
            catch (Exception ex)
            {
                throw new Exception(message: BusinessExceptionMessage.General);
            }
        }
    }
}
