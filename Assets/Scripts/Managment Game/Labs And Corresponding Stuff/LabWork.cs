using System;

public enum Department
{
    VT, SAPR, Physics, IS 
}

public static class LabWork
{
    #region �����
    public const int MAX_STATS_VALUE=100;

    /// <summary>
    /// ��������������
    /// </summary>
    public static int Originality
    {
        get => originality;
        set
        {
            originality = Math.Min(Math.Max(value, 0), MAX_STATS_VALUE); ;
            OriginalityValueChanged?.Invoke(originality);
        }
    }
    private static int originality=0;

    /// <summary>
    /// �������������
    /// </summary>
    public static int Complitness
     {
        get => complitness;
        set
        {
            complitness = Math.Min(Math.Max(value, 0), MAX_STATS_VALUE); ;
            ComplitnessValueChanged?.Invoke(complitness);
}
    }
    private static int complitness=0;

    /// <summary>
    /// ������������
    /// </summary>
    public static int Correctness
    {
        get => correctness;
        set
        {
            correctness = Math.Min(Math.Max(value,0),MAX_STATS_VALUE);
            CorrectnessValueChanged?.Invoke(correctness);
        }
    }
    private static int correctness=0;
    #endregion

    #region ������� �� ���������� ������
    public static event Action<int> OriginalityValueChanged;
    public static event Action<int> ComplitnessValueChanged;
    public static event Action<int> CorrectnessValueChanged;
    #endregion

    public static void ChangeValues(LabStats changes)
    {
        
        Correctness += changes.Correctness;
        Complitness += changes.Complitness;
        Originality += changes.Originality;
    }
}
