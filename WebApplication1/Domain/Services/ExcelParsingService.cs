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

        public async Task<int> ParseExcelToWeatherDetailsAndWeatherRecordInBatches(IFormFile file)
        {
            int batchSize = 200;
            using (var stream = file.OpenReadStream())
            {
                IWorkbook workbook = new XSSFWorkbook(stream);
                for (int sheetIndex = 0; sheetIndex < workbook.NumberOfSheets; sheetIndex++)
                {
                    List<WeatherRecord> weatherRecordsBatch = new List<WeatherRecord>();
                    ISheet sheet = workbook.GetSheetAt(sheetIndex);
                    for (int i = 4; i <= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row != null)
                        {
                            //form date
                            string? date = getCellValue(row, 0);
                            string? time = getCellValue(row, 1);

                            string filename = file.FileName;

                            if (date == null || time == null)
                                InvalidExcelFormatException.ThrowInvalidDateFormatException(date, time, filename);

                            string? temperature = getCellValue(row, 2);

                            if (temperature == null)
                                throw new InvalidExcelFormatException("File + " + filename + "can't be parsed." + "Temperature for the record should be specified");

                            DateTimeOffset? dateTime = parseDateTime(date!, time!);
                            if (dateTime == null)
                                InvalidExcelFormatException.ThrowInvalidDateFormatException(date, time, filename);


                            string? humidity = getCellValue(row, 3);
                            string? dewPoint = getCellValue(row, 4);
                            string? pressure = getCellValue(row, 5);
                            string? windDirection = getCellValue(row, 6);
                            string? windSpeed = getCellValue(row, 7);
                            string? cloudiness = getCellValue(row, 8);
                            string? cloudBase = getCellValue(row, 9);
                            string? visibility = getCellValue(row, 10);

                            WeatherRecord weatherRecord = WeatherRecord.parseAndCreate(0, dateTime!.Value, temperature, humidity, dewPoint, pressure, windDirection, windSpeed, cloudiness, cloudBase, visibility);

                            string? weatherRecordDetailsDescription = getCellValue(row, 11);

                            if (weatherRecordDetailsDescription != null)
                            {
                                WeatherRecordDetails? weatherRecordDetails = _context!.WeatherRecordDetails!.FirstOrDefault(wr =>
                                   wr.Description == weatherRecordDetailsDescription
                                );
                                if (weatherRecordDetails == null)
                                {
                                    weatherRecordDetails = new WeatherRecordDetails(0, weatherRecordDetailsDescription);
                                    weatherRecordDetails = _context.WeatherRecordDetails.Add(weatherRecordDetails).Entity;
                                }

                                weatherRecord.WeatherRecordDetails = weatherRecordDetails;
                            }
                            weatherRecordsBatch.Add(weatherRecord);
                            if (weatherRecordsBatch.Count % batchSize == 0)
                            {
                                _context.WeatherRecords.AddRange(weatherRecordsBatch);
                                await _context.SaveChangesAsync();
                                weatherRecordsBatch.Clear();
                            }
                        }

                    }
                    if (weatherRecordsBatch.Count > 0)
                    {
                        _context.WeatherRecords.AddRange(weatherRecordsBatch);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            return 1;
        }

    }
}
