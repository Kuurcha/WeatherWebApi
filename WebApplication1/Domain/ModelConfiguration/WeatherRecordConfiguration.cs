using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebWeatherApi.Entities.Model;

namespace WebWeatherApi.Domain.ModelConfiguration
{
    public class WeatherRecordConfiguration : IEntityTypeConfiguration<WeatherRecord>
    {
        public void Configure(EntityTypeBuilder<WeatherRecord> builder)
        {
            builder.HasKey(wr => wr.Id);
            builder.Property(wr => wr.Id).ValueGeneratedOnAdd();


        }
    }
}
