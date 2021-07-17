using System;

public enum Department
{
    VT, SAPR, Physics, IS 
}

public static class LabWork
{
    #region Статы
    public const int MAX_STATS_VALUE=100;

    /// <summary>
    /// Оригинальность
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
    /// Завершенность
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
    /// Правильность
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

    #region События об изменениях статов
    public static Action<int> OriginalityValueChanged;
    public static Action<int> ComplitnessValueChanged;
    public static Action<int> CorrectnessValueChanged;
    #endregion

    public static void ChangeValues(LabStats changes)
    {
        
        Correctness += changes.Correctness;
        Complitness += changes.Complitness;
        Originality += changes.Originality;
    }
}
