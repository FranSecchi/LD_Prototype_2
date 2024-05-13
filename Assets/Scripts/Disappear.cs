using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : MonoBehaviour
{
    public Transform bridge;
    public Transform bitxo;

    private void OnTriggerEnter(Collider other)
    {
        if (bridge.position.y <= 2.24f)
            bitxo.gameObject.SetActive(false);
    }
}
