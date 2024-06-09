using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerText : MonoBehaviour
{
    [SerializeField] private PlayerTextWriter writer;
    [SerializeField] private string textToWrite;
    [SerializeField] private float timePerCharacter;
    [SerializeField] private bool destroy = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            writer.WriteText(textToWrite, timePerCharacter);
            if (destroy) Destroy(gameObject);
        }
    }
}
