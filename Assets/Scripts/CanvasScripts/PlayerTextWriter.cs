using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerTextWriter : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private float timePerCharacter;
    private float timer = 0f;

    private int characterIndex;

    string currentText;

    // Start is called before the first frame update
    void Start()
    {

        characterIndex = 0;

        currentText = "";
        text.text = currentText;
    }
    public void WriteText(string text)
    {
        StopAllCoroutines();
        currentText = text;
        characterIndex = 0;
        StartCoroutine(Write());
    }
    private IEnumerator Write()
    {
        while (characterIndex < currentText.Length)
        {
            timer += Time.deltaTime;
            if (timer >= timePerCharacter)
            {
                characterIndex++;
                text.text = currentText.Substring(0, characterIndex);
                timer = 0f;
            }
            yield return null;
        }
        characterIndex = 0;
    }
}
