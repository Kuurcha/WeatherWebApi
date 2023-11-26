namespace WebWeatherApi.Shared.Helper
{
    public static class ParsingHelper
    {
        public static int? ParseNullableInt(string? value)
        {
            return int.TryParse(value, out int result) ? result : (int?)null;
        }

        public static double? ParseNullableDouble(string? value)
        {
            return double.TryParse(value, out double result) ? result : (double?)null;
        }
    }
}
