using UnityEngine;
using TMPro;

/// <summary>
/// Миниигра по указыванию веса
/// </summary>
public class WeightsManager : MiniGameManager
{
    [SerializeField]
    private TextMeshProUGUI measurmentsMesh;

    [SerializeField]
    private TMP_InputField inputField;

    [SerializeField]
    private Needle needle;

    private const int MAX_MEASUREMENTS = 8;
    private const int MIN_MEASUREMENTS = 4;
    private const int SECONDS_PER_MEASUREMENT = 10;
    private int numMeasurements;
    private int currentMeasurement;

    private int[] correctValues;
    private int[] playerValues;

    private Timer timer;

    private void Awake()
    {
        inputField.onEndEdit.AddListener(AddPlayerMeasurement);

        currentMeasurement = 0;
        numMeasurements = Random.Range(MIN_MEASUREMENTS, MAX_MEASUREMENTS + 1);
        playerValues = new int[numMeasurements];
        correctValues = new int[numMeasurements];
        for (int i = 0; i < numMeasurements; i++)
            correctValues[i] = Random.Range(100, 5000);

        timer = new Timer(SECONDS_PER_MEASUREMENT * numMeasurements);
        Timer.TimerRanOut += CalculateResults;
    }

    private void OnDestroy() => Timer.TimerRanOut -= CalculateResults;

    private void OnEnable()
    {
        measurmentsMesh.text = (numMeasurements - currentMeasurement).ToString();

        needle.SetNewWeight(correctValues[currentMeasurement]);

        inputField.Select();
        inputField.ActivateInputField();
    }

    private void Update() => timer.Tick();

    public void AddPlayerMeasurement(string grammsString)
    {
        try
        {           
            playerValues[currentMeasurement] = System.Convert.ToInt32(grammsString);
            currentMeasurement++;
            if (currentMeasurement == numMeasurements)
                CalculateResults();
            else if(currentMeasurement<numMeasurements)
            {
                needle.SetNewWeight(correctValues[currentMeasurement]);
                inputField.Select();
                inputField.ActivateInputField();
            }
            measurmentsMesh.text = (numMeasurements - currentMeasurement).ToString();
        }
        catch (System.FormatException)
        {
            Debug.Log("Строка состоит не из цифр");
        }
    }

    public void CalculateResults()
    {
        float result = 0;
        float maxResPerMeasurment = 1.0f / numMeasurements;

        for (int i = 0; i < numMeasurements; i++)
        {
            float perc = (float)correctValues[i] / 100;
            float error = Mathf.Abs(playerValues[i] - correctValues[i]);
            float resultPerc = 100.0f - (error / perc);
            result += MathAddition.Map(resultPerc, 0, 100, 0, maxResPerMeasurment);
        }

        MiniGameEnded?.Invoke(result);

        inputField.gameObject.SetActive(false);
        this.enabled = false;
    }
}
