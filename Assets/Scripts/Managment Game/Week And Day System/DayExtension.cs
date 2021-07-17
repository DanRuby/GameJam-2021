public static class DayExtension 
{
    public static string ToRussianString(this Day day)
    {
        switch(day)
        {
            case Day.Monday:
                return "Понедельник";
            case Day.Tuesday:
                return "Вторник";
            case Day.Wednesday:
                return "Среда";
            case Day.Thursday:
                return "Четверг";
            case Day.Friday:
                return "Пятница";
            case Day.Saturday:
                return "Суббота";
            case Day.Sunday:
                return "Воскресенье";
            default:
                return "Неизвестно";
        }
    }

    /// <summary>
    /// Подсчет количества дней до события через неделю
    /// </summary>
    public static int DaysBefore(this Day today, Day date) => (int)date < (int)today ? 7 - (today - date) : date - today;

    public static bool IsWeekDay(this Day today) => (int)today < (int)Day.Saturday;

}
