using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using WebWeatherApi.Entities.ModelConfiguration;

namespace WebWeatherApi.Entities.Model
{
    [Table(nameof(WeatherDetails))]
    [EntityTypeConfiguration(typeof(WeatherConfiguration))]
    public class WeatherDetails
    {
        public int Id { get; set; }

        public DateTime date { get; set; }

    }
}
