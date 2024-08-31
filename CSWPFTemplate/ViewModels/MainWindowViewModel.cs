using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CSWPFTemplate.Common.Messaging.Messages;
using CSWPFTemplate.Models;
using CSWPFTemplate.Settings;
using System.Collections.ObjectModel;

namespace CSWPFTemplate.ViewModels
{
    //this class receives messages, so it should inherit ObservableRecipient and handle registration and unregistration.
    //Otherwise inheriting just ObservableObject would be ok
    public sealed partial class MainWindowViewModel : ObservableRecipient 
    {
        #region Constructor
        public MainWindowViewModel(ISettingsService settings, IMessenger messenger) : base(messenger)
        {
            IsActive = true; //set the view model to active to allow receiving messages

            InitData(settings);
        }
        #endregion

        #region IsBusy
        [ObservableProperty]
        private bool isBusy = true;
        #endregion

        #region Message Handling
        readonly static string AssociatedView = typeof(MainWindow).Name;

        protected override void OnActivated()
        {
            base.OnActivated();

            //register for the IsBusyChangedMessage to handle busy information for this view from anywhere
            Messenger.Register<MainWindowViewModel, IsBusyChangedMessage, string>(this, AssociatedView, (r, m) =>
            {
                IsBusy = m.Value;
            });
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();

            ///unregister from any messages registered in OnActivated to clean up
            Messenger.Unregister<IsBusyChangedMessage, string>(this, AssociatedView);
        }
        #endregion

        #region Menu Commands
        public static RelayCommand MenuCommandQuit { get => new(System.Windows.Application.Current.Shutdown); }
        public RelayCommand MenuCommandNew { get => new(() => IsBusy = !IsBusy); }
        #endregion

        #region Data
        private ObservableCollection<Category> categories = [];

        public ObservableCollection<Category> Categories
        {
            get { return categories; }
            set { SetProperty(ref categories, value); }
        }

        private ObservableCollection<DatailRow> detailRows = [];

        public ObservableCollection<DatailRow> DetailRows
        {
            get { return detailRows; }
            set { SetProperty(ref detailRows, value); }
        }

        private void InitData(ISettingsService settings)
        {
            categories.Add(new Category("Add Item...", null, true));

            var rnd = new Random();
            for (int i = 0; i < 10_000; i++)
            {
                detailRows.Add(new DatailRow()
                {
                    Category = "Nothing",
                    Text = "MyTest",
                    Note = rnd.NextInt64().ToString()
                });
            }
        }
        #endregion
    }
}
