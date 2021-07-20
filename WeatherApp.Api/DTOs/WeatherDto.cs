namespace WeatherApp.Application.Weather.DTOs
{
    public record WeatherDto
    {
        public string Location { get; set; }
        public float CurrentTemperature { get; set; }
        public float MinTemperature { get; set; }
        public float MaxTemperature { get; set; }
        public float Pressure { get; set; }
        public float Humidity { get; set; }
        public int Sunrise { get; set; }
        public int Sunset { get; set; }
    }
}
