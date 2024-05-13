using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour, Item
{
    [SerializeField] private Transform lift;
    [SerializeField] private Transform player;
    [SerializeField] private float yPosTop;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void pickUp()
    {
        if (lift.position.y < yPosTop)
        {
            player.GetComponent<FPSController>().CantMove(false);
            lift.position += Vector3.up * Time.deltaTime * speed;
            player.position += Vector3.up * Time.deltaTime * speed;
        }
            //lift.position = Vector3.Lerp(lift.position, lift.position + Vector3.up * Time.deltaTime * speed, 0.15f);
        else
            HUD.instance.transform.GetComponent<Animator>().SetTrigger("FadeOut");
    }
}
