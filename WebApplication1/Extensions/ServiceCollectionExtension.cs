using Microsoft.EntityFrameworkCore;
using WebWeatherApi.Domain.Services;
using WebWeatherApi.Entities.ModelConfiguration;

namespace WebWeatherApi.Extensions
{
    public static class ServiceCollectionExtension
    {



        /// <summary>
        /// Injects user created services into the program
        /// </summary>
        /// <param name="services"></param>
        public static void InjectServices(this IServiceCollection services)
        {
            services.AddScoped<ContextService>();
            services.AddScoped<ExcelParsingService>();
            services.AddScoped<WeatherRecordService>();


        }
        public static void ConfigureDBContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
        }
    }
}
