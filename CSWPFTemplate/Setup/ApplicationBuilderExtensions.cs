using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CSWPFTemplate.Common.Windows;
using CSWPFTemplate.Settings;
using CSWPFTemplate.ViewModels;
using Serilog;
using Serilog.Exceptions;
using System.IO;

namespace CSWPFTemplate.Setup
{
    internal static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Configures Serilog as logger, logging into the LocalApplicationData directory, adding fitting default configuration.
        /// This configuration can be changed or extended (potentially after deployment) by modifying the appsettings.json file
        /// according to <see href="https://github.com/serilog/serilog-settings-configuration"/>.
        /// </summary>
        /// <param name="hostBuilder">The <c cref="HostApplicationBuilder">HostApplicationBuilder</c> used to build the application.</param>
        /// <returns>The provided <c cref="HostApplicationBuilder">HostApplicationBuilder</c> for further use.</returns>
        public static HostApplicationBuilder ConfigureSerilog(this HostApplicationBuilder hostBuilder)
        {
            string file = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                System.Reflection.Assembly.GetEntryAssembly()?.GetName()?.Name ?? "FallbackAppName",
                "Application.log");
            Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Verbose)
#endif
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            //.Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
            //    .WithDefaultDestructurers()
            //    .WithDestructurers(new[] { new SqlExceptionDestructurer() })) //sqlclient
            //    .WithDestructurers(new[] { new DbUpdateExceptionDestructurer() })) //entityframework
            .WriteTo.Debug()
            .WriteTo.File(file, rollingInterval: RollingInterval.Day)
            .ReadFrom.Configuration(hostBuilder.Configuration) //add settings from appsettings.json / appsettings.[Environment].json to the programmatic configuration
            .CreateLogger();

            hostBuilder.Services.AddLogging(logging => logging.AddSerilog(dispose: true));
            hostBuilder.Services.AddSerilog();

            return hostBuilder;
        }

        /// <summary>
        /// Registers common services for use in the application
        /// </summary>
        /// <param name="hostBuilder">The <c cref="HostApplicationBuilder">HostApplicationBuilder</c> used to build the application</param>
        /// <returns>The provided <c cref="HostApplicationBuilder">HostApplicationBuilder</c> for further use.</returns>
        public static HostApplicationBuilder RegisterServices(this HostApplicationBuilder hostBuilder)
        {
            //use the StrongReferenceMessenger for injection, since we plan on using ObservableRecipient everywhere and unregistering handlers correctly
            hostBuilder.Services.AddSingleton<IMessenger>(StrongReferenceMessenger.Default);
            //hostBuilder.Services.AddTransient<ILoggingService, LoggingService>();
            hostBuilder.Services.AddSingleton(Log.Logger);
            hostBuilder.Services.AddTransient<ISettingsService, SettingsService>();
            hostBuilder.Services.AddSingleton<IWindowPositionPersistanceService, WindowPositionService>();

            // More services registered here.

            return hostBuilder;
        }

        /// <summary>
        /// Registers view models used in the application.
        /// </summary>
        /// <param name="hostBuilder">The <c cref="HostApplicationBuilder">HostApplicationBuilder</c> used to build the application</param>
        /// <returns>The provided <c cref="HostApplicationBuilder">HostApplicationBuilder</c> for further use.</returns>
        public static HostApplicationBuilder RegisterViewModels(this HostApplicationBuilder hostBuilder)
        {
            hostBuilder.Services.AddSingleton<MainWindowViewModel>();

            // More view-models registered here.

            return hostBuilder;
        }

        /// <summary>
        /// Registers the views used in the application.
        /// </summary>
        /// <param name="hostBuilder">The <c cref="HostApplicationBuilder">HostApplicationBuilder</c> used to build the application</param>
        /// <returns>The provided <c cref="HostApplicationBuilder">HostApplicationBuilder</c> for further use.</returns>
        public static HostApplicationBuilder RegisterViews(this HostApplicationBuilder hostBuilder)
        {
            hostBuilder.Services.AddSingleton<MainWindow>();

            // More views registered here.

            return hostBuilder;
        }
    }
}
