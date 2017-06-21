using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace HanabiCompanion.UI.Converters
{
    public class ListColourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var val = System.Convert.ToInt32(value);

            if (val % 2 == 0)
            {
                return new SolidColorBrush(Color.FromArgb(255, 230, 232, 252));
            }
            else
            {
                return new SolidColorBrush(Color.FromArgb(255, 187, 192, 238));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
