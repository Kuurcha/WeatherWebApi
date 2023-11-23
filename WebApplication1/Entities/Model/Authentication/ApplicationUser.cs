using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using WebWeatherApi.Entities.ModelConfiguration;

namespace WebWeatherApi.Entities.Model.Authentication
{
    [Table(nameof(ApplicationUser))]
    [EntityTypeConfiguration(typeof(ApplicationUserConfiguration))]
    public class ApplicationUser : Microsoft.AspNetCore.Identity.IdentityUser
    {
        public RefreshToken? refreshToken { get; set; }

    }
}
