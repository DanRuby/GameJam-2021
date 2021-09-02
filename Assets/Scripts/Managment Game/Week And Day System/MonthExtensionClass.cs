public static class MonthExtensionClass
{
    public static string ToRussianString(this Month month)
    {
        switch (month)
        {
            case Month.January:
                return "Январь";
            case Month.February:
                return "Февраль";
            case Month.March:
                return "Март";
            case Month.April:
                return "Апрель";
            case Month.May:
                return "Май";
            case Month.June:
                return "Июнь";
            case Month.July:
                return "Июль";
            case Month.August:
                return "Август";
            case Month.September:
                return "Сентябрь";
            case Month.October:
                return "Октябрь";
            case Month.November:
                return "Ноябрь";
            case Month.December:
                return "Декабрь";
            default:
                return "Неизвестно";
        }
    }
}