using System;
using System.Net.Http;
using System.Threading.Tasks;
using App;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using WebClient.Models;

namespace IntegrationTests
{
    [TestFixture]
    [Explicit]
    public class BroadcastingProcessTests
    {
        private WebApplicationFactory<Startup> _appFactory;
        private IServiceScope _serviceScope;
        private HttpClient _httpClient;
        private ForecastClient _forecastClient;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _appFactory = new WebApplicationFactory<Startup>();
            _serviceScope = _appFactory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            _httpClient = _appFactory.CreateClient();
            _forecastClient = new ForecastClient(_httpClient);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _appFactory.Dispose();
            _serviceScope.Dispose();
            _httpClient.Dispose();
        }

        [Test]
        public async  Task Broadcast_ValidModel_ShouldReturnExpectedResult()
        {
            var model = new WeatherForecastRequestModel
            {
                Date = DateTime.Now,
                TemperatureInCelsius = 20
            };

            var result =  await _forecastClient.Broadcast(model) ;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}