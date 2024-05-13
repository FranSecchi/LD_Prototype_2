using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{

    [SerializeField] private Transform standPoint;
    [SerializeField] private Transform player;
    [SerializeField] private Collider deadzone;
    [SerializeField] private float radius;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(standPoint.position, radius);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.position, standPoint.position) < radius)
        {
            //player.GetComponent<FPSController>().enabled = false;
            deadzone.enabled = false;
            HUD.instance.transform.GetComponent<Animator>().SetTrigger("FadeOut");
            Destroy(gameObject);
        }
    }
}
