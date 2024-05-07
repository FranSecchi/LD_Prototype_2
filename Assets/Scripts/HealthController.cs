using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour, IDamageable
{
    public GameObject deadScreen;
    public float MaxHP;
    public float MaxShield;

    [SerializeField]
    private float mHp;
    [SerializeField]
    private float mShield;

    public float MHp { get => mHp; set => mHp = value; }
    public float MShield { get => mShield; set => mShield = value; }

    // Start is called before the first frame update
    void Start()
    {
        deadScreen.SetActive(false);
        mHp = MaxHP;
        mShield = MaxShield;
    }
    private void Update()
    {
        if (mHp > MaxHP) mHp = MaxHP;
        if (mShield > MaxShield) mShield = MaxShield;
    }
    public void TakeDamage(float amount, Vector3 hitPoint)
    {
        // If shield can take 75% of the damage
        if(mShield > amount * 0.75f)
        {
            mShield -= amount * 0.75f;
            mHp -= amount * 0.25f;
        }
        else // Else, take what it can and set it to 0
        {
            mHp -= amount - mShield;
            mShield = 0;
        }
        // Check negative values
        mShield = mShield <= 0 ? 0f : mShield;
        if (mHp <= 0)
            Die();
    }

    public void Die()
    {
        deadScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Destroy(GetComponent<Shooting>());
        Destroy(GetComponent<FPSController>());
    }
}
