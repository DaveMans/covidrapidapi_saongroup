using System.Collections.Generic;
using System.Threading.Tasks;
using SaonGroupTest.Client.Models;

namespace SaonGroupTest.Client.Services
{
    public interface ICovidRapiApiService
    {
        Task<List<CountryDtoModel>> GetRegionsList();
        Task<List<ReportResultDtoModel>> GetReport(string iso);
    }
}