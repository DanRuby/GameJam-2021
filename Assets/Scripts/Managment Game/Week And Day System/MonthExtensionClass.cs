public static class MonthExtensionClass
{
    public static string ToRussianString(this Month month)
    {
        switch (month)
        {
            case Month.January:
                return "������";
            case Month.February:
                return "�������";
            case Month.March:
                return "����";
            case Month.April:
                return "������";
            case Month.May:
                return "���";
            case Month.June:
                return "����";
            case Month.July:
                return "����";
            case Month.August:
                return "������";
            case Month.September:
                return "��������";
            case Month.October:
                return "�������";
            case Month.November:
                return "������";
            case Month.December:
                return "�������";
            default:
                return "����������";
        }
    }
}