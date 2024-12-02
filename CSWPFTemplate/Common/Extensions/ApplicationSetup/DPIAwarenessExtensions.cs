using CSWPFTemplate.Common.Helpers;
using Microsoft.Extensions.Hosting;

namespace CSWPFTemplate.Common.Extensions.ApplicationSetup
{
    internal static class DPIAwarenessExtensions
    {
        /// <summary>
        /// Registers the application for newest available DPI awareness logic
        /// </summary>
        public static HostApplicationBuilder RegisterDPIAwareness(this HostApplicationBuilder hostBuilder)
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                if (Environment.OSVersion.Version >= new Version(6, 3, 0)) // win 8.1 added support for per monitor dpi
                {
                    if (Environment.OSVersion.Version >= new Version(10, 0, 15063)) // win 10 creators update added support for per monitor v2
                    {
                        NativeMethods.SetProcessDpiAwarenessContext((int)NativeMethods.DPI_AWARENESS_CONTEXT.DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2);
                    }
                    else
                    {
                        NativeMethods.SetProcessDpiAwareness(NativeMethods.PROCESS_DPI_AWARENESS.Process_Per_Monitor_DPI_Aware);
                    }
                }
                else
                {
                    NativeMethods.SetProcessDPIAware();
                }
            }
            return hostBuilder;
        }
    }
}
