using Microsoft.Extensions.Configuration;
using STUDY.CodingTracker.Configuration;

IConfiguration config = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json", optional: false,
    reloadOnChange: true)
.Build();

var databaseSettings = config.GetSection("DatabaseSettings");

Configuration myConfig = new Configuration();
databaseSettings.Bind(myConfig);
Console.WriteLine(myConfig.connectionString + myConfig.databasePath);