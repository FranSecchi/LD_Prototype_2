using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candau : MonoBehaviour, IDamageable
{
    public Animator anim;
    public void Die()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(float amount, Transform actor)
    {
        anim.SetTrigger("Open");
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        other.transform.GetComponent<FPSController>().CantMove(false);
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
