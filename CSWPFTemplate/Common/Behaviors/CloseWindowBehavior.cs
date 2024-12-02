using Microsoft.Xaml.Behaviors;
using System.Windows;

namespace CSWPFTemplate.Common.Behaviors
{
    public class CloseWindowBehavior : Behavior<Window>
    {
        public bool ShouldClose
        {
            get { return (bool)GetValue(ShouldCloseProperty); }
            set { SetValue(ShouldCloseProperty, value); }
        }

        public static readonly DependencyProperty ShouldCloseProperty =
            DependencyProperty.Register(
                "ShouldClose",
                typeof(bool),
                typeof(CloseWindowBehavior),
                new PropertyMetadata(false, OnShouldCloseChanged)
                );

        private static void OnShouldCloseChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = d as CloseWindowBehavior;
            behavior?.OnShouldCloseChanged();
        }

        private void OnShouldCloseChanged()
        {
            if (ShouldClose)
            {
                AssociatedObject.Close();
            }
        }
    }
}
