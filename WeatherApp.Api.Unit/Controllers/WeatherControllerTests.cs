using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using WeatherApp.Api.Controllers;
using WeatherApp.Api.Services;
using WeatherApp.Application.Weather.DTOs;

namespace WeatherApp.Api.UnitTests.Controllers
{
    [TestFixture]
    public class WeatherControllerTests
    {
        [Test]
        public async Task GetWeather_WhenLocationIsValid_WeatherReturned()
        {
            var weatherService = new Mock<IWeatherService>();
            var weatherController = new WeatherController(weatherService.Object);
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

            weatherService.Setup(x => x.GetWeather(It.IsAny<string>(), new CancellationToken()))
                .ReturnsAsync(new WeatherDto
                {
                    Location = "Newcastle",
                    CurrentTemperature = 281.71f,
                    MinTemperature = 279.87f,
                    MaxTemperature = 282.86f,
                    Pressure = 1011,
                    Humidity = 82,
                    Sunrise = 1626727881,
                    Sunset = 1626764804
                });

            var response = await weatherController.GetWeather("Newcastle", new CancellationToken()) as OkObjectResult;

            Assert.AreEqual(weatherDto, response.Value);

        }

        [Test]
        public async Task GetWeather_WhenLocationIsInvalid_BadRequestReturned()
        {
            var weatherService = new Mock<IWeatherService>();
            var weatherController = new WeatherController(weatherService.Object); 

            var response = await weatherController.GetWeather(null, new CancellationToken()) as BadRequestResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, response.StatusCode);

        }
    }
}
