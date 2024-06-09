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
    public void pickUp()
    {
        if (used) return;
        used = true;
        StartCoroutine(Move());
            //lift.position = Vector3.Lerp(lift.position, lift.position + Vector3.up * Time.deltaTime * speed, 0.15f);
        //else
        //    HUD.instance.transform.GetComponent<Animator>().SetTrigger("FadeOut");
    }
    private IEnumerator Move()
    {
        while (lift.position.y < yPosTop)
        {
            player.GetComponent<FPSController>().CantMove(false);
            lift.position += Vector3.up * Time.deltaTime * speed;
            player.position += Vector3.up * Time.deltaTime * speed;
            yield return null;
        }
        player.GetComponent<FPSController>().CantMove(true);
    }
}
