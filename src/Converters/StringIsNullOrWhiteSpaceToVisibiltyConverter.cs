using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TaskListAppTest.Converters
{
    /// <summary>
    /// Конвертирует объект <see cref="string"/>
    /// в состояние отображения элемента <see cref="Visibility"/>.
    /// Реализует интерфейс <seealso cref="IValueConverter"/>
    /// </summary>
    [ValueConversion(typeof(string), typeof(Visibility))]
    public class StringIsNullOrWhiteSpaceToVisibiltyConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                return string.IsNullOrWhiteSpace(str) ? Visibility.Visible : Visibility.Hidden;
            }

            return DependencyProperty.UnsetValue;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => DependencyProperty.UnsetValue;
    }
}
