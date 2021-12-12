using System;
using Domain.Media;
using Domain.Models;
using Domain.Publisher;

namespace Domain.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IPublisher _publisher;
        public WeatherForecastService(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public bool Broadcast(WeatherForecastModel forecastModel)
        {
            if (forecastModel == null)
                throw new NullReferenceException(nameof(forecastModel));

            try
            {
                var newspaper = new Newspaper();
                var radioStation = new RadioStation();
                var socialMedia = new SocialMedia();

                _publisher.OnChange += newspaper.Display;
                _publisher.OnChange += socialMedia.Display;
                _publisher.OnChange += radioStation.Display;

                _publisher.NotifySubscribers(forecastModel);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public string DisplayForecasts()
        {
            var xmlHelper = new XmlHelper();
            return xmlHelper.ReadForecast();
        }

        public bool DeleteResultFile()
        {
            try
            {
                var xmlHelper = new XmlHelper();
                xmlHelper.DeleteForecastXmlFile();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
