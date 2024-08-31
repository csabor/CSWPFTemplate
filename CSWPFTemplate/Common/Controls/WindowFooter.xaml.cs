using System.Windows;
using System.Windows.Controls;

namespace CSWPFTemplate.Common.Controls
{
    public partial class WindowFooter : UserControl
    {
        #region DependecyProperty ShowBusy
        public static readonly DependencyProperty ShowBusyProperty =
            DependencyProperty.Register(
              name: "ShowBusy",
              propertyType: typeof(bool),
              ownerType: typeof(WindowFooter),
              typeMetadata: new FrameworkPropertyMetadata(
                  defaultValue: true,
                  flags: FrameworkPropertyMetadataOptions.AffectsRender,
                  propertyChangedCallback: new PropertyChangedCallback(OnShowBusyChanged)
                  )
            );

        private static void OnShowBusyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
            System.Diagnostics.Debug.WriteLine($"ShowBusy for {d} changed to {e.NewValue}");
            ((WindowFooter)d).IsBusyProgressBar.Visibility = (bool)e.NewValue ? Visibility.Visible : Visibility.Hidden;
        }

        public bool ShowBusy
        {
            get => (bool)GetValue(ShowBusyProperty); 
            set => SetValue(ShowBusyProperty, value);
        }
        #endregion

        public string? VersionString { get; }
        public WindowFooter()
        {
            InitializeComponent();

            VersionString = System.Reflection.Assembly.GetEntryAssembly()?.GetName()?.Version?.ToString();
            VersionText.Text = VersionString;
        }
    }
}
