using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Timers;
using TaskListAppTest.Models;

namespace TaskListAppTest.ViewModels
{
    /// <summary>
    /// Класс модели-представления задачи.
    /// Реализует интерфейс <seealso cref="INotifyPropertyChanged"/>
    /// </summary>
    public class TaskViewModel : INotifyPropertyChanged
    {
        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Имя задачи
        /// </summary>
        public string Name => model.Name;
        /// <summary>
        /// Время работы задачи
        /// </summary>
        public TimeSpan WorkTime => model.WorkTime;
        /// <summary>
        /// Время окончания задачи
        /// </summary>
        public DateTime EndWorkTime => model.EndWorkTime;
        /// <summary>
        /// Статус работы задачи
        /// </summary>
        public WorkStatus Status
        {
            get
            {
                switch (true)
                {
                    case var _ when Progress == 0: return WorkStatus.Started;
                    case var _ when Progress > 0 && Progress < 100: return WorkStatus.InProgress;
                    case var _ when Progress == 100: return WorkStatus.Ended;
                    default: return WorkStatus.NotStarted;
                }
            }
        }
        /// <summary>
        /// Прогресс выполнения задачи
        /// </summary>
        public int Progress
        {
            get => progress;
            private set
            {
                if (value != progress)
                {
                    progress = value;
                    RaisePropertyChanged();
                    RaisePropertiesChanged(nameof(IsCompleted), nameof(Status));
                }
            }
        }
        /// <summary>
        /// Флаг, указывающий на завершение 
        /// выполнения работы задачи
        /// </summary>
        public bool IsCompleted => Status == WorkStatus.Ended;


        private readonly TaskModel model;
        private readonly Timer refreshTimer;
        private int progress;

        /// <summary>
        /// Создает новый экземпляр класса <see cref="TaskViewModel"/>
        /// </summary>
        /// <param name="name">Имя задачи</param>
        /// <param name="time">Время работы задачи</param>
        /// <param name="timer">
        /// Таймер, указывающий, когда необходимо 
        /// обновить данные из модели
        /// </param>
        public TaskViewModel(string name, TimeSpan time, in Timer timer)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"Параметр {nameof(name)} не может быть равен null, " +
                                             "пустой строкой или строкой состоящей только" +
                                             " из символов-разделителей.");
            }

            model = new TaskModel(name, time);

            refreshTimer = timer;
            refreshTimer.Elapsed += TimeToUpdate;
        }


        private void TimeToUpdate(object sender, ElapsedEventArgs e) 
        {
            Progress = (int)(GetPercentageProgress() * 100);
            
            if(IsCompleted)
            {
                refreshTimer.Elapsed -= TimeToUpdate;
            }
        }

        private double GetPercentageProgress()
        {
            var remainingTime = model.EndWorkTime - DateTime.Now;

            if (remainingTime < TimeSpan.Zero || WorkTime == TimeSpan.Zero)
            {
                return 1.0d;
            }
            else
            {
                return 1.0d - (remainingTime.TotalMilliseconds / model.WorkTime.TotalMilliseconds);
            }
        }

        private void RaisePropertyChanged([CallerMemberName]string propertyName = default) => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void RaisePropertiesChanged(params string[] propertiesNames)
        {
            foreach(var propertyName in propertiesNames)
            {
                RaisePropertyChanged(propertyName);
            }
        }
    }
}
