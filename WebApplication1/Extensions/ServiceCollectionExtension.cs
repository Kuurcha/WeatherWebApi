using Microsoft.EntityFrameworkCore;
using WebWeatherApi.Entities.ModelConfiguration;

namespace WebWeatherApi.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void configureDBContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
        }
    }
}
