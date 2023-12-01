using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebWeatherApi.Domain.Services;
using WebWeatherApi.Interface_Adapters.DTO;
namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase

    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherRecordService _weatherRecordService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherRecordService weatherRecordService, IMapper mapper)
        {
            _weatherRecordService = weatherRecordService;
            _logger = logger;

        }

        private async Task<ActionResult<T>> HandleWeatherRecordsAsync<T>(Func<Task<T>> getRecordsFunc, string successMessage)
        {
            Response.Headers.Add("Content-Type", "application/json");

            try
            {
                var records = await getRecordsFunc.Invoke();
                return Ok(new { message = successMessage, content = records });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<WeatherRecordDTO>>> GetWeatherDetails(int offset, int limit)
        {
            return await HandleWeatherRecordsAsync(
                async () => await _weatherRecordService.GetWeatherRecordsAsync(offset, limit),
                "Successfully returned records"
            );
        }

        [HttpGet("biggerThanId")]
        public async Task<ActionResult<List<WeatherRecordDTO>>> GetWeatherDetailsBiggerThanLastId(int lastId, int limit)
        {
            return await HandleWeatherRecordsAsync(
                async () => await _weatherRecordService.GetWeatherRecordsBiggerThanIdAsync(lastId, limit),
                "Successfully returned records"
            );
        }

        [HttpGet("inDateRange")]
        public async Task<ActionResult<List<WeatherRecordDTO>>> GetWeatherRecordsInDateRange(int lastId, int limit, DateTime startDate, DateTime endDate)
        {
            return await HandleWeatherRecordsAsync(
                async () => await _weatherRecordService.GetWeatherRecordsBiggerThanIdInDateRangeAsync(lastId, limit, startDate, endDate),
                "Successfully returned records within date range"
            );
        }

        [HttpGet("total")]
        public async Task<ActionResult<int>> GetTotalWeatherRecords()
        {
            return await HandleWeatherRecordsAsync(
                async () => await _weatherRecordService.CountAllRecords(),
                "Successfully returned record count"
            );
        }

        [HttpGet("totalInDateRange")]
        public async Task<ActionResult<int>> getTotalWeatherRecordsInDateRange(DateTime startDate, DateTime endDate)
        {
            return await HandleWeatherRecordsAsync(
                async () => await _weatherRecordService.CountRecordsInDateRange(startDate, endDate),
                "Successfully returned count for records within date range"
           );
        }

        [HttpPost("upload/batch")]
        public async Task<ActionResult> UploadFileBatch(IFormFile file)
        {
            string statusMessages = "";
            Response.Headers.Add("Content-Type", "application/json");

            if (!Request.HasFormContentType || Request.Form.Files.Count == 0 || file == null)
            {
                return BadRequest(new { message = "No file uploaded" });
            }
            try
            {
                try
                {
                    await _weatherRecordService.AddExcelRecordBatch(file);
                }
                catch (ICSharpCode.SharpZipLib.Zip.ZipException zipException)
                {
                    return BadRequest(new { message = "File " + file.FileName + " has unsupported file format or is corrupted." });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = statusMessages + ex.Message });
                }
                return Ok(new { message = "File " + file.FileName + " uploaded and processed successfully\n" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Internal server error: {e.Message}" });
            }
        }
    }
}
