using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using WebWeatherApi.Entities.ModelConfiguration;
using WebWeatherApi.Shared.Helper;

namespace WebWeatherApi.Entities.Model
{
    [Table(nameof(Model.WeatherRecord))]
    [EntityTypeConfiguration(typeof(WeatherRecordConfiguration))]
    public class WeatherRecord
    {


        public static WeatherRecord parseAndCreate(int id, DateTimeOffset date, string temperature, string? humidty, string? dewPoint, string? pressure, string? windDirection, string? windSpeed, string? cloudiness, string? cloudBase, string? visibility)
        {
            return new WeatherRecord
            {
                Id = id,
                Date = date,
                Temperature = temperature,
                Humidty = ParsingHelper.ParseNullableInt(humidty),
                DewPoint = ParsingHelper.ParseNullableDouble(dewPoint),
                Pressure = ParsingHelper.ParseNullableDouble(pressure),
                WindDirection = windDirection,
                WindSpeed = ParsingHelper.ParseNullableInt(windSpeed),
                Cloudiness = ParsingHelper.ParseNullableInt(cloudiness),
                CloudBase = ParsingHelper.ParseNullableInt(cloudBase),
                Visibility = ParsingHelper.ParseNullableInt(visibility),
            };
        }

        public int Id { get; set; }

        public DateTimeOffset Date { get; set; }

        public string Temperature { get; set; }

        public int? Humidty { get; set; }

        public double? DewPoint { get; set; }

        public double? Pressure { get; set; }

        public string? WindDirection { get; set; }

        public int? WindSpeed { get; set; }

        public int? Cloudiness { get; set; }

        public int? CloudBase { get; set; }

        public int? Visibility { get; set; }

        public int? WeatherRecordDetailsId { get; set; }

        public WeatherRecordDetails? WeatherRecordDetails { get; set; }

    }
}
