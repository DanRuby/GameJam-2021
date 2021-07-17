public static class DepartmentExtension 
{
    public static string ToRussianString(this Department department)
    {
        switch(department)
        {
            case Department.SAPR:
                return "����";
            case Department.VT:
                return "��";
            case Department.Physics:
                return "������";
            case Department.IS:
                return "��";
            default: return "����������";
        }
    }
}
