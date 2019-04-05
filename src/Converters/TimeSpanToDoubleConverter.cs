using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using TaskListAppTest.Models;

namespace TaskListAppTest.Converters
{
    /// <summary>
    /// Конвертирует значение времени <see cref="TimeSpan"/>
    /// в <see cref="double"/> представление.
    /// Реализует интерфейс <seealso cref="IValueConverter"/>
    /// </summary>
    [ValueConversion(typeof(TimeSpan), typeof(double))]   
    public class TimeSpanToDoubleConverter : IValueConverter
    {
        /// <summary>
        /// Единицы измерения времени
        /// </summary>
        public TimeUnits TimeUnit { get; set; }

        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeSpan time)
            {
                var dVal = TimeSpanConvertToDouble(time);

                return parameter is string format ? dVal.ToString(format, culture)
                                                  : dVal.ToString();
            }

            return DependencyProperty.UnsetValue;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str && targetType == typeof(TimeSpan) &&
                double.TryParse(str, out var dVal) &&
                dVal <= TimeSpanConvertToDouble(TimeSpan.MaxValue))
            {
                switch (TimeUnit)
                {
                    case TimeUnits.Days:
                        return TimeSpan.FromDays(dVal);
                    case TimeUnits.Hours:
                        return TimeSpan.FromHours(dVal);
                    case TimeUnits.Milliseconds:
                        return TimeSpan.FromMilliseconds(dVal);
                    case TimeUnits.Minutes:
                        return TimeSpan.FromMinutes(dVal);
                    case TimeUnits.Seconds:
                        return TimeSpan.FromSeconds(dVal);
                }
            }

            return DependencyProperty.UnsetValue;
        }


        private double TimeSpanConvertToDouble(TimeSpan time)
        {
            switch (TimeUnit)
            {
                case TimeUnits.Days:
                    return time.TotalDays;
                case TimeUnits.Hours:
                    return time.TotalHours;
                case TimeUnits.Milliseconds:
                    return time.TotalMilliseconds;
                case TimeUnits.Minutes:
                    return time.TotalMinutes;
                case TimeUnits.Seconds:
                    return time.TotalSeconds;
                default:
                    return double.NaN;
            }
        }
    }
}