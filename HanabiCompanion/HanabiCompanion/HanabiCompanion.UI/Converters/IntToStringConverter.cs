using System;
using Windows.UI.Xaml.Data;

namespace HanabiCompanion.UI.Converters
{
    public class IntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (System.Convert.ToInt32(value) == 0)
            {
                return string.Empty;
            }
            else
            {
                return value.ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value.ToString() == string.Empty)
            {
                return 0;
            }
            else
            {
                return System.Convert.ToInt32(value);
            }
        }
    }
}
