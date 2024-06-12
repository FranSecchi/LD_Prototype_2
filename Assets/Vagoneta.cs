using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vagoneta : MonoBehaviour, Item
{
    // Start is called before the first frame update
    [SerializeField]private Animator animator;

    public void pickUp()
    {
        animator.speed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        animator.speed = 0;
    }
}
