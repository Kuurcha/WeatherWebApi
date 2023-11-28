using Microsoft.AspNetCore.Mvc;
using WebWeatherApi.Domain.Services;
namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase

    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherRecordService _weatherRecordService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherRecordService weatherRecordService)
        {
            _weatherRecordService = weatherRecordService;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> GetWeatherDetails(int offset, int limit)
        {
            Response.Headers.Add("Content-Type", "application/json");
            try
            {
                var weatherDetails = await _weatherRecordService.GetWeatherDetails(offset, limit);
                return Ok(weatherDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }


        [HttpPost("upload")]
        public async Task<ActionResult> UploadFile(IFormFileCollection files)
        {
            string statusMessages = "";
            Response.Headers.Add("Content-Type", "application/json");
            // Check if the request contains multipart/form-data.
            if (!Request.HasFormContentType || Request.Form.Files.Count == 0 || files == null || files.Count == 0)
            {
                return BadRequest(new { message = "No file uploaded" });
            }

            try
            {
                foreach (var file in files)
                {
                    try
                    {
                        await _weatherRecordService.AddExcelRecord(file);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(new { message = statusMessages + ex.Message });
                    }

                    statusMessages += "File " + file.FileName + " uploaded and processed successfully\n";
                }


                return Ok(new { message = statusMessages });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = statusMessages + $"Internal server error: {e.Message}" });
            }
        }
    }
}