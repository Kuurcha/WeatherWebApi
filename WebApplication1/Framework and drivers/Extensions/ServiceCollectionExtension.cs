using Microsoft.EntityFrameworkCore;
using WebWeatherApi.Domain.Services;
using WebWeatherApi.Entities.ModelConfiguration;

namespace WebWeatherApi.Extensions
{
    public static class ServiceCollectionExtension
    {

        public static void InjectServices(this IServiceCollection services)
        {
            services.AddScoped<ExcelParsingService>(); string statusMessages = "";
            services.AddScoped<WeatherRecordService>();
        }

        public static void AddCorsAny(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.WithOrigins("http://localhost:4200")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });
        }
        public static void ConfigureDBContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
        }
    }
}
