using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generador : MonoBehaviour, Item
{
    [SerializeField] private GameObject lights;
    private bool used = false;
    public void pickUp()
    {
        if (used) return;
        used = true;
        lights.SetActive(true);
    }
}
