using WebWeatherApi.Entities.Model;
using WebWeatherApi.Entities.ModelConfiguration;

namespace WebWeatherApi.Domain.Services
{
    public class WeatherRecordService
    {

        private readonly ApplicationDbContext _context;

        public WeatherRecordService(ApplicationDbContext context)
        {
            _context = context;
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
