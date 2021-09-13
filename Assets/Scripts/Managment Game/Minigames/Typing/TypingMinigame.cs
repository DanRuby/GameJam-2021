using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Typing Minigame
/// </summary>
public class TypingMinigame : MiniGameManager
{
    [Tooltip("Word prefab")]
    [SerializeField] private Word wordPrefab;

    private string[] wordTextsPool = { "int","bool","string", "float", "double", "short", "byte", "MonoBehaviour", "UnityEngine", "using", "Input", "Update", "Start", "Awake", "delegate", "Action", "event",
    "Func", "Predicate", "ToString", "public", "private", "protected", "void", "deltaTime", "fixedDeltaTime", "GetAxis", "interface", "Button", "GameObject", "Transform", "Vector3", "glDrawElements",
    "GL_TRIANGLES", "glBindBuffer", "class", "interface", "struct", "null", "Sprite", "TextMeshProUGUI", "SerializeField", "SerializeReference", "SetActive", "Serializable", "Vector2","KeyCode",
    "main", "Generic", "List", "Length","System"};

    private int numOfWordsSpawned;
    private int maxNumOfWords;

    private int maxScore;
    private int score=0;
    private const int ScorePerWord=100;

    private GenericPool<Word> wordsPool;
    private const int initialPool = 10;
    private Word activeInputWord;
    private List<Word> spawnedWords;

    private const float timeBetweenSpawns = 2f;
    private float timer=0;

    private void Awake()
    {
        spawnedWords = new List<Word>();
        wordsPool = new GenericPool<Word>(initialPool, wordPrefab);

        maxNumOfWords = Random.Range((int)(wordTextsPool.Length*.75f),wordTextsPool.Length);
        maxScore = maxNumOfWords * ScorePerWord;

        Word.WordDestroyed += OnWordDestroyed;
    }
    private void OnDestroy() => Word.WordDestroyed -= OnWordDestroyed;

    //Create new word
    private void SpawnNewWord()
    {
        numOfWordsSpawned++;
        string text=wordTextsPool[Random.Range(0, wordTextsPool.Length)];
        Word newWord=wordsPool.GetObject();
        
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
        
        if (timer >= timeBetweenSpawns && numOfWordsSpawned < maxNumOfWords)
        {
            SpawnNewWord();
            timer = 0;
        }

        HandleKeyboardInput();
    }

    
    private void HandleKeyboardInput()
    {
        // transfer all input to active word
        foreach (char letter in Input.inputString)
        {
            if (activeInputWord)
                activeInputWord.HandleInput(letter);
        }
    }

    /// <summary>
    /// Handles word destruction
    /// </summary>
    /// <param name="word"></param>
    private void OnWordDestroyed(Word word)
    {
        word.gameObject.SetActive(false);
        spawnedWords.Remove(word);
        
        //if where are words on screen, pick furthest one as an active one
        if (spawnedWords.Count != 0)
        {
            activeInputWord = spawnedWords[0];
            activeInputWord.ChangeColorToActive();
        }
        else activeInputWord = null;

        if (word.LettersLeft == 0)
            score += ScorePerWord;
        wordsPool.ReturnObject(word);

        //if destroyed word wal the last one, end minigame
        if (numOfWordsSpawned == maxNumOfWords && spawnedWords.Count == 0)
            MiniGameEnded?.Invoke((float)score/maxScore);
    }
}
