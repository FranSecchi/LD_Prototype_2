using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appear : MonoBehaviour, Item
{
    public Transform bitxo;
    public float time;

    public void pickUp()
    {
        bitxo.gameObject.SetActive(true);
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(time);
        HUD.instance.transform.GetComponent<Animator>().SetTrigger("FadeOut");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
