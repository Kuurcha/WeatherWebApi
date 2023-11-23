using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using WebWeatherApi.Entities.ModelConfiguration;

namespace WebWeatherApi.Entities.Model.Authentication
{
    [Table(nameof(Role))]
    [EntityTypeConfiguration(typeof(RoleConfiguration))]
    public class Role : IdentityRole
    {
    }
}
