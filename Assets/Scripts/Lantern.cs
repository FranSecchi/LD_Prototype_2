using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : Item
{
    [SerializeField] private Transform pitch;
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject light;
    public override void pickUp()
    {
        transform.parent = pitch;
        transform.localPosition = Vector3.zero;
        transform.rotation = pitch.rotation;
        light.SetActive(true);
        Destroy(model);
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
