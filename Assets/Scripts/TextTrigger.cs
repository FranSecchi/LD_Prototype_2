using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextTrigger : MonoBehaviour
{

    public TextMeshProUGUI txt;
    public string text;

    private void OnTriggerEnter(Collider other)
    {
        txt.enabled = true;
        txt.text = text;
    }
    private void OnTriggerExit(Collider other)
    {
        txt.enabled = false;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
