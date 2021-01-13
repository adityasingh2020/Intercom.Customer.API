using Intercom.Customer.Contract;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Intercom.Customer.Infrastructure
{
    public class CustomerClient : ICustomerClient
    {
        readonly HttpClient _httpClient;

        public CustomerClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory?.CreateClient();
        }

        public async Task<List<CustomerInfo>> GetCustomerDeatils(string customerRecordUrl)
        {
            var response = await _httpClient.GetAsync(customerRecordUrl);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException("Error while fetching company");


            var stream = await response.Content.ReadAsStreamAsync();
            return GetCustomers(stream);
        }

        private List<CustomerInfo> GetCustomers(Stream stream)
        {
            var customerInfos = new List<CustomerInfo>();
            using (var reader = new StreamReader(stream))
            {
                string line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        customerInfos.Add(JsonConvert.DeserializeObject<CustomerInfo>(line));
                    }
                }
            }
            return customerInfos;
        }
    }
}
