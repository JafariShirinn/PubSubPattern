using System;
using Domain.Models;

namespace Domain.Publisher
{
    public interface IPublisher
    {
        public event EventHandler<WeatherForecastModel> OnChange;
        void NotifySubscribers(WeatherForecastModel eventArgs);
    }
}
