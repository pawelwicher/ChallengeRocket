using Xunit;
using FluentAssertions;

namespace WeatherStations
{
    public class TestsBasic
    {
        [Fact]
        public async void GetActiveWeatherStationListTest()
        {
            var appService = new AppService();

            var actual = await appService.GetActiveWeatherStationList();

            actual.Should().HaveCount(5);
        }

        [Fact]
        public void GetTemperatureStatisticsTest()
        {
            var appService = new AppService();

            var weatherStations = new[]
            {
                new WeatherStation
                {
                    Id = 1,
                    Name = "New York",
                    WeatherStationType = WeatherStationType.Thermometer
                },
                new WeatherStation
                {
                    Id = 2,
                    Name = "London",
                    WeatherStationType = WeatherStationType.UvSensor
                }
            };
            var temperatureMeasurements = new[]
            {
                new TemperatureMeasurement() { WeatherStationId = 1, Value = 25 },
                new TemperatureMeasurement() { WeatherStationId = 2, Value = 15 },
                new TemperatureMeasurement() { WeatherStationId = 1, Value = 25 },
                new TemperatureMeasurement() { WeatherStationId = 2, Value = 15 }
            };

            var actual = appService.GetTemperatureStatistics(
                weatherStations,
                temperatureMeasurements);

            actual.Should().HaveCount(2);
        }
    }
}
