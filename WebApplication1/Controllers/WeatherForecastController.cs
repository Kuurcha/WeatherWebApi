using Microsoft.AspNetCore.Mvc;
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

            ICollection<WeatherDetails> weatherDetails = new List<WeatherDetails>();

            // Check if the request contains multipart/form-data.
            if (!Request.HasFormContentType || Request.Form.Files.Count == 0)
            {
                return BadRequest("No file uploaded");
            }

            try
            {


                await _weatherRecordService.AddExcelRecord(file);
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
