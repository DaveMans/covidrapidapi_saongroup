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
        public async Task<JsonResult> GetReportByRegion(string iso)
        {
            var data = await _covidRapidApiService.GetReport(iso);
            return Json(data);
        }


        [HttpPost]
        public async Task<JsonResult> GetRegions()
        {
            var regions = await _covidRapidApiService.GetRegionsList();
            return Json(regions);
        }


    }
}
