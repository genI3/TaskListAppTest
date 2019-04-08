using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using TaskListAppTest.Utils;

namespace TaskListAppTest.ViewModels
{
    /// <summary>
    /// Класс главной модели-представления.
    /// Реализует интерфейс <seealso cref="INotifyPropertyChanged"/>
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Список задач
        /// </summary>
        public ObservableCollection<TaskViewModel> TaskList { get; } = new ObservableCollection<TaskViewModel>();
        
        /// <summary>
        /// Команда добавления новой задачи
        /// </summary>
        public ICommand AddTaskCommand => new DelegateCommand(CreateAndStartNewTask);
        /// <summary>
        /// Команда удаления задачи
        /// </summary>
        public ICommand RemoveTaskCommand => new DelegateCommand(RemoveTask);

        /// <summary>
        /// Задает время работы новой задачи
        /// </summary>
        public TimeSpan NewTaskTime
        {
            get => newTaskTime;
            set
            {
                if (newTaskTime != value)
                {
                    newTaskTime = value;
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Задает имя новой задачи
        /// </summary>
        public string NewTaskName
        {
            get => newTaskName;
            set
            {
                if (newTaskName != value)
                {
                    newTaskName = value;
                    RaisePropertyChanged();
                }
            }
        }


        private readonly TimeSpan refreshFrequency = TimeSpan.FromSeconds(1.0d);
        private readonly Timer workerTimer;
        private string newTaskName = string.Empty;
        private TimeSpan newTaskTime = TimeSpan.Zero;


        /// <summary>
        /// Создает новый экземпляр класса <see cref="MainViewModel"/>
        /// </summary>
        public MainViewModel()
        {
            workerTimer = new Timer(refreshFrequency.TotalMilliseconds)
            {
                AutoReset = true
            };

            workerTimer.Elapsed += TimeToCheck;
        }


        private void TimeToCheck(object sender, ElapsedEventArgs e)
        {
            if(workerTimer.Enabled && (!TaskList.Any() || TaskList.All(t => t.IsCompleted)))
            {
                workerTimer.Stop();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void CreateAndStartNewTask(object parameter)
        {
            if (string.IsNullOrWhiteSpace(NewTaskName))
            {
                MessageBox.Show("Необходимо задать название.", "Ошибка",
                                MessageBoxButton.OK, 
                                MessageBoxImage.Exclamation);
                return;
            }

            var newTask = new TaskViewModel(NewTaskName, NewTaskTime, workerTimer);

            TaskList.Add(newTask);

            ClearParameters();

            if (!workerTimer.Enabled)
            {
                workerTimer.Start();
            }
        }

        private void RemoveTask(object removingTask)
        {
            if (removingTask is TaskViewModel taskVM)
            {
                TaskList.Remove(taskVM);
            }
        }

        private void ClearParameters()
        {
            NewTaskName = string.Empty;
            NewTaskTime = TimeSpan.Zero;
        }

        private void RaisePropertyChanged([CallerMemberName]string propertyName = default) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
