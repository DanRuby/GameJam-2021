public static class DayExtension 
{
    public static string ToRussianString(this Day day)
    {
        switch(day)
        {
            case Day.Monday:
                return "�����������";
            case Day.Tuesday:
                return "�������";
            case Day.Wednesday:
                return "�����";
            case Day.Thursday:
                return "�������";
            case Day.Friday:
                return "�������";
            case Day.Saturday:
                return "�������";
            case Day.Sunday:
                return "�����������";
            default:
                return "����������";
        }
    }

    /// <summary>
    /// ������� ���������� ���� �� ������� ����� ������
    /// </summary>
    public static int DaysBefore(this Day today, Day date) => (int)date < (int)today ? 7 - (today - date) : date - today;

    public static bool IsWeekDay(this Day today) => (int)today < (int)Day.Saturday;

}
