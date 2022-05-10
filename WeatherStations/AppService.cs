using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherStations
{
    public class AppService
    {
        private readonly DbContext _dbContext;

        public AppService()
        {
            _dbContext = new DbContext();
        }

        public async Task<IReadOnlyCollection<WeatherStation>> GetActiveWeatherStationList()
        {
            const string commandText = @"
            SELECT ws.Id, ws.Name, wst.Code
              FROM WeatherStation ws
              JOIN WeatherStationType wst ON wst.Id = ws.WeatherStationTypeId
             WHERE ws.IsActive = 1
             ORDER BY ws.Name;";

            using var command = _dbContext.Connection.CreateCommand();
            command.CommandText = commandText;

            using var reader = await command.ExecuteReaderAsync();

            var result = new List<WeatherStation>();

            var weatherStationTypeDict = new Dictionary<string, WeatherStationType>()
            {
                ["Thermometer"] = WeatherStationType.Thermometer,
                ["UV Sensor"] = WeatherStationType.UvSensor,
                ["Soil Temperature Measurer"] = WeatherStationType.SoilTemperatureMeasurer
            };

            while (reader.Read())
            {
                result.Add(new WeatherStation()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    WeatherStationType = weatherStationTypeDict.GetValueOrDefault(reader.GetString(2))
                });
            }

            return result.ToArray();
        }

        public IReadOnlyCollection<TemperatureStatistics> GetTemperatureStatistics(
            IReadOnlyCollection<WeatherStation> weatherStations,
            IReadOnlyCollection<TemperatureMeasurement> temperatureMeasurements)
        {
            var temperatureMeasurementsLookup = temperatureMeasurements
                .ToLookup(x => x.WeatherStationId);

            return weatherStations
                .Select(station => new TemperatureStatistics()
                {
                    WeatherStationName = station.Name,
                    AverageTemperature = temperatureMeasurementsLookup[station.Id]
                        .Average(x => x.Value)
                })
                .OrderBy(x => x.WeatherStationName)
                .ToArray();
        }
    }
}
