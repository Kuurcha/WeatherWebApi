using WebWeatherApi.Entities.Model;
using WebWeatherApi.Entities.ModelConfiguration;
using WebWeatherApi.Use_Cases.Exceptions;

namespace WebWeatherApi.Domain.Services
{
    public class WeatherRecordService
    {

        private readonly ApplicationDbContext _context;
        private readonly ExcelParsingService _excelParsingService;
        private readonly ContextService _contextService;
        public WeatherRecordService(ApplicationDbContext context, ExcelParsingService excelParsingService, ContextService contextService)
        {
            _context = context;
            _excelParsingService = excelParsingService;
            _contextService = contextService;
        }

        public async Task<int> AddExcelRecord(IFormFile file)
        {
            try
            {
                await _excelParsingService.ParseExcelToWeatherDetailsAndWeatherRecord(file);
            }
            catch (InvalidExcelFormatException ex)
            {
                _contextService.RejectChanges();
                throw new InvalidExcelFormatException(ex.Message);

            }

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
