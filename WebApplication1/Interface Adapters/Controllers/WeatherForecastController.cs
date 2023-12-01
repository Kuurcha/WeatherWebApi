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


        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherRecordDTO>>> GetWeatherDetails(int offset, int limit)
        {
            Response.Headers.Add("Content-Type", "application/json");
            try
            {
                var records = await _weatherRecordService.GetWeatherRecordsAsync(offset, limit);
                return Ok(new { message = "Sucessfully returned " + records, content = records });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpGet("biggerThanId")]
        public async Task<ActionResult<IEnumerable<WeatherRecordDTO>>> GetWeatherDetailsBiggerThanLastId(int lastId, int limit)
        {
            Response.Headers.Add("Content-Type", "application/json");
            try
            {
                var records = await _weatherRecordService.GetWeatherRecordsBiggerThanIdAsync(lastId, limit);
                return Ok(new { message = "Successfully returned records", content = records });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }
        [HttpGet("inDateRange")]
        public async Task<ActionResult<IEnumerable<WeatherRecordDTO>>> GetWeatherRecordsInDateRange(int lastId, int limit, DateTime startDate, DateTime endDate)
        {
            Response.Headers.Add("Content-Type", "application/json");
            try
            {
                var records = await _weatherRecordService.GetWeatherRecordsBiggerThanIdInDateRangeAsync(lastId, limit, startDate, endDate);
                return Ok(new { message = "Successfully returned records within date range", content = records });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpGet("total")]
        public async Task<ActionResult<int>> GetTotalWeatherRecords()
        {
            try
            {
                int totalRecords = await _weatherRecordService.CountAllRecords();
                return Ok(new { message = "Sucessfully returned " + totalRecords, content = totalRecords });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }

        }

        [HttpGet("totalInDateRange")]
        public async Task<ActionResult<int>> getTotalWeatherRecordsInDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                int count = await _weatherRecordService.CountRecordsInDateRange(startDate, endDate);
                return Ok(new { message = $"Successfully returned count for records within date range", content = count });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
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
        [HttpPost("upload")]
        public async Task<ActionResult> UploadFile(IFormFile file)
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
                    await _weatherRecordService.AddExcelRecord(file);
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
