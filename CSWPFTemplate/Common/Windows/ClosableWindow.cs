using CommunityToolkit.Mvvm.ComponentModel;

namespace CSWPFTemplate.Common.Windows
{
    public partial class ClosableWindow : ObservableObject
    {
        [ObservableProperty]
        private bool shouldWindowClose = false;

        protected void CloseWindow()
        {
            ShouldWindowClose = true;
        }
    }
}
