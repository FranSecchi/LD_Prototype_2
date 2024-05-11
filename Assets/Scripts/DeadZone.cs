using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        GameObject go = collision.gameObject;
        IDamageable damagable = go.GetComponent<IDamageable>();
        if (damagable != null)
        {
            damagable.Die();
        }
    }
}
