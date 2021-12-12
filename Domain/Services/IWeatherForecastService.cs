using Domain.Models;

namespace Domain.Services
{
    public interface IWeatherForecastService
    {
        bool Broadcast(WeatherForecastModel forecastModel);
        string DisplayForecasts();
        bool DeleteResultFile();
    }
}
