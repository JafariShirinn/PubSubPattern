using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebClient.Models
{
    public class WeatherForecastRequestModel : IValidatableObject
    {
        /// <summary>
        /// forecast Date.
        /// Should be today date or later
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        ///  The forecast temperature in celsius
        /// </summary>
        public int TemperatureInCelsius { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Date.Date < DateTime.UtcNow.Date)
                yield return new ValidationResult("Forecast date should be today or later.");
        }
    }
}
