using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebWeatherApi.Entities.Model;

namespace WebWeatherApi.Entities.ModelConfiguration
{
    public class WeatherRecordConfiguration : IEntityTypeConfiguration<WeatherRecord>
    {
        public void Configure(EntityTypeBuilder<WeatherRecord> builder)
        {
            builder.HasKey(wd => wd.Id);
            builder.Property(wd => wd.Id).ValueGeneratedOnAdd();

            builder.HasOne(wd => wd.WeatherRecordDetails).
                 WithMany(wr => wr.WeatherRecords)
                 .HasForeignKey(wr => wr.WeatherRecordDetailsId);

            builder.Property(wd => wd.Date).IsRequired();
            builder.Property(wd => wd.Temperature).IsRequired();
        }
    }
}
