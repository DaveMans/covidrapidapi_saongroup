using System.Threading.Tasks;

namespace SaonGroupTest.Client.Services
{
    public interface ICovidRapiApiService
    {
        Task<object> GetRegionsList();
        Task<object> GetProvincesListByRegionId(string iso);
        Task<object> GetCountriesDataByRegion(string name);
        Task<object> GetProvincesDataByCountryId(string iso);
    }
}