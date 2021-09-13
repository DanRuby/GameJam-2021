using UnityEngine;
using TMPro;
using System;

/// <summary>
/// Class that moves a word down and checks if it became empty
/// </summary>
public class Word : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;

    private readonly Color NORMAL_COLOR=Color.white;
    private readonly Color ACTIVE_COLOR = Color.red;
    private const float moveSpeed = 50.0f;
    private const float destroyCoordinate = 0;

    public static event Action<Word> WordDestroyed;

    public int LettersLeft => textMesh.text.Length;

    /// <summary>
    /// Sets new text as a word 
    /// </summary>
    /// <param name="text"></param>
    public void SetNewText(string text)
    {
        textMesh.text = text;
        textMesh.color = NORMAL_COLOR;
    }

    public void ChangeColorToActive() => textMesh.color = ACTIVE_COLOR;

    /// <summary>
    /// Check if pressed button and first letter are the same 
    /// </summary>
    /// <param name="letter"></param>
    public void HandleInput(char letter)
    {
        if (LettersLeft > 0)
        {
            if (textMesh.text[0] == letter)
            {
                textMesh.text = textMesh.text.Remove(0, 1);
                if (LettersLeft == 0)
                    WordDestroyed?.Invoke(this);
            }
        }
    }

    void Update()
    {
        transform.Translate(0, -Time.deltaTime * moveSpeed, 0);

        if (transform.position.y <= destroyCoordinate)
            WordDestroyed?.Invoke(this);
    }
}
