using Microsoft.Data.Sqlite;

namespace WeatherStations
{
    public class DbContext
    {
        private readonly SqliteConnection _connection;

        public DbContext()
        {
            _connection = new SqliteConnection("Data Source=WeatherStations;Mode=Memory");
            _connection.Open();
            Seed();
        }

        public SqliteConnection Connection => _connection;

        private void Seed()
        {
            const string commandText = @"

            CREATE TABLE WeatherStationType (
               Id INTEGER PRIMARY KEY AUTOINCREMENT,
               Code TEXT NOT NULL               
            );

            INSERT INTO WeatherStationType (Code) VALUES ('Thermometer');
            INSERT INTO WeatherStationType (Code) VALUES ('UV Sensor');
            INSERT INTO WeatherStationType (Code) VALUES ('Soil Temperature Measurer');

            CREATE TABLE WeatherStation (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                IsActive INTEGER NOT NULL,
                WeatherStationTypeId INTEGER,
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
            
            using var command = _connection.CreateCommand();
            command.CommandText = commandText;
            command.ExecuteNonQuery();
        }
    }
}
