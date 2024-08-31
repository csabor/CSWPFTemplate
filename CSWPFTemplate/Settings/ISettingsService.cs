namespace CSWPFTemplate.Settings
{
    /// <summary>
    /// Service managing the settings specific for the application. The settings can be added in the interface and the implementing class to be provided by injection.
    /// </summary>
    public interface ISettingsService
    {
        /// <summary>
        /// Example setting DatabaseLocation
        /// </summary>
        public string? DatabaseLocation { get; }
    }
}
