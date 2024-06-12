using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyRock : MonoBehaviour
{
    [SerializeField]private GameObject destruible;
    [SerializeField] private GameObject destroyOther;

    void explode()
    {
        Destroy(destruible);
        if (destroyOther != null)
        {
            Destroy(destroyOther);
        }
    }
}
