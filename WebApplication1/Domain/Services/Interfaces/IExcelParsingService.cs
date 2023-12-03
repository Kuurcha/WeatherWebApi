namespace WebWeatherApi.Domain.Services.Interfaces
{
    public interface IExcelParsingService
    {
        Task<int> ParseExcelToWeatherDetailsAndWeatherRecordInBatches(IFormFile file);
    }
}
