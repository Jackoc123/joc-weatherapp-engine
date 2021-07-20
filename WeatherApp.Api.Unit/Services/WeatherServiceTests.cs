using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Refit;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WeatherApp.Api.Services;
using WeatherApp.Application.Weather.DTOs;
using WeatherApp.Application.Weather.Models;

namespace WeatherApp.Api.UnitTests.Services
{
    [TestFixture]
    public class WeatherServiceTests
    {
        [Test]
        public async Task GetWeather_CorrectUsage_WeatherDtoReturned()
        {
            var openWeatherApiService = new Mock<IOpenWeatherApiService>();
            var configuration = new Mock<IConfiguration>();
            var weatherService = new WeatherService(openWeatherApiService.Object, configuration.Object);
            var weatherDto = new WeatherDto
            {
                Location = "Newcastle",
                CurrentTemperature = 281.71f,
                MinTemperature = 279.87f,
                MaxTemperature = 282.86f,
                Pressure = 1011,
                Humidity = 82,
                Sunrise = 1626727881,
                Sunset = 1626764804
            };

            openWeatherApiService.Setup(x => x.GetWeatherApi(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new WeatherResponse
                {
                    main = new WeatherMain
                    {
                        temp = 281.71f,
                        humidity = 82,
                        pressure = 1011,
                        temp_max = 282.86f,
                        temp_min = 279.87f
                    },
                    name = "Newcastle",
                    sys = new WeatherSys
                    {
                        sunrise = 1626727881,
                        sunset = 1626764804
                    }

                });

            var response = await weatherService.GetWeather("Newcastle", new CancellationToken());

            Assert.AreEqual(weatherDto, response);

        }

        [Test]
        public async Task GetWeather_WhenOpenWeatherApiReturnsBadRequest_BadRequestIsThrown()
        {
            var openWeatherApiService = new Mock<IOpenWeatherApiService>();
            var configuration = new Mock<IConfiguration>();
            var weatherService = new WeatherService(openWeatherApiService.Object, configuration.Object);

            openWeatherApiService.Setup(x => x.GetWeatherApi(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(ApiException.Create(new HttpRequestMessage(), HttpMethod.Get, new HttpResponseMessage(HttpStatusCode.BadRequest), new RefitSettings()).Result);

            Assert.ThrowsAsync<ArgumentException>(async () => await weatherService.GetWeather("", new CancellationToken()));
        }
    }
}
