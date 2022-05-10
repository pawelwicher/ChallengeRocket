using System.Collections.Generic;
using System.Linq;

namespace WeatherStations
{
    public class AppServiceNotOptimalComputation
    {
        public IReadOnlyCollection<TemperatureStatistics> GetTemperatureStatistics(
            IReadOnlyCollection<WeatherStation> weatherStations,
            IReadOnlyCollection<TemperatureMeasurement> temperatureMeasurements)
        {
            return weatherStations
                .Select(station => new TemperatureStatistics()
                {
                    WeatherStationName = station.Name,
                    AverageTemperature = temperatureMeasurements
                        .Where(x => x.WeatherStationId == station.Id)
                        .Average(x => x.Value)
                })
                .OrderBy(x => x.WeatherStationName)
                .ToArray();
        }
    }
}
