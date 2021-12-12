using System;

namespace Domain.Models
{
    public class WeatherForecastModel : EventArgs
    {
        public DateTime Date { get; set; }
        public string Summary { get; set; }

        public WeatherForecastModel()
        {
            
        }
        public WeatherForecastModel(DateTime date, string summary)
        {
            Date = date;
            Summary = summary;
        }
    }
}
