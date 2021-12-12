using Domain.Models;
using Domain.Publisher;
using Domain.Services;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;
using System;
using Domain.Media;
using FluentAssertions;

namespace DomainTest.Services
{
    [TestFixture]
    public class WeatherForecastServiceTests
    {
        private Mock<IPublisher> _publisherMock;
        private IWeatherForecastService _service;

        [SetUp]
        public void Setup()
        {
            _publisherMock = new Mock<IPublisher>();
        }

        [Test]
        public void BroadcastAsync_modelIsNull_ShouldThrowNullReferenceException()
        {
            WeatherForecastModel weatherForecastModel = null;

            Func<bool> action =  () => _service.Broadcast(weatherForecastModel);

            action.Should().Throw<NullReferenceException>();
        }

        [Test]
        public void BroadcastAsync_OK_ShouldNotifyAllMedia()
        {
            var weatherForecastModel = Builder<WeatherForecastModel>
                .CreateNew()
                .With(model => model.Date, DateTime.UtcNow)
                .With(model => model.Summary, "Today the temperature is  32C")
                .Build();

            _publisherMock.Setup(p => p.NotifySubscribers(weatherForecastModel));

            //var publisher = new Publisher();

            //var newspaper = new Newspaper();
            //var radioStation = new RadioStation();

            //publisher.NotifySubscribers(weatherForecastModel);

            _service.Broadcast(weatherForecastModel);

            _publisherMock.Verify(p=>p.NotifySubscribers(weatherForecastModel), Times.Once);
        }
    }
}