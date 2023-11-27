using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebWeatherApi.Entities.Model;

namespace WebWeatherApi.Entities.ModelConfiguration
{
    public class WeatherDetailsConfiguration : IEntityTypeConfiguration<WeatherDetails>
    {
        public void Configure(EntityTypeBuilder<WeatherDetails> builder)
        {
            builder.HasKey(wd => wd.Id);
            builder.Property(wd => wd.Id).ValueGeneratedOnAdd();

            builder.HasOne(wd => wd.WeatherRecord).
                 WithMany(wr => wr.WeatherDetails)
                 .HasForeignKey(wr => wr.WeatherRecordId);

            builder.Property(wd => wd.Date).IsRequired();
            builder.Property(wd => wd.Temperature).IsRequired();
        }
    }
}
