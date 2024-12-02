#region License Information
/*
   Copyright 2024 csabor https://github.com/csabor

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/
#endregion

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CSWPFTemplate.Setup;
using Serilog;
using System.Windows;
using CSWPFTemplate.Common.Extensions.ApplicationSetup;

namespace CSWPFTemplate
{
    /// <summary>
    /// This class manages setup, start and closing of the application.
    /// </summary>
    internal class ApplicationSetup
    {
        private readonly IHost _host;

        /// <summary>
        /// Initializes the application by creating a HostApplicationBuilder and configuring it using methods in
        /// <c cref="ApplicationBuilderExtensions">ApplicationBuilderExtensions</c>
        /// </summary>
        public ApplicationSetup()
        {
            var builder = Host.CreateApplicationBuilder()
                .RegisterDPIAwareness()
                .ConfigureSerilog()
                .RegisterServices()
                .RegisterViewModels()
                .RegisterViews();

            _host = builder.Build();
        }

        /// <summary>
        /// Starts the application, displaying the MainWindow.
        /// </summary>
        public async void Start()
        {
            try
            {
                await _host.StartAsync();

                MainWindow startupForm = _host.Services.GetRequiredService<MainWindow>();
                startupForm.Show();
            }
            catch (Exception ex)
            {
                _host?.Services?.GetService<ILogger>()?.Fatal(ex, "Could not init application");
                MessageBox.Show($"Could not init application: {ex.Message}", "Application Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }
        
        /// <summary>
        /// Ends the application cleanly.
        /// </summary>
        public async void Exit()
        {
            using (_host)
            {
                await _host.StopAsync(TimeSpan.FromSeconds(2));

                //this makes sure that disposable services also get disposed
                if (_host is IAsyncDisposable disposable)
                {
                    await disposable.DisposeAsync();
                }
            }
        }
    }
}
