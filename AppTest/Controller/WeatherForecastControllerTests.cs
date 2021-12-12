using System;
using App.Controllers;
using Domain.Mappers;
using Domain.Models;
using Domain.Services;
using Moq;
using NUnit.Framework;
using WebClient.Models;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using FluentAssertions;

namespace AppTest.Controller
{
    [TestFixture]
    public class WeatherForecastControllerTests
    {
        private readonly Mock<IMapper<WeatherForecastRequestModel, WeatherForecastModel>> _mapperMock =
            new();
        private readonly Mock<IWeatherForecastService> _weatherForecastServiceMock = new();
        private readonly Mock<ILogger<WeatherForecastController>> _loggerMock = new();

        private WeatherForecastController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new WeatherForecastController(_loggerMock.Object,
                _mapperMock.Object,
                _weatherForecastServiceMock.Object);
        }

        [Test]
        public void Broadcast_EverythingIsOk_ShouldReturnOK1()
        {
            Assert.Pass();
        }

        [Test]
        public void Broadcast_ServiceReturnsResult_ShouldReturnOK()
        {
            var weatherForecastModel = Builder<WeatherForecastModel>
                .CreateNew()
                .With(model=>model.Date, DateTime.UtcNow)
                .With(model=>model.Summary, "Today the temperature is  32C")
                .Build();

            _mapperMock.Setup(mapper => mapper.Map(It.IsAny<WeatherForecastRequestModel>()))
                .Returns(weatherForecastModel);

            _weatherForecastServiceMock.Setup(x =>
                    x.Broadcast(weatherForecastModel))
                .Returns(It.IsAny<bool>());

            var result = _controller.Broadcast(It.IsAny<WeatherForecastRequestModel>()) as ObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Test]
        public void Broadcast_MapperThrowArgumentNullException_ShouldReturnBadRequest()
        {
            _mapperMock.Setup(x => x.Map(It.IsAny<WeatherForecastRequestModel>()))
                .Throws(new ArgumentNullException());

            var result = _controller.Broadcast(new WeatherForecastRequestModel()) as IStatusCodeActionResult;

            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Test]
        public void Broadcast_ServiceThrowArgumentNullException_ShouldReturnBadRequest()
        {
            _weatherForecastServiceMock.Setup(x => x.Broadcast(It.IsAny<WeatherForecastModel>()))
                .Throws(new ArgumentNullException());

            var result = _controller.Broadcast(new WeatherForecastRequestModel()) as IStatusCodeActionResult;

            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Test]
        public void Broadcast_ServiceThrowException_ShouldReturnProblem()
        {
            _weatherForecastServiceMock.Setup(x => x.Broadcast(It.IsAny<WeatherForecastModel>()))
                .Throws(new Exception());

            var result = _controller.Broadcast(new WeatherForecastRequestModel()) as IStatusCodeActionResult;

            result.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
    }
}
