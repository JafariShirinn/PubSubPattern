using System;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Domain.Mappers;
using Domain.Media;
using Domain.Models;
using Domain.Publisher;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebClient.Models;

namespace App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ISocialMedia _socialMedia;
        private readonly INewspaper _newspaper;
        private readonly IRadioStation _radioStation;
        private readonly IBroadcasting _broadcasting;
        private readonly IWeatherForecastService _weatherForecastService;
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMapper<WeatherForecastRequestModel, WeatherForecastModel> _mapper;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IBroadcasting broadcasting,
            IMapper<WeatherForecastRequestModel, WeatherForecastModel> mapper,
            IRadioStation radioStation,
            INewspaper newspaper,
            ISocialMedia socialMedia,
            IWeatherForecastService weatherForecastService)
        {
            _logger = logger;
            _broadcasting = broadcasting;
            _mapper = mapper;
            _radioStation = radioStation;
            _newspaper = newspaper;
            _socialMedia = socialMedia;
            _weatherForecastService = weatherForecastService;
        }

        [HttpPost]
        public async Task<IActionResult> Broadcast(WeatherForecastRequestModel forecastRequestModel)
        {
            try
            {
                var weatherForecastModel = _mapper.Map(forecastRequestModel);

                var result = await _weatherForecastService.BroadcastAsync(weatherForecastModel);

                return Ok(result);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Problem(ex.Message);
            }
        }
    }
}
