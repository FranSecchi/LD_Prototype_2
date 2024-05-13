using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palanca : MonoBehaviour, Item
{
    [SerializeField] private Transform bridge;
    [SerializeField] private Transform bitxo;
    [SerializeField] private float yPosTop;
    [SerializeField] private float speed;
    [SerializeField] private float mult;
    [SerializeField] private Transform manivela;
    private float fov;
    public void pickUp()
    {
        if (bridge.position.y <= 2.24f)
            bitxo.gameObject.SetActive(true);
        if (bridge.position.y > yPosTop)
            RotatePalanca();
        else
        {
            Camera.main.fieldOfView = fov;
            bridge.GetComponent<Bridge>().enabled = true;
        }
        //center = mesh.bounds.center;
    }

    private void RotatePalanca()
    {
        fov = Camera.main.fieldOfView;
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 48.3f, 0.2f);
            bridge.position += Vector3.down * Time.deltaTime * speed;
        // Rotate the palanca around the calculated axis
        manivela.Rotate(Vector3.up, speed * mult * Time.deltaTime);
    }
}
