public static class DepartmentExtension 
{
    public static string ToRussianString(this Department department)
    {
        switch(department)
        {
            case Department.SAPR:
                return "САПР";
            case Department.VT:
                return "ВТ";
            case Department.Physics:
                return "Физики";
            case Department.IS:
                return "ИБ";
            default: return "Неизвестно";
        }
    }
}
