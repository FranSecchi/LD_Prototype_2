using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private float spawnRate;
    [SerializeField] private int spawnTimes;
    [SerializeField] private bool useTrigger = false;

    private int times = 0;
    private bool first = true;
    private void OnDrawGizmos()
    {
        Collider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            // Get the bounds of the BoxCollider
            Bounds bounds = boxCollider.bounds;

            // Draw the bounds using Gizmos
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(bounds.center, bounds.size);
        }
        foreach(Transform t in spawnPoints)
        {
            Gizmos.color = new Color(1f, 0f, 1f, 0.5f);
            Gizmos.DrawCube(t.position, Vector3.one * 0.5f);
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(t.position, Vector3.one * 0.5f);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!useTrigger)
        {
            Spawn();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (useTrigger)
        {
            if (other.transform.CompareTag("Player"))
            {
                Spawn();
            }
        }
    }

    internal void Spawn()
    {
        if (first)
        {

            foreach (Transform t in spawnPoints)
            {
                Instantiate(prefab, t.position, Quaternion.identity);
            }
            first = false;
        }
        if (times < spawnTimes)
            StartCoroutine(InstantiateEnemies());
    }

    private IEnumerator InstantiateEnemies()
    { 
        ++times;
        yield return new WaitForSeconds(spawnRate);
        foreach (Transform t in spawnPoints)
        {
            Instantiate(prefab, t.position, Quaternion.identity, transform);
        }
        Spawn();
    }
}
