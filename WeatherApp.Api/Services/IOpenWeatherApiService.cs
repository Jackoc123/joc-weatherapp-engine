using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Application.Weather.Models;

namespace WeatherApp.Api.Services
{
    public interface IOpenWeatherApiService
    {
        [Get("/weather")]
        Task<WeatherResponse> GetWeatherApi([Query] string q, [Query] string appid);
    }
}
