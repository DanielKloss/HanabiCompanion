using System;
using Windows.UI.Xaml.Data;

namespace HanabiCompanion.UI.Converters
{
    public class BooleanReverserConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool val = System.Convert.ToBoolean(value);

            return !val;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
