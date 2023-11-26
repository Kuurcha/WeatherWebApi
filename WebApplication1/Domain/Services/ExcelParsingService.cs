using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Globalization;
using WebWeatherApi.Entities.Model;
using WebWeatherApi.Entities.ModelConfiguration;
using WebWeatherApi.Use_Cases.Exceptions;

namespace WebWeatherApi.Domain.Services
{
    public class ExcelParsingService
    {
        private readonly int amountOfRows = 11;

        private readonly ApplicationDbContext _context;

        public ExcelParsingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public string? getCellValue(IRow row, int i)
        {
            return row.GetCell(i, MissingCellPolicy.RETURN_BLANK_AS_NULL)?.ToString();
        }

        public DateTimeOffset? parseDateTime(string date, string time)
        {
            string dateTimeString = $"{date} {time}";
            if (DateTimeOffset.TryParseExact(dateTimeString, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTimeOffset result))
                return result;
            return null;
        }
        public async Task<int> ParseExcelToWeatherDetailsAndWeatherRecord(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                IWorkbook workbook = await Task.Run(() => new XSSFWorkbook(stream));
                ISheet sheet = workbook.GetSheetAt(0);


                for (int i = 4; i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row != null)
                    {
                        //form date
                        string? date = getCellValue(row, 0);
                        string? time = getCellValue(row, 1);

                        if (date == null || time == null)
                            InvalidExcelFormatException.ThrowInvalidDateFormatException(date, time);

                        string? temperature = getCellValue(row, 2);

                        if (temperature == null)
                            throw new InvalidExcelFormatException("Temperature for the record should be specified");

                        DateTimeOffset? dateTime = parseDateTime(date!, time!);
                        if (dateTime == null)
                            InvalidExcelFormatException.ThrowInvalidDateFormatException(date, time);


                        string? humidity = getCellValue(row, 3);
                        string? dewPoint = getCellValue(row, 4);
                        string? pressure = getCellValue(row, 5);
                        string? windDirection = getCellValue(row, 6);
                        string? windSpeed = getCellValue(row, 7);
                        string? cloudiness = getCellValue(row, 8);
                        string? cloudBase = getCellValue(row, 9);
                        string? visibility = getCellValue(row, 10);

                        WeatherDetails weatherDetails = WeatherDetails.parseAndCreate(0, dateTime!.Value, temperature, humidity, dewPoint, pressure, windDirection, windSpeed, cloudiness, cloudBase, visibility);

                        string? weatherRecordDetails = getCellValue(row, 11);
                        WeatherRecord? weatherRecord = null;
                        if (weatherRecordDetails != null)
                        {
                            weatherRecord = _context!.WeatherRecords!.FirstOrDefault(wr => wr.Description == weatherRecordDetails);
                            if (weatherRecord == null)
                                weatherRecord = new WeatherRecord(0, weatherRecordDetails);

                            var weatherRecordEntity = _context.WeatherRecords.Add(weatherRecord);

                            weatherDetails.WeatherRecord = weatherRecordEntity.Entity;
                            weatherDetails.WeatherRecordId = weatherRecordEntity.Entity.Id;

                        }

                        _context.Add(weatherDetails);
                        _context.SaveChanges();

                    }
                }
            }
            return 1;
        }

    }
}
