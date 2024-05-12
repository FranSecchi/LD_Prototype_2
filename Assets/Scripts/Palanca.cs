using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palanca : MonoBehaviour, Item
{
    [SerializeField] private Transform bridge;
    [SerializeField] private float yPosTop;
    [SerializeField] private float speed;
    private Vector3 center;
    public void pickUp()
    {
        if(bridge.position.y > yPosTop)
            bridge.position += Vector3.down * Time.deltaTime * speed;
        //Mesh mesh = GetComponent<MeshFilter>().mesh;
        //center = mesh.bounds.center;
        //RotatePalanca();
    }

    //private void RotatePalanca()
    //{
    //    Vector3 pivotOffset = palanca.position - center;
    //    Vector3 rotationAxis = Vector3.Cross(pivotOffset.normalized, Vector3.up);

    //    // Rotate the palanca around the calculated axis
    //    palanca.Rotate(rotationAxis, speed * Time.deltaTime);
    //}
}
