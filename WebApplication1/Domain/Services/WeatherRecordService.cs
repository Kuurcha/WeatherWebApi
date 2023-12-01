using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebWeatherApi.Entities.Model;
using WebWeatherApi.Entities.ModelConfiguration;
using WebWeatherApi.Interface_Adapters.DTO;

namespace WebWeatherApi.Domain.Services
{
    public class WeatherRecordService
    {

        private readonly ApplicationDbContext _context;
        private readonly ExcelParsingService _excelParsingService;
        private readonly IMapper _mapper;

        public WeatherRecordService(ApplicationDbContext context, ExcelParsingService excelParsingService, IMapper mapper)
        {
            _context = context;
            _excelParsingService = excelParsingService;
            _mapper = mapper;
        }

        public async Task<int> AddExcelRecord(IFormFile file)
        {

            await _excelParsingService.ParseExcelToWeatherDetailsAndWeatherRecord(file);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddExcelRecordBatch(IFormFile file)
        {

            await _excelParsingService.ParseExcelToWeatherDetailsAndWeatherRecordInBatches(file);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<WeatherRecordDTO>> GetAllRecordsAsync()
        {
            var records = await _context.WeatherRecords.Include(w => w.WeatherRecordDetails).ToListAsync();
            return _mapper.Map<List<WeatherRecordDTO>>(records);
        }

        public async Task<int> CountAllRecords()
        {
            return await _context.WeatherRecords.CountAsync();
        }

        public async Task<int> CountRecordsInDateRange(DateTime startDate, DateTime endDate)
        {
            return await _context.WeatherRecords
                .Where(w => w.Date >= startDate && w.Date <= endDate)
                .CountAsync();
        }

        public async Task<List<WeatherRecordDTO>> GetWeatherRecordsAsync(int offset, int limit)
        {
            var records = await _context.WeatherRecords
                .Include(w => w.WeatherRecordDetails)
                .OrderBy(w => w.Id)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
            return _mapper.Map<List<WeatherRecordDTO>>(records);
        }

        public async Task<List<WeatherRecordDTO>> GetWeatherRecordsBiggerThanIdAsync(int lastId, int limit)
        {
            var records = await _context.WeatherRecords
                .Include(w => w.WeatherRecordDetails)
                .Where(w => w.Id > lastId)
                .OrderBy(w => w.Id)
                .Take(limit)
                .ToListAsync();

            return _mapper.Map<List<WeatherRecordDTO>>(records);
        }

        public async Task<List<WeatherRecordDTO>> GetWeatherRecordsBiggerThanIdInDateRangeAsync(int lastId, int limit, DateTime startDate, DateTime endDate)
        {
            var records = await _context.WeatherRecords
                .Include(w => w.WeatherRecordDetails)
                .Where(w => w.Id > lastId && w.Date >= startDate && w.Date <= endDate)
                .OrderBy(w => w.Id)
                .Take(limit)
                .ToListAsync();

            return _mapper.Map<List<WeatherRecordDTO>>(records);
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

        public List<WeatherRecord> GetRecordsByMonthWithWeatherRecord(int year, int month)
        {
            return _context.WeatherRecords
                .Include(w => w.WeatherRecordDetails)
                .Where(w => w.Date.Year == year && w.Date.Month == month)
                .ToList();
        }
    }
}
