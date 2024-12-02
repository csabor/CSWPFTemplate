using System.IO;

namespace CSWPFTemplate.Settings
{
    internal sealed class BaseSettingsService : IBaseSettings
    {
        public string LocalDataDirectory { get; } = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                System.Reflection.Assembly.GetEntryAssembly()?.GetName()?.Name ?? "FallbackAppName");
    }
}
