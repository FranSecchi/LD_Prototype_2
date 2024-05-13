using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDoor : MonoBehaviour
{
    private bool move = false;
    [SerializeField] private float speed;
    [SerializeField] private float topy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (move && transform.position.y < topy)
        {
            transform.position += Vector3.up * Time.deltaTime * speed;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player"))
            move = true;
    }

}
