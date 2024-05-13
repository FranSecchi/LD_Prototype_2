using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextWriter : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    [SerializeField] private string textToWrite;

    [SerializeField] private string textToWrite2;

    [SerializeField] private float timePerCharacter;

    [SerializeField] private int sceneToLoad;

    private float timer;

    private int characterIndex;

    string currentText;

    bool firstTextFinished;

    // Start is called before the first frame update
    void Start()
    {

        firstTextFinished = false;

        characterIndex = 0;

        currentText = textToWrite;

    }

    // Update is called once per frame
    void Update()
    {

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer += timePerCharacter;
            characterIndex++;
            text.text = currentText.Substring(0, characterIndex);

            if (characterIndex >= currentText.Length && firstTextFinished == false)
            {
                characterIndex = 0;
                currentText = textToWrite2;
                firstTextFinished = true;
            }
            else if (characterIndex >= currentText.Length && firstTextFinished == true)
            {
                SceneManager.LoadScene(sceneToLoad);
            }

        }


    }
}
