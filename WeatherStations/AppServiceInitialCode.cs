using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace WeatherStations
{
    public class AppServiceInitialCode
    {
        public IReadOnlyCollection<WeatherStation> GetActiveWeatherStationList(
            SQLiteConnection connection)
        {
            const string commandText = "...";

            using var command = connection.CreateCommand();
            command.CommandText = commandText;

            using var reader = command.ExecuteReader();

            var result = new List<WeatherStation>();

            while (reader.Read())
            {

            }

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
