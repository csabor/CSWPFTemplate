using Microsoft.Extensions.Logging;
using System.IO;
using System.Text.Json;

namespace CSWPFTemplate.Common.Windows
{
    /// <summary>
    /// Implementation of the <c ref="IWindowPositionPersistanceService">IWindowPositionPersistanceService</c>
    /// </summary>
    /// <param name="logger">A logger to be injected during construction</param>
    internal class WindowPositionService(ILogger<WindowPositionService> logger) : IWindowPositionPersistanceService
    {
        /// <summary>
        /// The path to save the window settings into. This is located in the LocalApplicationData directory since it is 
        /// machine specific configuration
        /// </summary>
        private readonly string settingsPath =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                System.Reflection.Assembly.GetEntryAssembly()?.GetName()?.Name ?? "FallbackAppName",
                "windowLocations.json");

        private ILogger<WindowPositionService> Logger { get; } = logger;

        public async Task<WindowInformation?> GetWindowInformation(string windowName)
        {
            Logger.LogDebug("Loading window information for {windowName}", windowName);
            var settings = await Read(settingsPath) ?? [];
            settings.TryGetValue(windowName, out WindowInformation? value);
            return value;
        }

        public async Task<bool> SaveWindowInformation(string windowName, WindowInformation windowInformation)
        {
            Logger.LogDebug("Saving window information for {windowName}: {@windowInformation}", windowName, windowInformation);

            var settings = await Read(settingsPath) ?? [];
            settings[windowName] = windowInformation;
            return await Save(settingsPath, settings);
        }

        private async Task<bool> Save(string settingsPath, Dictionary<string, WindowInformation> settings)
        {
            try
            {
                using FileStream fileStream = new(settingsPath, FileMode.Create);
                await JsonSerializer.SerializeAsync(fileStream, settings);
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Could not save window position");
                return false;
            }
        }

        private async Task<Dictionary<string, WindowInformation>?> Read(string settingsPath)
        {
            try
            {
                if (!File.Exists(settingsPath))
                {
                    return null;
                }
                using FileStream fileStream = new(settingsPath, FileMode.Open);
                return await JsonSerializer.DeserializeAsync<Dictionary<string, WindowInformation>>(fileStream);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Could not load window position");
            }
            return null;
        }
    }
}
