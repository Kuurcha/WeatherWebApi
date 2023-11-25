using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using WebWeatherApi.Entities.ModelConfiguration;

namespace WebWeatherApi.Entities.Model
{
    [Table(nameof(WeatherDetails))]
    [EntityTypeConfiguration(typeof(WeatherDetailsConfiguration))]
    public class WeatherDetails
    {

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Temperature { get; set; }

        public int? Humidty { get; set; }

        public double? DewPoint { get; set; }

        public double? Pressure { get; set; }

        public string? WindDirection { get; set; }

        public int? WindSpeed { get; set; }

        public int? Cloudiness { get; set; }

        public int? CloudBase { get; set; }

        public int? Visibility { get; set; }

        public int? WeatherRecordId { get; set; }

        public WeatherRecord? WeatherRecord { get; set; }

    }
}
