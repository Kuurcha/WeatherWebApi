namespace WebWeatherApi.Use_Cases.Exceptions
{
    public class InvalidExcelFormatException : Exception
    {
        public InvalidExcelFormatException()
        {
        }

        public InvalidExcelFormatException(string? message) : base(message)
        {
        }

        public static void ThrowInvalidDateFormatException(string? date, string? time)
        {
            string exceptionMessage = "The date and time should be specified for the record. Specified date: " + (date ?? "") + " specified time: " + (time ?? "");
            throw new InvalidExcelFormatException(exceptionMessage);
        }
    }
}
