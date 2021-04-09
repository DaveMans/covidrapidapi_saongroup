using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SaonGroupTest.Client.Services
{
    public class CovidRapiApiService : ICovidRapiApiService
    {
        private readonly IHttpClientFactory _factory;
        private readonly IConfiguration _config;

        public CovidRapiApiService(
            IHttpClientFactory factory,
            IConfiguration config)
        {
            _factory = factory;
            _config = config;
        }

        public async Task<object> GetRegionsList()
        {
            var client = GetClient();
            var path = $"/regions";
            var response = await client.GetFromJsonAsync<object>(path);
            return response;
        }
        public async Task<object> GetProvincesListByRegionId(string iso)
        {
            var client = GetClient();
            var path = $"/provinces?iso={iso}";
            var response = await client.GetFromJsonAsync<object>(path);
            return response;
        }
        public async Task<object> GetCountriesDataByRegion(string name)
        {
            var client = GetClient();
            var path = $"reports?region_name={name}";
            var response = await client.GetFromJsonAsync<object>(path);
            return response;
        }
        public async Task<object> GetProvincesDataByCountryId(string iso)
        {
            var client = GetClient();
            var path = $"/reports?iso={iso}";
            var response = await client.GetFromJsonAsync<object>(path);
            return response;
        }


        private HttpClient GetClient()
        {
            var baseUrl = _config["CovidRapiApiService:Url"];
            var client = _factory.CreateClient("covid-rapidapi-service");
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Add("x-rapidapi-key", _config["CovidRapiApiService:Key"]);
            client.DefaultRequestHeaders.Add("x-rapidapi-host", _config["CovidRapiApiService:Host"]);
            return client;
        }
    }
}