﻿using System.Runtime.InteropServices;

namespace CSWPFTemplate.Common.Helpers
{
    internal static class NativeMethods

    {
        [DllImport("user32.dll", SetLastError = true)]

        internal static extern bool SetProcessDpiAwarenessContext(int dpiFlag);

        [DllImport("SHCore.dll", SetLastError = true)]

        internal static extern bool SetProcessDpiAwareness(PROCESS_DPI_AWARENESS awareness);

        [DllImport("user32.dll")]

        internal static extern bool SetProcessDPIAware();

        internal enum PROCESS_DPI_AWARENESS
        {
            Process_DPI_Unaware = 0,
            Process_System_DPI_Aware = 1,
            Process_Per_Monitor_DPI_Aware = 2
        }

        internal enum DPI_AWARENESS_CONTEXT
        {

            DPI_AWARENESS_CONTEXT_UNAWARE = 16,
            DPI_AWARENESS_CONTEXT_SYSTEM_AWARE = 17,
            DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE = 18,
            DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2 = 34
        }
    }
}
