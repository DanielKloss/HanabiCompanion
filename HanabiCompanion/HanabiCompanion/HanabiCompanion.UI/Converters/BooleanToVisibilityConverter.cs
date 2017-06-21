using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace HanabiCompanion.UI.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public bool isReversed { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var val = System.Convert.ToBoolean(value);

            if (isReversed)
            {
                val = !val;
            }

            if (val)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
