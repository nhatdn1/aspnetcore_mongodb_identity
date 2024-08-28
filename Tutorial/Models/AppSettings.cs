namespace IntergrateMongodb.Models
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string UsersCollectionName { get; set; } = string.Empty;
    }

    public class Logging
    {
        public LogLevel LogLevel { get; set; } = new LogLevel();
    }

    public class LogLevel
    {
        public string Default { get; set; } = string.Empty;
        public string MicrosoftAspNetCore { get; set; } = string.Empty;
    }

    public class AppSettings
    {
        public MongoDbSettings MongoDbSettings { get; set; } = new MongoDbSettings();
        public Logging Logging { get; set; } = new Logging();
        public string AllowedHosts { get; set; } = string.Empty;
    }
}
