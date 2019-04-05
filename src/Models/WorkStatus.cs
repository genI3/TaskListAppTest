namespace TaskListAppTest.Models
{
    /// <summary>
    /// Перечисление, представляющее
    /// статус работы
    /// </summary>
    public enum WorkStatus
    {
        /// <summary>
        /// Не запущена
        /// </summary>
        NotStarted,
        /// <summary>
        /// Окончена
        /// </summary>
        Ended,
        /// <summary>
        /// Запущена
        /// </summary>
        Started,
        /// <summary>
        /// В процессе выполнения
        /// </summary>
        InProgress
    }
}
