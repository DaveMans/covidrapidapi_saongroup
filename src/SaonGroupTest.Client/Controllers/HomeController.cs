using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SaonGroupTest.Client.Services;

namespace SaonGroupTest.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICovidRapiApiService _covidRapidApiService;

        public HomeController(ICovidRapiApiService covidRapiApiService)
        {
            _covidRapidApiService = covidRapiApiService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetProvincesDataByCountryId(string iso)
        {
            var data = await _covidRapidApiService.GetProvincesDataByCountryId(iso);
            return Json(data);
        }

        [HttpPost]
        public async Task<JsonResult> GetCountriesDataByRegion(string name)
        {
            var data = await _covidRapidApiService.GetCountriesDataByRegion(name);
            return Json(data);
        }

        [HttpPost]
        public async Task<JsonResult> GetRegions()
        {
            var regions = await _covidRapidApiService.GetRegionsList();
            return Json(regions);
        }

        [HttpPost]
        public async Task<JsonResult> GetProvincesListByRegionId(string iso)
        {
            var regions = await _covidRapidApiService.GetProvincesListByRegionId(iso);
            return Json(regions);
        }
    }
}
