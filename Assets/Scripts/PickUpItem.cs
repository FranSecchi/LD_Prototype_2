using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] private Transform pitch;
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask itemLayer;
    [SerializeField] private KeyCode key;
    private RaycastHit r;
    private void Update()
    {
        if (Input.GetKey(key))
        {
            if(Physics.Raycast(pitch.position, pitch.forward, out r, maxDistance, itemLayer))
            {
                PickUp(r.transform.GetComponent<Item>());
            }
        }
    }

    private void PickUp(Item item)
    {
        item.pickUp();
    }
}
