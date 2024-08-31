
namespace CSWPFTemplate.Common.Windows
{
    /// <summary>
    /// Service which allows individual windows to save and load the position, size and state associated with the name of the window.
    /// </summary>
    public interface IWindowPositionPersistanceService
    {
        /// <summary>
        /// Loads the information saved to a specific name, returning the position information or null if none is available.
        /// </summary>
        /// <param name="windowName">The name of the window.</param>
        /// <returns>The WindowInformation or null if no information is found for this window.</returns>
        Task<WindowInformation?> GetWindowInformation(string windowName);

        /// <summary>
        /// Saves the provided information for the specified window name. 
        /// This is designed to be low priority, so if something fails during saving, the method returns false but no exception is raised
        /// </summary>
        /// <param name="windowName">The name of the window.</param>
        /// <param name="windowInformation">The information to save for the window.</param>
        /// <returns>True if saving succeeded, otherwise false</returns>
        Task<bool> SaveWindowInformation(string windowName, WindowInformation windowInformation);
    }
}