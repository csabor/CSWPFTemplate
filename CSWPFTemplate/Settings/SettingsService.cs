using Microsoft.Extensions.Configuration;

namespace CSWPFTemplate.Settings
{
    /// <summary>
    /// Implements the ISettingsService by loading the settings from IConfiguration 
    /// (which gets filled by the HostApplicationBuilder from environment and appsettings.json)
    /// </summary>
    /// <param name="configuration">The IConfiguration injected during construction.</param>
    internal sealed class SettingsService(IConfiguration configuration) : ISettingsService
    {
        private readonly IConfiguration configuration = configuration;

        public string? DatabaseLocation
        {
            get => configuration.GetValue("database_location", "");
        }
    }
}
