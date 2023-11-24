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

        public string date { get; set; }

    }
}
