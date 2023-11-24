using System.ComponentModel.DataAnnotations.Schema;

namespace WebWeatherApi.Entities.Model.Authentication
{
    [Table(nameof(RefreshToken))]
    public class RefreshToken
    {
        public string Token { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
