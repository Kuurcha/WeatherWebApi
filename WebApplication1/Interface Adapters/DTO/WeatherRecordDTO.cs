namespace WebWeatherApi.Interface_Adapters.DTO
{
    public class WeatherRecordDTO
    {
        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Temperature { get; set; }
        public int? Humidity { get; set; }
        public double? DewPoint { get; set; }
        public double? Pressure { get; set; }
        public string WindDirection { get; set; }
        public int? WindSpeed { get; set; }
        public int? Cloudiness { get; set; }
        public int? CloudBase { get; set; }
        public int? Visibility { get; set; }

        public WeatherRecordDetailsDTO WeatherRecordDetails { get; set; }
    }
}
