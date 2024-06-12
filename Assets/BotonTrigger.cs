using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonTrigger : MonoBehaviour, Item
{

    [SerializeField] private Animator caustor;
    [SerializeField] private String paustor;
    public void pickUp()
    {
        caustor.SetBool(paustor, true);
    }
}
