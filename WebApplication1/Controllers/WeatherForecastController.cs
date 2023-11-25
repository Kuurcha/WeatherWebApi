using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using WebWeatherApi.Domain.Services;
using WebWeatherApi.Entities.Model;
namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase

    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherRecordService _weatherRecordService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherRecordService weatherRecordService)
        {
            _weatherRecordService = weatherRecordService;
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost(Name = "PostWeatherRecord")]
        public async Task<ActionResult> AddWeatherRecord(WeatherRecord record)
        {
            await _weatherRecordService.AddWeatherRecord(record);
            return Accepted();
        }


        [HttpPost("upload")]
        public async Task<ActionResult> UploadFile(IFormFile file)
        {
            // Check if the request contains multipart/form-data.
            if (!Request.HasFormContentType || Request.Form.Files.Count == 0)
            {
                return BadRequest("No file uploaded");
            }

            try
            {

                using (var stream = file.OpenReadStream())
                {
                    IWorkbook workbook = await Task.Run(() => new XSSFWorkbook(stream));
                    ISheet sheet = workbook.GetSheetAt(0);


                    for (int i = 0; i <= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row != null)
                        {

                            for (int j = 0; j < row.LastCellNum; j++)
                            {
                                ICell cell = row.GetCell(j);
                                if (cell != null)
                                {

                                    string cellValue = cell.ToString();

                                }
                            }
                        }
                    }
                }

                // You can add more processing logic here based on the Excel data.

                return Ok("File uploaded and processed successfully");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }
    }
}
