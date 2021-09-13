using UnityEngine;
using TMPro;

/// <summary>
/// Weights minigame controller
/// </summary>
public class WeightsMiniGame : MiniGameManager
{
    [SerializeField] private TextMeshProUGUI measurmentsMesh;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Needle needle;

    [Header("Minigame Params")]
    [SerializeField] private int maxMeasurements = 8;
    [SerializeField] private int minMeasurements = 4;
    
    [Tooltip("How much time player has to input displayed weight")]
    [SerializeField] private int secondsPerMeasurement = 10;
    
    private int numMeasurements;
    private int currentMeasurement;
    private int[] correctValues;
    private int[] playerValues;
    private Timer timer;

    private void Awake()
    {
        inputField.onEndEdit.AddListener(AddPlayerMeasurement);
        CreateMeasurementValues();
        SetTimer();
    }
    
    private void OnDestroy() => Timer.TimerRanOut -= CalculateResults;
    
    private void Update() => timer.Tick();
    
    private void Start() => StartMinigame();

    
    //Add player input in array
    private void AddPlayerMeasurement(string grammsString)
    {
        try
        {           
            playerValues[currentMeasurement] = System.Convert.ToInt32(grammsString);
            
            currentMeasurement++;
            if (currentMeasurement == numMeasurements)
                CalculateResults();
            else if(currentMeasurement<numMeasurements)
            {
                //Set new measurement
                needle.SetNewWeight(correctValues[currentMeasurement]);
                inputField.Select();
                inputField.ActivateInputField();
            }
            measurmentsMesh.text = (numMeasurements - currentMeasurement).ToString();
        }
        catch (System.FormatException)
        {
            Debug.Log("String consists of not only numbers");
        }
    }
    
    //Create array to store player values and randomise weights
    private void CreateMeasurementValues()
    {
        currentMeasurement = 0;
        numMeasurements = Random.Range(minMeasurements, maxMeasurements + 1);
        playerValues = new int[numMeasurements];
        correctValues = new int[numMeasurements];
        for (int i = 0; i < numMeasurements; i++)
            correctValues[i] = Random.Range(100, 5000);
    }
    
    private void SetTimer()
    {
        timer = new Timer(secondsPerMeasurement * numMeasurements);
        Timer.TimerRanOut += CalculateResults;
    }

    private void StartMinigame()
    {
        measurmentsMesh.text = (numMeasurements - currentMeasurement).ToString();
        needle.SetNewWeight(correctValues[currentMeasurement]);
        inputField.Select();
        inputField.ActivateInputField();
    }

    private void CalculateResults()
    {
        float result = 0;
        float maxResPerMeasurment = 1.0f / numMeasurements;
        
        /*Result is described as( (100% - percentage error in measurement) * 1/num of measurements )
         *Result is a float in range [0 1]*/
        for (int i = 0; i < numMeasurements; i++)
        {
            float perc = (float)correctValues[i] / 100;
            float error = Mathf.Abs(playerValues[i] - correctValues[i]);
            float resultPerc = 100.0f - (error / perc);
            result += MathAddition.Map(resultPerc, 0, 100, 0, maxResPerMeasurment);
        }

        MiniGameEnded?.Invoke(result);

        inputField.gameObject.SetActive(false);
        enabled = false;
    }
}
