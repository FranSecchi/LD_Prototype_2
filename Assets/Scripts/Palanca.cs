using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palanca : MonoBehaviour, Item
{
    [SerializeField] private Transform bridge;
    [SerializeField] private float yPosTop;
    [SerializeField] private float speed;
    [SerializeField] private float mult;
    [SerializeField] private Transform manivela;
    public void pickUp()
    {
        if (bridge.position.y > yPosTop)
            RotatePalanca();
        else bridge.GetComponent<Bridge>().enabled = true;
        //Mesh mesh = GetComponent<MeshFilter>().mesh;
        //center = mesh.bounds.center;
    }

    private void RotatePalanca()
    {

            bridge.position += Vector3.down * Time.deltaTime * speed;
        // Rotate the palanca around the calculated axis
        manivela.Rotate(Vector3.up, speed * mult * Time.deltaTime);
    }
}
