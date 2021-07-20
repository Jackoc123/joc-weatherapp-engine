using Microsoft.Extensions.Configuration;
using Refit;
using System;
using System.Threading;
using System.Threading.Tasks;
using WeatherApp.Application.Weather.DTOs;

namespace WeatherApp.Api.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IOpenWeatherApiService _openWeatherApiService;
        private readonly IConfiguration _configuration;
        public WeatherService(IOpenWeatherApiService openWeatherApiService, IConfiguration configuration)
        {
            _openWeatherApiService = openWeatherApiService;
            _configuration = configuration;
        }


        public async Task<WeatherDto> GetWeather(string location, CancellationToken cancellationToken)
        {

            try
            {
                var response = await _openWeatherApiService.GetWeatherApi(location, _configuration["OpenWeatherApi:AppId"]);

                return new WeatherDto
                {
                    Location = response.name,
                    CurrentTemperature = response.main.temp,
                    MinTemperature = response.main.temp_min,
                    MaxTemperature = response.main.temp_max,
                    Pressure = response.main.pressure,
                    Humidity = response.main.humidity,
                    Sunrise = response.sys.sunrise,
                    Sunset = response.sys.sunset,
                };

            }
            catch (ApiException ex)
            {

                if (ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    throw new ArgumentException(ex.Content);
                }

                if (ex.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new Exception(ex.Content);
                }

                throw;
            }
        }
    }
}
