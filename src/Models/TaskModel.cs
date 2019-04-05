using System;

namespace TaskListAppTest.Models
{
    /// <summary>
    /// Класс модели задачи
    /// </summary>
    public class TaskModel
    {
        /// <summary>
        /// Имя задачи
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Время работы задачи
        /// </summary>
        public TimeSpan WorkTime { get; private set; }
        /// <summary>
        /// Время выполнения задачи
        /// </summary>
        public DateTime EndWorkTime { get; private set; }

        /// <summary>
        /// Создает новый экземпляр класса <see cref="TaskModel"/>
        /// </summary>
        /// <param name="name">Имя задачи</param>
        /// <param name="time">Время работы задачи</param>
        public TaskModel(string name, TimeSpan time)
        {
            Name = name ?? 
                   throw new ArgumentException($"Параметр {nameof(name)} не может быть равен null, " +
                                                "пустой строкой или строкой состоящей только " +
                                                "из символов-разделителей."); ;
            
            WorkTime = time;
            EndWorkTime = DateTime.Now + time;
        }        
    }
}
