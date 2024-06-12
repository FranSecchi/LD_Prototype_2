using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour, Item
{
    [SerializeField] private Transform lift;
    [SerializeField] private Transform player;
    [SerializeField] private float yPosTop;
    [SerializeField] private float speed;
    private bool used = false;
    private bool moving = false;
    public void pickUp()
    {
        if (used) return;
        used = true;
        moving = true;
        player.GetComponent<FPSController>().CantMove(false);
            //lift.position = Vector3.Lerp(lift.position, lift.position + Vector3.up * Time.deltaTime * speed, 0.15f);
        //else
        //    HUD.instance.transform.GetComponent<Animator>().SetTrigger("FadeOut");
    }
    private void Update()
    {
        if (moving)
        {
            if (lift.position.y < yPosTop)
            {
                lift.position += Vector3.up * Time.deltaTime * speed;
                player.position += Vector3.up * Time.deltaTime * speed;
            }
            else
            {
                player.GetComponent<FPSController>().CantMove(true);
                moving = false;
            }
        }
    }
}
