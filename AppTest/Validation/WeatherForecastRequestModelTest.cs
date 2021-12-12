using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using WebClient.Models;
using FluentAssertions;



namespace AppTest.Validation
{
    [TestFixture]
    public class WeatherForecastRequestModelTest
    {
        [Test]
        public void Validate_DateIsOnPast_ShouldBeInvalid()
        {
            var model = Builder<WeatherForecastRequestModel>
                .CreateNew()
                .With(x => x.Date, DateTime.UtcNow.AddDays(-2))
                .Build();

            var validationContext = new ValidationContext(model);

            var results = model.Validate(validationContext);

            results.Count().Should().Be(1);
        }
    }
}
