using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterfall : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        GetComponent<Collider>().isTrigger = false;
    }
}
