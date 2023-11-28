using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using WebWeatherApi.Domain.ModelConfiguration;

namespace WebWeatherApi.Entities.Model
{
    [Table(nameof(WeatherRecordDetails))]
    [EntityTypeConfiguration(typeof(WeatherRecordDetailsConfiguration))]
    public class WeatherRecordDetails
    {
        public WeatherRecordDetails(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public int Id { get; set; }

        public string Description { get; set; }

        public ICollection<WeatherRecord> WeatherRecords { get; set; }
    }
}
