using System;
using Windows.UI.Xaml.Data;

namespace HanabiCompanion.UI.Converters
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime dateTime = System.Convert.ToDateTime(value);

            return dateTime.ToString("dd MMM yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
