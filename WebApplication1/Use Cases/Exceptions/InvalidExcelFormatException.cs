namespace WebWeatherApi.Use_Cases.Exceptions
{
    public class InvalidExcelFormatException : Exception
    {

        string exceptionMessage;
        public InvalidExcelFormatException()
        {
        }


        public InvalidExcelFormatException(string? message) : base(message)
        {

        }


        public static void ThrowInvalidDateFormatException(string? date, string? time, string filename)
        {
            string exceptionMessage = "File + " + filename + " can't be parsed. " + " The date and time should be specified for the record. Specified date: " + (date ?? "") + " specified time: " + (time ?? "");
            throw new InvalidExcelFormatException(exceptionMessage);
        }
    }
}
