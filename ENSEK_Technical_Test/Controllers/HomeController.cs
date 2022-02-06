using ENSEK_Technical_Test.Models;
using EnsekGlobal;
using EnsekGlobal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace ENSEK_Technical_Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> UploadMeterReadings(IFormFile csv)
        {
            var viewModel = new MeterReadingsViewModel();

            try
            {
                if (csv.Length > 0)
                {
                    var json = await CSVHelper.GenerateCSVJson(csv);
                    var client = new HttpClient { BaseAddress = new Uri("https://localhost:7166/") };
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var result = await client.PostAsync("meter-reading-uploads", content);
                    result.EnsureSuccessStatusCode();

                    var meterReadingJson = await result.Content.ReadAsStringAsync();
                    var meterReadingData = JsonSerializer.Deserialize<List<MeterReadingsDM>>(meterReadingJson, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    viewModel.SuccessfulMeterReadings = meterReadingData.Where(x => !x.IsError).ToList();
                    viewModel.FailedMeterReadings = meterReadingData.Where(x => x.IsError).ToList();
                    viewModel.Message = "CSV uploaded successfully.";

                }
                else
                {
                    viewModel.MessageClass = "alert-danger";
                    viewModel.Message = "No CSV file uploaded.";
                }
            }
            catch (Exception ex)
            {
                viewModel.MessageClass = "alert-danger";
                viewModel.Message = ex.Message;
            }

            return PartialView("~/Views/Shared/_MeterReadings.cshtml", viewModel);
        }
    }
}