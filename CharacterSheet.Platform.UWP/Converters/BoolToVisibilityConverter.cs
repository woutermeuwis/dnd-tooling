using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace CharacterSheet.Platform.UWP.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool isVisible)
                return isVisible ? Visibility.Visible : Visibility.Collapsed;

            throw new ArgumentException($"{nameof(BoolToVisibilityConverter)}.{nameof(Convert)} parameter {nameof(value)} was of type {value.GetType().FullName} but expected type {typeof(Visibility).FullName}", nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is Visibility visibility)
                return visibility == Visibility.Visible;

            throw new ArgumentException($"{nameof(BoolToVisibilityConverter)}.{nameof(ConvertBack)} parameter {nameof(value)} was of type {value.GetType().FullName} but expected type {typeof(Visibility).FullName}", nameof(value));
        }
    }
}
