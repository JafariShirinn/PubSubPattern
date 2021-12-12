using Domain.Models;

namespace Domain.Media
{
    //public class Newspaper : Subscriber
    //{
    //    public override void Display(object sender, WeatherForecastModel forecastModel)
    //    {
    //        var xmlHelper = new XmlHelper();
    //        xmlHelper.UpdateForecast("Newspaper", forecastModel);
    //    }
    //}

    public class Newspaper : INewspaper
    {
        public void Display(object sender, WeatherForecastModel forecastModel)
        {
            var xmlHelper = new XmlHelper();
            xmlHelper.UpdateForecast("Newspaper", forecastModel);
        }
    }
}
