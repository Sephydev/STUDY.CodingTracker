using Microsoft.Extensions.Configuration;

namespace STUDY.CodingTracker.Configuration;

internal class Configuration
{
    public string ConnectionString { get; set; } = "";
    public string DatabasePath { get; set; } = "";

}
