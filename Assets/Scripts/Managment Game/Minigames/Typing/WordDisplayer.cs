using UnityEngine;
using TMPro;
using System;

/// <summary>
/// �����, ��������� ����� �� ������ 
/// </summary>
public class WordDisplayer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMesh;

    private readonly Color NORMAL_COLOR=Color.white;
    private readonly Color ACTIVE_COLOR = Color.red;
    private const float moveSpeed = 50.0f;
    private const float destroyCoordinate = 0;

    public static event Action<WordDisplayer> WordDestroyed;

    public int LettersLeft => textMesh.text.Length;

    /// <summary>
    /// ��������� ������ �����
    /// </summary>
    /// <param name="text"></param>
    public void SetNewText(string text)
    {
        textMesh.text = text;
        textMesh.color = NORMAL_COLOR;
    }

    public void ChangeColorToActive() => textMesh.color = ACTIVE_COLOR;

    /// <summary>
    /// �������� ������� �� ����������
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
