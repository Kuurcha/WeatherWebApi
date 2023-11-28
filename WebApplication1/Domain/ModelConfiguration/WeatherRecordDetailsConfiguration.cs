using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebWeatherApi.Entities.Model;

namespace WebWeatherApi.Domain.ModelConfiguration
{
    public class WeatherRecordDetailsConfiguration : IEntityTypeConfiguration<WeatherRecordDetails>
    {
        public void Configure(EntityTypeBuilder<WeatherRecordDetails> builder)
        {
            builder.HasKey(wr => wr.Id);
            builder.Property(wr => wr.Id).ValueGeneratedOnAdd();


        }
    }
}
