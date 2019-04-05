using System;
using System.Windows.Input;

namespace TaskListAppTest.Utils
{
    /// <summary>
    /// Класс представляющий делегированную команду.
    /// Реализует интерфейс <seealso cref="ICommand"/>.
    /// </summary>
    public class DelegateCommand : ICommand
    {        
        /// <inheritdoc/>
        public event EventHandler CanExecuteChanged;


        private readonly Predicate<object> canExecute;
        private readonly Action<object> executeAction;


        /// <summary>
        /// Создает новый экземпляр 
        /// делегированной команды
        /// </summary>
        /// <param name="execute">
        /// Метод, который долежн 
        /// вызываться по команде
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Если <paramref name="execute"/> равен <code>null</code>
        /// </exception>
        public DelegateCommand(Action<object> execute) : this(execute, null) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="execute">
        /// Метод, который долежн 
        /// вызываться по команде
        /// </param>
        /// <param name="canExecute">
        /// Предикат, определяющий
        /// возможность вызова команды
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Если <paramref name="execute"/> равен <code>null</code>
        /// </exception>
        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            executeAction = execute ?? 
                            throw new ArgumentNullException(nameof(execute), 
                                    "Нельзя создать комманду с вызовом null");
            this.canExecute = canExecute;
        }

        /// <inheritdoc/>
        public bool CanExecute(object parameter) => canExecute?.Invoke(parameter) ?? true;

        /// <inheritdoc/>
        public void Execute(object parameter) => executeAction(parameter);

        /// <summary>
        /// Инициирует проверку
        /// изменения возможности
        /// выполнения команды
        /// </summary>
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
