using System.Threading.Tasks;
using SaonGroupTest.Client.Models;

namespace SaonGroupTest.Client.Services
{
    public interface ICovidRapiApiService
    {
        Task<RegionDtoModel> GetRegionsList();
        Task<object> GetProvincesListByRegionId(string iso);
        Task<object> GetCountriesDataByRegion(string name);
        Task<object> GetProvincesDataByCountryId(string iso);
    }
}