using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebWeatherApi.Entities.Model;

namespace WebWeatherApi.Entities.ModelConfiguration
{
    public class WeatherConfiguration : IEntityTypeConfiguration<WeatherDetails>
    {
        public void Configure(EntityTypeBuilder<WeatherDetails> builder)
        {
            builder.HasKey(wd => wd.Id);
            builder.Property(wd => wd.Id).ValueGeneratedOnAdd();
        }
    }
}
