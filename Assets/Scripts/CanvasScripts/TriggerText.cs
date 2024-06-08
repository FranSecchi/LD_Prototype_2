using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerText : MonoBehaviour
{
    [SerializeField] private PlayerTextWriter writer;
    [SerializeField] private string textToWrite;
    [SerializeField] private bool destroy;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            writer.WriteText(textToWrite);
            if (destroy) Destroy(gameObject);
        }
    }
}
