using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using WeatherApp.Api.Services;

namespace WeatherApp.Api.Controllers
{
    [Route("api/v1/weather")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWeather([FromQuery] string location, CancellationToken cancellationToken)
        {
            if (String.IsNullOrEmpty(location))
            {
                return BadRequest();
            }

            var result = await _weatherService.GetWeather(location, cancellationToken);

            return Ok(result);
        }
    }
}
