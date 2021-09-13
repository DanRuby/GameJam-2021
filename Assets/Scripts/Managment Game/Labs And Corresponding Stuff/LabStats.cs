/// <summary>
/// Structure that stores lab parameters
/// </summary>
[System.Serializable]
public struct LabStats
{
    public int Originality;
    public int Complitness;
    public int Correctness;

    public LabStats(int originality, int complitness, int correctness)
    {
        Originality = originality;
        Complitness = complitness;
        Correctness = correctness;
    }

    public static LabStats operator *(LabStats a, float value) => new LabStats((int)(a.Originality * value), 
        (int)(a.Complitness * value), (int)(a.Correctness * value));

    public static LabStats operator +(LabStats a, LabStats b) => new LabStats(a.Originality + b.Originality, 
        a.Complitness + b.Complitness, a.Correctness + b.Complitness);

    public bool HasValues() => Originality != 0 || Complitness != 0 || Correctness != 0;

    public string GetString()
    {
        string res = string.Empty;
        if (Originality != 0)
            res += $"{Originality} �������������� ";
        if (Complitness != 0)
            res += $"{Complitness} ������������� ";
        if (Correctness != 0)
            res += $"{Correctness} ������������";
        return res;
    }
}