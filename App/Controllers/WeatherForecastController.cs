using System;
using Domain.Mappers;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using WebClient.Models;

namespace App.Controllers
{
    /// <summary>
    /// this controller contains all endpoint related to weather forecasting
    /// post: Broadcast the weather forecast
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _weatherForecastService;
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMapper<WeatherForecastRequestModel, WeatherForecastModel> _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">used for all logs</param>
        /// <param name="mapper">used for mapping the request model to domain model</param>
        /// <param name="weatherForecastService">interface of domain service </param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IMapper<WeatherForecastRequestModel, WeatherForecastModel> mapper,
            IWeatherForecastService weatherForecastService)
        {
            _logger = logger;
            _mapper = mapper;
            //_radioStation = radioStation;
            //_newspaper = newspaper;
            //_socialMedia = socialMedia;
            _weatherForecastService = weatherForecastService;
        }

        /// <summary>
        /// this method broadcasts the weather forecast to all subscriber media
        /// </summary>
        /// <param name="forecastRequestModel">model is contains the forecast Date and the temperature in celsius</param>
        /// <returns> a xml string contains all media forecasting news</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Broadcast the weather forecast to all subscriber media")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public IActionResult Broadcast(WeatherForecastRequestModel forecastRequestModel)
        {
            try
            {
                var weatherForecastModel = _mapper.Map(forecastRequestModel);

                var broadcastResult = _weatherForecastService.Broadcast(weatherForecastModel);

                if (!broadcastResult)
                    return Problem("broadcasting failed");

                var result = _weatherForecastService.DisplayForecasts();

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


        [HttpDelete]
        [SwaggerOperation(Summary = "Clear all the weather forecast for all subscriber media")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public IActionResult ClearForecastHistory()
        {
            try
            {
                var result = _weatherForecastService.DeleteResultFile();

                return result ? Ok() : Problem();
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
