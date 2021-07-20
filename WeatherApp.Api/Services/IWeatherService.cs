using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeatherApp.Application.Weather.DTOs;

namespace WeatherApp.Api.Services
{
    public interface IWeatherService
    {
        Task<WeatherDto> GetWeather(string location, CancellationToken cancellationToken);
    }
}
