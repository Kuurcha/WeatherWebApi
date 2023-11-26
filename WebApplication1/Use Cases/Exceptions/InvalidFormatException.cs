namespace WebWeatherApi.Use_Cases.Exceptions
{
    public class InvalidFormatException : Exception
    {
        public InvalidFormatException()
        {
        }

        public InvalidFormatException(string? message) : base(message)
        {
        }

        public static void ThrowInvalidDateFormatException(string? date, string? time)
        {
            string exceptionMessage = "The date and time should be specified for the record. Specified date: " + (date ?? "") + " specified time: " + (time ?? "");
            throw new InvalidFormatException(exceptionMessage);
        }
    }
}
