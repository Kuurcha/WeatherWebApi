using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebWeatherApi.Domain.Services.Implementation;
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

        /// <summary>
        /// Get all weather record for the offset pagination
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>

        [HttpGet]
        public async Task<ActionResult<List<WeatherRecordDTO>>> GetWeatherDetails(int offset, int limit)
        {
            return await HandleWeatherRecordsAsync(
                async () => await _weatherRecordService.GetWeatherRecordsAsync(offset, limit),
                "Successfully returned records"
            );
        }

        /// <summary>
        /// Get all weather records for keyset pagination
        /// </summary>
        /// <param name="lastId"> id of the last retrieved entity </param>
        /// <param name="limit"> amount of records to be recieved </param>
        /// <returns></returns>

        [HttpGet("biggerThanId")]
        public async Task<ActionResult<List<WeatherRecordDTO>>> GetWeatherDetailsBiggerThanLastId(int lastId, int limit)
        {
            return await HandleWeatherRecordsAsync(
                async () => await _weatherRecordService.GetWeatherRecordsBiggerThanIdAsync(lastId, limit),
                "Successfully returned records"
            );
        }

        /// <summary>
        ///  Get all weather records for keyset pagination in date range
        /// </summary>
        /// <param name="lastId"> id of the last retrieved entity </param>
        /// <param name="limit"> amount of records to be recieved </param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet("inDateRange")]
        public async Task<ActionResult<List<WeatherRecordDTO>>> GetWeatherRecordsInDateRange(int lastId, int limit, DateTime startDate, DateTime endDate)
        {
            return await HandleWeatherRecordsAsync(
                async () => await _weatherRecordService.GetWeatherRecordsBiggerThanIdInDateRangeAsync(lastId, limit, startDate, endDate),
                "Successfully returned records within date range"
            );
        }


        /// <summary>
        /// Get record count from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet("total")]
        public async Task<ActionResult<int>> GetTotalWeatherRecords()
        {
            return await HandleWeatherRecordsAsync(
                async () => await _weatherRecordService.CountAllRecords(),
                "Successfully returned record count"
            );
        }

        /// <summary>
        /// Get record count of record in database within specified date range
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet("totalInDateRange")]
        public async Task<ActionResult<int>> getTotalWeatherRecordsInDateRange(DateTime startDate, DateTime endDate)
        {
            return await HandleWeatherRecordsAsync(
                async () => await _weatherRecordService.CountRecordsInDateRange(startDate, endDate),
                "Successfully returned count for records within date range"
           );
        }

        /// <summary>
        /// Upload excel file to the database 
        /// </summary>
        /// <param name="file">Excel file of specific format</param>
        /// <returns></returns>

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
