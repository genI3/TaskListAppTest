using System.Windows;
using System.Windows.Controls;

namespace TaskListAppTest.Controls
{
    /// <summary>
    /// Класс представляющий расширенный
    /// вариант класса <see cref="TextBox"/>
    /// </summary>
    public class ExtendedTextBox : TextBox
    {
        /// <summary>
        /// Задает контент
        /// в правой части поля ввода
        /// <seealso cref="TextBox.Text"/>
        /// </summary>
        public Control RightContent
        {
            set => SetValue(RightContentProperty, value);
            get => (Control)GetValue(RightContentProperty);
        }
        /// <summary>
        /// Свойство зависимости для
        /// свойства <see cref="RightContent"/>
        /// </summary>
        public static readonly DependencyProperty RightContentProperty =
            DependencyProperty.Register(nameof(RightContent), typeof(Control), 
                                        typeof(ExtendedTextBox), 
                                        new PropertyMetadata(default(Control), null));

        /// <summary>
        /// Задает текст заполнитель
        /// при отсутствии значения
        /// в поле ввода 
        /// <seealso cref="TextBox.Text"/>
        /// </summary>
        public string PlaceHolder
        {
            get => (string)GetValue(PlaceHolderProperty);
            set => SetValue(PlaceHolderProperty, value);
        }
        /// <summary>
        /// Свойство зависимости для
        /// свйоства <see cref="PlaceHolder"/>
        /// </summary>
        public static readonly DependencyProperty PlaceHolderProperty =
            DependencyProperty.Register(nameof(PlaceHolder), typeof(string), 
                                        typeof(ExtendedTextBox), 
                                        new PropertyMetadata(string.Empty, null));
    }
}
