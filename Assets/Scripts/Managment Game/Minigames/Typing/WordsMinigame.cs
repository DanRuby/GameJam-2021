using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Миниигра на напечатывание слов
/// </summary>
public class WordsMinigame : MiniGameManager
{
    [SerializeField]
    [Tooltip("Префаб для спауна")]
    private WordDisplayer wordPrefab;

    private string[] wordTextsPool = { "int","bool","string", "float", "double", "short", "byte", "MonoBehaviour", "UnityEngine", "using", "Input", "Update", "Start", "Awake", "delegate", "Action", "event",
    "Func", "Predicate", "ToString", "public", "private", "protected", "void", "deltaTime", "fixedDeltaTime", "GetAxis", "interface", "Button", "GameObject", "Transform", "Vector3", "glDrawElements",
    "GL_TRIANGLES", "glBindBuffer", "class", "interface", "struct", "null", "Sprite", "TextMeshProUGUI", "SerializeField", "SerializeReference", "SetActive", "Serializable", "Vector2","KeyCode",
    "main", "Generic", "List", "Length","System"};

    private int numOfWordsSpawned;
    private int maxNumOfWords;

    private int maxScore;
    private int score=0;
    private const int ScorePerWord=100;

    private GenericPool<WordDisplayer> wordsPool;
    private const int initialPool = 10;
    private WordDisplayer activeInputWord;
    private List<WordDisplayer> spawnedWords;

    private const float timeBetweenSpawns = 2f;
    private float timer=0;

    private void Awake()
    {
        spawnedWords = new List<WordDisplayer>();
        wordsPool = new GenericPool<WordDisplayer>(initialPool, wordPrefab);

        maxNumOfWords = Random.Range((int)(wordTextsPool.Length*.75f),wordTextsPool.Length);
        maxScore = maxNumOfWords * ScorePerWord;

        WordDisplayer.WordDestroyed += OnWordDestroyed;
    }
    private void OnDestroy() => WordDisplayer.WordDestroyed -= OnWordDestroyed;

    private void SpawnNewWord()
    {
        numOfWordsSpawned++;
        string text=wordTextsPool[Random.Range(0, wordTextsPool.Length)];
        WordDisplayer newWord=wordsPool.GetObject();
        
        newWord.SetNewText(text);
        newWord.transform.SetParent(transform);
        newWord.transform.position = new Vector2(Random.Range(200, 1701), 1000);
        newWord.gameObject.SetActive(true);

        spawnedWords.Add(newWord);
        activeInputWord = spawnedWords[0];
        activeInputWord.ChangeColorToActive();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenSpawns && 
            numOfWordsSpawned < maxNumOfWords)
        {
            SpawnNewWord();
            timer = 0;
        }

        foreach (char letter in Input.inputString)
        {
            if (activeInputWord)
                activeInputWord.HandleInput(letter);
        }
    }

    /// <summary>
    /// Хендлинг уничтожения слова
    /// </summary>
    /// <param name="wordDisplayer"></param>
    private void OnWordDestroyed(WordDisplayer wordDisplayer)
    {
        wordDisplayer.gameObject.SetActive(false);

        spawnedWords.Remove(wordDisplayer);
        if (spawnedWords.Count != 0)
        {
            activeInputWord = spawnedWords[0];
            activeInputWord.ChangeColorToActive();
        }
        else activeInputWord = null;

        if (wordDisplayer.LettersLeft == 0)
            score += ScorePerWord;
        wordsPool.ReturnObject(wordDisplayer);

        if (numOfWordsSpawned == maxNumOfWords && spawnedWords.Count == 0)
            MiniGameEnded?.Invoke((float)score/maxScore);
    }
}
