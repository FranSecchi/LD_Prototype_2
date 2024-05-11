using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour, Item
{
    [SerializeField] private Transform hand;
    [SerializeField] private Shooting shooting;
    public void pickUp()
    {
        transform.parent = hand;
        transform.localPosition = Vector3.zero;
        transform.rotation = hand.rotation;
        shooting.enabled = true;
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
