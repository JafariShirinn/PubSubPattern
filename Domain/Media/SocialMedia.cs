using Domain.Models;

namespace Domain.Media
{
    //public class SocialMedia : Subscriber
    //{
    //    public override void Display(object sender, WeatherForecastModel forecastModel)
    //    {
    //        var xmlHelper = new XmlHelper();
    //        xmlHelper.UpdateForecast("SocialMedia", forecastModel);
    //    }
    //}

    public class SocialMedia :  ISocialMedia
    {
        public void Display(object sender, WeatherForecastModel forecastModel)
        {
            var xmlHelper = new XmlHelper();
            xmlHelper.UpdateForecast("SocialMedia", forecastModel);
        }
    }
}
