using System;
using Domain.Media;
using Domain.Models;
using Domain.Publisher;

namespace Domain.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IPublisher _publisher;
        private readonly INewspaper _newspaper;
        private readonly IRadioStation _radioStation;
        private readonly ISocialMedia _socialMedia;

        //public WeatherForecastService(IPublisher publisher)
        //{
        //    _publisher = publisher;
        //}

        public WeatherForecastService(IPublisher publisher,
            INewspaper newspaper,
            IRadioStation radioStation,
            ISocialMedia socialMedia)
        {
            _publisher = publisher;
            _newspaper = newspaper;
            _radioStation = radioStation;
            _socialMedia = socialMedia;
        }

        public bool Broadcast(WeatherForecastModel forecastModel)
        {
            if (forecastModel == null)
                throw new NullReferenceException(nameof(forecastModel));

            try
            {
                //var newspaper = new Newspaper();
                //var radioStation = new RadioStation();
                //var socialMedia = new SocialMedia();

                _publisher.OnChange += _newspaper.Display;
                _publisher.OnChange += _socialMedia.Display;
                _publisher.OnChange += _radioStation.Display;

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
