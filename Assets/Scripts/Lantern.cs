using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour, Item
{
    [SerializeField] private GameObject spawner;
    [SerializeField] private Transform pitch;
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject light;
    [SerializeField] private bool start;
    public void pickUp()
    {
        transform.parent = pitch;
        transform.localPosition = Vector3.zero;
        transform.rotation = pitch.rotation;
        light.SetActive(true);
        if(spawner != null)spawner.SetActive(true);
        Destroy(model);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (start) pickUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
