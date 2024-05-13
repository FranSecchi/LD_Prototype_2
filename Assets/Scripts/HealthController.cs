using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour, IDamageable
{
    [SerializeField] private Transform checkPoint;
    public float maxHP;
    private float currentHP;
    public Transform CheckPoint { get => checkPoint; set => checkPoint = value; }
    public float CurrentHP { get => currentHP; set => currentHP = value; }

    private void Awake()
    {
        currentHP = maxHP;
    }
    public void Die()
    {
        transform.position = checkPoint.position;
        currentHP = maxHP;
        GetComponent<FPSController>().Die();
    }
    public void TakeDamage(float amount, Transform actor)
    {
        currentHP -= amount;
        if(currentHP <= 0) Die();
        if(currentHP / maxHP < 0.4f)
        {
            float normalizedHP = currentHP / maxHP;
            BlurEffect blurController = FindObjectOfType<BlurEffect>();
            if (blurController != null)
            {
                blurController.SetBlurIntensity(normalizedHP);
            }
        }
        if (actor == null)
            GetComponent<FPSController>().Slow();
    }
}
