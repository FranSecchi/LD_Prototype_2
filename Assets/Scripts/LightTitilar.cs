using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTitilar : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private Light light;
    private float elapsed = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if(elapsed > time)
        {
            elapsed = 0f;
            light.enabled = !light.isActiveAndEnabled;
        }
    }
}
