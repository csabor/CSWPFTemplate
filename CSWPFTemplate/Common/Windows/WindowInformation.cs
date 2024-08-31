
using System.Windows;

namespace CSWPFTemplate.Common.Windows
{
    /// <summary>
    /// Combined information indicating position and size of a window
    /// </summary>
    public sealed class WindowInformation
    {
        public Point? Position { get; set; }
        public Size? Size { get; set; }
        public WindowState? WindowState { get; set; }
    }
}
