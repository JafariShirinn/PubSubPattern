using Domain.Models;

namespace Domain.Media
{
    public class RadioStation: Subscriber
    {
        public override void Display(object sender, WeatherForecastModel forecastModel)
        {
            var xmlHelper = new XmlHelper();
            xmlHelper.UpdateForecast("RadioStation", forecastModel);
        }
    }
}
