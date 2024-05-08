using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Shooting : MonoBehaviour
{
    public Animator anim;
    public Camera camera;
    //public GameObject decal;
    public float damage = 0.0f;
    public float fireRate = 0.0f;
    public float maxDistance = 0.0f;
    public int magazine = 0;
    public int maxAmmo = 0;
    public TextMeshProUGUI ammoTxt;
    public KeyCode fireKey;
    public KeyCode reloadKey;

    private int ammo = 0;
    private float myTime = 0.0f;
    private float nextFire = 0.5F;

    public int Ammo { get => ammo; set => ammo = value; }

    private void Start()
    {
        ammo = magazine;
        ammoTxt.text = $"{ammo}/{magazine} | {maxAmmo}";
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (ammo > magazine) ammo = magazine;
        myTime += Time.fixedDeltaTime;

        if(ammo > 0 && myTime >= fireRate && Input.GetKeyDown(fireKey))
        {
            Shoot();
            myTime = 0f;
        }
        if (Input.GetKeyDown(reloadKey))
            Reload();
        ammoTxt.text = $"{ammo}/{magazine} | {maxAmmo}";
    }

    private void Reload()
    {
        if (ammo == magazine) return;
        ammo = (maxAmmo > magazine) ? ammo = magazine : ammo = maxAmmo;
        maxAmmo -= ammo;
        //anim.SetTrigger("Reload");
    }

    private void Shoot()
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        // Perform the raycast
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            //Instantiate(decal, hit.point + (hit.normal * 0.01f), Quaternion.LookRotation(hit.normal), hit.collider.gameObject.transform);
            IDamageable damageable = hit.collider.gameObject.GetComponent<IDamageable>();
            // Hit object
            if(damageable != null)
            {
                damageable.TakeDamage(damage, hit.point);
            }

        }
        else
        {
        }

        ammo--;
    }
}
