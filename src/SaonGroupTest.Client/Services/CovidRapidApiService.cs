using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SaonGroupTest.Client.Models;

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



        public async Task<List<CountryDtoModel>> GetRegionsList()
        {
            var client = GetClient();
            var path = $"/regions";
            var response = await client.GetFromJsonAsync<RegionDtoModel>(path);
            var result = response.data.OrderBy(x => x.name).Select(x => x).ToList();
            var allRegionsItem = new CountryDtoModel
            {
                iso = "ALL",
                name = "**Regions**"
            };

            result.Insert(0, allRegionsItem);
            return result;
        }


        public List<ReportResultDtoModel> FormatReportData(List<ReportDtoModel> baseData, bool areAllRegions)
        {
            var reportRawData = new List<ReportResultDtoModel>();

            if (areAllRegions)
            {
                reportRawData = (from result in baseData
                                 group result by result.region.name into g
                                 select new ReportResultDtoModel
                                 {
                                     location = areAllRegions ? (g.Max(x => x.region.name) ?? "***") : (g.Max(x => x.region.province) ?? g.Max(x => x.region.name)),
                                     deaths = g.Sum(x => x.deaths),
                                     confirmed = g.Sum(x => x.confirmed)
                                 }).OrderByDescending(x => x.confirmed).Take(10).ToList();

            }
            else
            {
                reportRawData = (from result in baseData
                                 let region = result.region
                                 select new ReportResultDtoModel
                                 {
                                     location = areAllRegions ? (region.name ?? "***") : (region.province ?? region.name),
                                     deaths = result.deaths,
                                     confirmed = result.confirmed
                                 }).OrderByDescending(x => x.confirmed).Take(10).ToList();

            }

            return reportRawData;
        }
        public async Task<List<ReportResultDtoModel>> GetReport(string iso)
        {
            var client = GetClient();
            var path = iso == "ALL" ? "reports" : $"reports?iso={iso}";
            var areAllRegions = iso == "ALL";
            var response = await client.GetFromJsonAsync<ReportVm>(path);
            var result = FormatReportData(response.data, areAllRegions);
            return result;
        }

        private HttpClient GetClient()
        {
            var baseUrl = _config["CovidRapiApiService:Url"];
            var client = _factory.CreateClient("covid-rapidapi-service");
            var key = _config["CovidRapiApiService:Key"];
            var host = _config["CovidRapiApiService:Host"];
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Add("x-rapidapi-key", key);
            client.DefaultRequestHeaders.Add("x-rapidapi-host", host);
            return client;
        }
    }
}