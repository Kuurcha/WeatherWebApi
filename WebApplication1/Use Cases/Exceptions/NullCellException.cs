namespace WebWeatherApi.Use_Cases.Exceptions
{
    public class NullCellException : Exception
    {
        public NullCellException()
        {
        }

        public NullCellException(string? message) : base(message)
        {
        }
    }
}
