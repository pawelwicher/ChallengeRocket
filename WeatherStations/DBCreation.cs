using System.Data.SQLite;

namespace WeatherStations
{
    public class DBCreation
    {
        public static SQLiteConnection PrepareData()
        {
            const string commandText = @"
            CREATE TABLE WeatherStationType (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Code TEXT NOT NULL
            );

            INSERT INTO WeatherStationType (Code) VALUES ('Thermometer');
            INSERT INTO WeatherStationType (Code) VALUES ('UvSensor');
            INSERT INTO WeatherStationType (Code) VALUES ('SoilTemperatureMeasurer');

            CREATE TABLE WeatherStation (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                IsActive INTEGER NOT NULL,
                WeatherStationTypeId INTEGER NOT NULL,
                FOREIGN KEY (WeatherStationTypeId) REFERENCES WeatherStationType (Id)
            );

            INSERT INTO WeatherStation (Name, IsActive, WeatherStationTypeId) VALUES ('New York', 1, 1);
            INSERT INTO WeatherStation (Name, IsActive, WeatherStationTypeId) VALUES ('London', 1, 2);
            INSERT INTO WeatherStation (Name, IsActive, WeatherStationTypeId) VALUES ('Berlin', 1, 2);
            INSERT INTO WeatherStation (Name, IsActive, WeatherStationTypeId) VALUES ('Sydney', 0, 1);
            INSERT INTO WeatherStation (Name, IsActive, WeatherStationTypeId) VALUES ('Toronto', 1, 3);
            INSERT INTO WeatherStation (Name, IsActive, WeatherStationTypeId) VALUES ('Tokyo', 0, 1);
            INSERT INTO WeatherStation (Name, IsActive, WeatherStationTypeId) VALUES ('Paris', 1, 2);
            ";

            var connection = new SQLiteConnection("FullUri=file:mem.db?mode=memory");
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = commandText;
            command.ExecuteNonQuery();

            return connection;
        }
    }
}
