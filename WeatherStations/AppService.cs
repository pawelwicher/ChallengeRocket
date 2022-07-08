using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace WeatherStations
{
    public class AppService
    {
        public IReadOnlyCollection<WeatherStation> GetActiveWeatherStationList(
            SQLiteConnection connection)
        {
            const string commandText = @"
            SELECT ws.Id, ws.Name, wst.Code
              FROM WeatherStation ws
              JOIN WeatherStationType wst ON wst.Id = ws.WeatherStationTypeId
             WHERE ws.IsActive = 1
             ORDER BY ws.Name;";

            using var command = connection.CreateCommand();
            command.CommandText = commandText;

            using var reader = command.ExecuteReader();

            var result = new List<WeatherStation>();

            while (reader.Read())
            {
                result.Add(new WeatherStation()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    WeatherStationType = Enum.Parse<WeatherStationType>(reader.GetString(2))
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
