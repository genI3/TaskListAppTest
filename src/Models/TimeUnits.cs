namespace TaskListAppTest.Models
{
    /// <summary>
    /// Перечисление, представляющее
    /// единицы измерения времени
    /// </summary>
    public enum TimeUnits
    {
        /// <summary>
        /// Не определено
        /// </summary>
        Undefined = -1,
        /// <summary>
        /// Милисекунды
        /// </summary>
        Milliseconds, 
        /// <summary>
        /// Секунды
        /// </summary>
        Seconds,
        /// <summary>
        /// Минуты
        /// </summary>
        Minutes,
        /// <summary>
        /// Часы
        /// </summary>
        Hours,
        /// <summary>
        /// Дни
        /// </summary>
        Days
    }
}
