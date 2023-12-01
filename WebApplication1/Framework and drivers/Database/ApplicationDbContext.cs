using Microsoft.EntityFrameworkCore;
using WebWeatherApi.Entities.Model;


namespace WebWeatherApi.Entities.ModelConfiguration
{
    public class ApplicationDbContext : DbContext
    {


        public DbSet<WeatherRecord> WeatherRecords { get; set; }

        public DbSet<WeatherRecordDetails> WeatherRecordDetails { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
