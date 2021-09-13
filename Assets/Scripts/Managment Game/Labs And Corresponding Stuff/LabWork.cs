using System;

public enum Department
{
    VT, SAPR, Physics, IS 
}

/// <summary>
/// Class that represents current lab that player is working on and will defend
/// </summary>
public static class LabWork
{
    #region Stats
    public const int MAX_STATS_VALUE=100;
    
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

    #region Stats changed events
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
