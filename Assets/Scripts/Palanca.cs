using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palanca : MonoBehaviour, Item
{
    [SerializeField] private Transform bridge;
    [SerializeField] private float yPosTop;
    [SerializeField] private float speed;
    public void pickUp()
    {
        if(bridge.position.y > yPosTop)
            bridge.position += Vector3.down * Time.deltaTime * speed;
    }
}
