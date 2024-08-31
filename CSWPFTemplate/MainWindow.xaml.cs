using CommunityToolkit.Mvvm.Messaging;
using CSWPFTemplate.Common.Windows;
using CSWPFTemplate.ViewModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CSWPFTemplate
{
    public partial class MainWindow : PositionedWindow
    {
        public MainWindow(MainWindowViewModel viewModel, IWindowPositionPersistanceService windowPositionService, IMessenger messenger) : base(windowPositionService, messenger)
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        #region GridView Sorting
        GridViewColumnHeader? _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;

        void GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        {
            ListSortDirection direction;

            if (e.OriginalSource is not GridViewColumnHeader headerClicked
                || sender is not ListView listView 
                || headerClicked.Role == GridViewColumnHeaderRole.Padding)
            {
                return;
            }
            if (headerClicked != _lastHeaderClicked)
            {
                direction = ListSortDirection.Ascending;
            }
            else
            {
                if (_lastDirection == ListSortDirection.Ascending)
                {
                    direction = ListSortDirection.Descending;
                }
                else
                {
                    direction = ListSortDirection.Ascending;
                }
            }

            var columnBinding = headerClicked.Column.DisplayMemberBinding as Binding;
            var sortBy = columnBinding?.Path.Path ?? headerClicked.Column.Header as string;

            Sort(listView, sortBy, direction);

            if (direction == ListSortDirection.Ascending)
            {
                headerClicked.Column.HeaderTemplate = Resources["HeaderTemplateArrowUp"] as DataTemplate;
            }
            else
            {
                headerClicked.Column.HeaderTemplate = Resources["HeaderTemplateArrowDown"] as DataTemplate;
            }

            // Remove arrow from previously sorted header
            if (_lastHeaderClicked != null && _lastHeaderClicked != headerClicked)
            {
                _lastHeaderClicked.Column.HeaderTemplate = null;
            }

            _lastHeaderClicked = headerClicked;
            _lastDirection = direction;
        }

        private static void Sort(ListView lv, string? sortBy, ListSortDirection direction)
        {
            ICollectionView dataView =
              CollectionViewSource.GetDefaultView(lv.ItemsSource);

            dataView.SortDescriptions.Clear();
            SortDescription sd = new(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }
        #endregion

        #region Nested Datagrid Scrolling Workaround
        //when the mouse is positioned over the inner datagrid, only this one gets scrolled
        //this workaround routes the scrolling to the parent datagrid, so the complete area of the datagrid scrolls
        //this needs to be registered as event handler in the inner datagrid
        //TODO can this be converted into a behaviour or attached property?
        private void NestedDataGrid_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            e.Handled = true;
            System.Windows.Input.MouseWheelEventArgs eventArg = new(e.MouseDevice, e.Timestamp, e.Delta)
            {
                RoutedEvent = MouseWheelEvent,
                Source = sender
            };
            UIElement? parent = (sender as Control)?.Parent as UIElement;
            parent?.RaiseEvent(eventArg);
        }
        #endregion
    }
}