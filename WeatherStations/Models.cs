namespace WeatherStations
{
    public enum WeatherStationType
    {
        Thermometer = 1,
        UvSensor = 2,
        SoilTemperatureMeasurer = 3
    }

    public class WeatherStation
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public WeatherStationType WeatherStationType { get; set; }
    }

    public class TemperatureMeasurement
    {
        public int WeatherStationId { get; set; }

        public decimal Value { get; set; }
    }

    public class TemperatureStatistics
    {
        public string WeatherStationName { get; set; } = string.Empty;

        public decimal AverageTemperature { get; set; }
    }
}
