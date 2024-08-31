using System.Windows;

namespace CSWPFTemplate
{
    public partial class App : Application
    {
        private ApplicationSetup? m_app;

        protected override void OnStartup(StartupEventArgs e)
        {
            m_app = new ApplicationSetup();
            m_app.Start();

            //this makes sure that tool tips also show up for disabled controls
            System.Windows.Controls.ToolTipService.ShowOnDisabledProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(true));

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            m_app?.Exit();

            base.OnExit(e);
        }
    }
}
