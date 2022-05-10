using NUnit.Framework;

namespace WeatherStations
{
    [TestFixture]
    public class TestsBasic
    {
        [Test]
        public void GetActiveWeatherStationListTest()
        {
            var connection = DBCreation.PrepareData();

            var appService = new AppService();

            var actual = appService.GetActiveWeatherStationList(connection);

            Assert.AreEqual(5, actual.Count);
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

            Assert.AreEqual(2, actual.Count);
        }
    }
}
