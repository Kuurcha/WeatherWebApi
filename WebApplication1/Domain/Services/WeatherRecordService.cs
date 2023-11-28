using Microsoft.EntityFrameworkCore;
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

        public List<WeatherRecord> GetAllRecords()
        {
            return _context.WeatherRecords.Include(w => w.WeatherRecordDetails).ToList();
        }

        public async Task<int> AddWeatherRecord(WeatherRecordDetails weatherRecord)
        {
            if (weatherRecord != null)
            {
                _context.WeatherRecordDetails.Add(weatherRecord);
            }
            return await _context.SaveChangesAsync();
        }

        public List<WeatherRecord> GetRecordsByYear(int year)
        {
            return _context.WeatherRecords
                .Include(w => w.WeatherRecordDetails)
                .Where(w => w.Date.Year == year)
                .ToList();
        }
        public async Task<List<WeatherRecord>> GetWeatherDetails(int offset, int limit)
        {
            return await _context.WeatherRecords
                .Include(w => w.WeatherRecordDetails)
                .OrderBy(w => w.Id)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
        }
        public List<WeatherRecord> GetRecordsByMonthWithWeatherRecord(int year, int month)
        {
            return _context.WeatherRecords
                .Include(w => w.WeatherRecordDetails)
                .Where(w => w.Date.Year == year && w.Date.Month == month)
                .ToList();
        }
    }
}
