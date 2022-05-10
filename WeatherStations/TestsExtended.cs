using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace WeatherStations
{
    [TestFixture]
    public class TestsExtended
    {
        [Test]
        public void GetActiveWeatherStationListTest()
        {
            var connection = DBCreation.PrepareData();

            var appService = new AppService();

            var actual = appService.GetActiveWeatherStationList(connection)
                .ToArray();

            var expected = new[]
            {
                new WeatherStation
                {
                    Id = 3,
                    Name = "Berlin",
                    WeatherStationType = WeatherStationType.UvSensor
                },
                new WeatherStation
                {
                    Id = 2,
                    Name = "London",
                    WeatherStationType = WeatherStationType.UvSensor
                },
                new WeatherStation
                {
                    Id = 1,
                    Name = "New York",
                    WeatherStationType = WeatherStationType.Thermometer
                },
                new WeatherStation
                {
                    Id = 7,
                    Name = "Paris",
                    WeatherStationType = WeatherStationType.UvSensor
                },
                new WeatherStation
                {
                    Id = 5,
                    Name = "Toronto",
                    WeatherStationType = WeatherStationType.SoilTemperatureMeasurer
                }
            };

            Assert.IsTrue(expected.Length == actual.Length);
            for (var i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i].Id == actual[i].Id);
                Assert.IsTrue(expected[i].Name == actual[i].Name);
                Assert.IsTrue(expected[i].WeatherStationType == actual[i].WeatherStationType);
            }
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
                temperatureMeasurements)
                .ToArray();

            var expected = new[]
            {
                new TemperatureStatistics() { WeatherStationName = "London", AverageTemperature = 15 },
                new TemperatureStatistics() { WeatherStationName = "New York", AverageTemperature = 25 }
            };

            Assert.IsTrue(expected.Length == actual.Length);
            for (var i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(expected[i].WeatherStationName == actual[i].WeatherStationName);
                Assert.IsTrue(expected[i].AverageTemperature == actual[i].AverageTemperature);
            }
        }

        [Test]
        public void GetTemperatureStatisticsNotOptimalComputationTest()
        {
            var weatherStations = Enumerable.Range(1, 1000)
                .Select(id => new WeatherStation() { Id = id })
                .ToArray();

            var temperatureMeasurements = new List<TemperatureMeasurement>();

            foreach (var station in weatherStations)
            {
                for (var i = 0; i < 100; i++)
                {
                    temperatureMeasurements.Add(new TemperatureMeasurement()
                    {
                        WeatherStationId = station.Id,
                        Value = 100
                    });
                }
            }

            var appService = new AppService();
            var appServiceNotOptimalComputation = new AppServiceNotOptimalComputation();

            var sw1 = System.Diagnostics.Stopwatch.StartNew();
            appService.GetTemperatureStatistics(weatherStations, temperatureMeasurements);
            sw1.Stop();
            var elapsed1 = sw1.Elapsed;

            var sw2 = System.Diagnostics.Stopwatch.StartNew();
            appServiceNotOptimalComputation.GetTemperatureStatistics(weatherStations, temperatureMeasurements);
            sw2.Stop();
            var elapsed2 = sw2.Elapsed;

            Assert.IsTrue(elapsed1.TotalSeconds < elapsed2.TotalSeconds / 2);
        }
    }
}
