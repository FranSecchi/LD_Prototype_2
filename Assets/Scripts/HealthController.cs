using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour, IDamageable
{
    [SerializeField] private Transform checkPoint;

    public Transform CheckPoint { get => checkPoint; set => checkPoint = value; }

    public void Die()
    {
        transform.position = checkPoint.position;
        GetComponent<FPSController>().Die();
    }
    public void TakeDamage(float amount, Vector3 hitPoint)
    {
        Die();
    }
}
