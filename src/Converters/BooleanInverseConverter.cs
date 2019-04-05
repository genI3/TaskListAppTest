using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TaskListAppTest.Converters
{
    /// <summary>
    /// Инвертирует значение <see cref="bool"/>.
    /// Реализует интерфейс <seealso cref="IValueConverter"/>
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    public class BooleanInverseConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool val && targetType == typeof(bool))
            {
                return !val;
            }

            return DependencyProperty.UnsetValue;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool val && targetType == typeof(bool))
            {
                return !val;
            }

            return DependencyProperty.UnsetValue;
        }
    }
}
