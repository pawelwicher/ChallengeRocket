using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherStations
{
    public class AppServiceInitialCode
    {
        private readonly DbContext _dbContext;

        public AppServiceInitialCode()
        {
            _dbContext = new DbContext();
        }

        public async Task<IReadOnlyCollection<WeatherStation>> GetActiveWeatherStationList()
        {
            const string commandText = "...";

            using var command = _dbContext.Connection.CreateCommand();
            command.CommandText = commandText;

            using var reader = await command.ExecuteReaderAsync();

            var result = new List<WeatherStation>();

            return result.ToArray();
        }

        public IReadOnlyCollection<TemperatureStatistics> GetTemperatureStatistics(
            IReadOnlyCollection<WeatherStation> weatherStations,
            IReadOnlyCollection<TemperatureMeasurement> temperatureMeasurements)
        {
            return Array.Empty<TemperatureStatistics>();
        }
    }
}
