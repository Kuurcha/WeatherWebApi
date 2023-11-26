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
        private readonly ContextService _contextService;
        private readonly ApplicationDbContext _context;

        public ExcelParsingService(ContextService contextService, ApplicationDbContext context)
        {
            _contextService = contextService;
            _context = context;
        }

        public string? getCellValue(IRow row, int i)
        {
            return row.GetCell(i, MissingCellPolicy.RETURN_BLANK_AS_NULL)?.ToString();
        }

        public DateTime? parseDateTime(string date, string time)
        {
            string dateTimeString = $"{date} {time}";


            if (DateTime.TryParseExact(dateTimeString, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
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
                            InvalidFormatException.ThrowInvalidDateFormatException(date, time);

                        string? temperature = getCellValue(row, 2);

                        if (temperature == null)
                            throw new InvalidFormatException("Temperature for the record should be specified");

                        DateTime? dateTime = parseDateTime(date!, time!);
                        if (dateTime == null)
                            InvalidFormatException.ThrowInvalidDateFormatException(date, time);


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

                        if (weatherRecordDetails != null)
                        {
                            WeatherRecord weatherRecord = new WeatherRecord(0, weatherRecordDetails);
                        }

                    }
                }
            }
            return 1;
        }

    }
}
