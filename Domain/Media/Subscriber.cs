using Domain.Models;

namespace Domain.Media
{
    public abstract class Subscriber
    {
        public abstract void Display(object sender, WeatherForecastModel forecastModel);
    }
}
