using WebWeatherApi.Entities.Model;
using WebWeatherApi.Entities.ModelConfiguration;

namespace WebWeatherApi.Domain.Services
{
    public class WeatherRecordService
    {

        private readonly ApplicationDbContext _context;
        private readonly ExcelParsingService _excelParsingService;

        public WeatherRecordService(ApplicationDbContext context, ExcelParsingService excelParsingService)
        {
            _context = context;
            _excelParsingService = excelParsingService;
        }

        public async Task<int> AddExcelRecord(IFormFile file)
        {
            await _excelParsingService.ParseExcelToWeatherDetailsAndWeatherRecord(file);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddWeatherRecord(WeatherRecord weatherRecord)
        {
            if (weatherRecord != null)
            {
                _context.WeatherRecords.Add(weatherRecord);
            }
            return await _context.SaveChangesAsync();
        }
    }
}
