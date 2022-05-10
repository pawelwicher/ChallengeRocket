using NUnit.Framework;
using FluentAssertions;

namespace WeatherStations
{
    [TestFixture]
    public class TestsBasic
    {
        [Test]
        public void GetActiveWeatherStationListTest()
        {
            var appService = new AppService();

            var actual = appService.GetActiveWeatherStationList();

            actual.Should().HaveCount(5);
        }

        [Test]
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
