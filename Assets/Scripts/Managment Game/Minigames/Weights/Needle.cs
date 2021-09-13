using UnityEngine;

public static class MathAddition
{
    public static float Map(float x, float in_min, float in_max, float out_min, float out_max) =>
            (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
}

/// <summary>
/// Class that changes needle angle based on picked weight
/// </summary>
public class Needle : MonoBehaviour
{
    [SerializeField] private Transform needleRotationPoint;

    //This parameters are set based on a weights model
    private const int MAX_GRAMMS = 5000;
    private const int MIN_GRAMMS = 0;
    private const int MAX_ANGLE = 0;
    private const int MIN_ANGLE = 360;

    private float targetAngle=0;//angle that needle should point at
    private float currentAngle=0;

    void Update()
    {
       currentAngle = Mathf.Lerp(currentAngle, targetAngle, Time.deltaTime);
       transform.RotateAround(needleRotationPoint.position, transform.up, (targetAngle-currentAngle)*Time.deltaTime);
    }

    /// <summary>
    /// Set new weight for display
    /// </summary>
    /// <param name="grams"></param>
    public void SetNewWeight(int grams) => targetAngle = MathAddition.Map(grams, MIN_GRAMMS, MAX_GRAMMS, MIN_ANGLE, MAX_ANGLE);
}
