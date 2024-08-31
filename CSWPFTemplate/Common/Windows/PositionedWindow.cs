using CommunityToolkit.Mvvm.Messaging;
using CSWPFTemplate.Common.Messaging.Messages;
using System.ComponentModel;
using System.Windows;

namespace CSWPFTemplate.Common.Windows
{
    /// <summary>
    /// An extension to Window, adding automatic persisting of window position, size and state. The properties are saved in the OnClosing method,
    /// and loaded and set in the OnInitialized method of the base Window class.
    /// </summary>
    /// <param name="windowPositionService">An implementation of IWindowPositionService, providing methods for persisting window properties</param>
    public class PositionedWindow(IWindowPositionPersistanceService windowPositionService, IMessenger messenger) : Window
    {
        #region Services
        private IWindowPositionPersistanceService WindowPositionService { get; } = windowPositionService;
        private IMessenger Messenger { get; } = messenger;
        #endregion

        private string GetTypeName()
        {
            return this.GetType().Name;
        }

        #region State Change Handler
        protected override async void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            string windowName = GetTypeName();
            var info = await WindowPositionService.GetWindowInformation(windowName);
            if (info != null)
            {
                if (info.WindowState != null)
                {
                    WindowState = info.WindowState ?? WindowState.Normal;
                }
                if (info.Position != null)
                {
                    Left = info.Position?.X ?? 100;
                    Top = info.Position?.Y ?? 100;
                }
                if (info.Size != null)
                {
                    Width = info.Size?.Width ?? 500;
                    Height = info.Size?.Height ?? 500;
                }
            }
            //once initialization is done, the busy indicator should be removed
            //to do this, a message gets sent with the current windowName as token
            //this assumes each window type can only exist once - if multiple windows of the same type can be visible in parallel,
            //it would be better to inform the view model directly by something like (this.DataContext as IBusyIndicatorProvider)?.SetBusy(false);
            //(the easiest way would be to set the busy indicator directly within the window, but in this case the view model would be unable to
            //reuse the indicator for other busy states managed by the view model
            Messenger.Send(new IsBusyChangedMessage(value: false), windowName);
        }

        protected override async void OnClosing(CancelEventArgs e)
        {
            await WindowPositionService.SaveWindowInformation(GetTypeName(),
                new WindowInformation()
                {
                    Position = new Point(Left, Top),
                    Size = new Size(Width, Height),
                    WindowState = WindowState
                });

            base.OnClosing(e);
        }
        #endregion
    }
}
