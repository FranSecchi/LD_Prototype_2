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
    public float shootRange = 0.0f;
    public float meleeRange = 0.0f;
    public int magazine = 0;
    public int maxAmmo = 0;
    public TextMeshProUGUI ammoTxt;
    public KeyCode fireKey;
    public KeyCode reloadKey;

    private int ammo = 0;
    private float myTime = 0.0f;
    private float nextFire = 0.5F;
    private bool firePressedThisFrame = false;

    public int Ammo { get => ammo; set => ammo = value; }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(camera.transform.position, camera.transform.position + camera.transform.forward * shootRange);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(camera.transform.position, camera.transform.position + camera.transform.forward * meleeRange);
    }
    private void Start()
    {
        ammo = magazine;
        ammoTxt.text = $"{ammo}/{magazine} | {maxAmmo}";
    }
    private void OnEnable()
    {
        ammoTxt.transform.parent.gameObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (ammo > magazine) ammo = magazine;
        if(myTime < fireRate) myTime += Time.deltaTime;

        if(!firePressedThisFrame && myTime >= fireRate && Input.GetKeyDown(fireKey))
        {
            Shoot();
            myTime = 0f;
            firePressedThisFrame = true; // Set flag to true to indicate fire key has been pressed
        }

        // Check if fire key is released
        if (Input.GetKeyUp(fireKey))
        {
            firePressedThisFrame = false; // Reset flag when fire key is released
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
        float distance = ammo > 0 ? Mathf.Infinity : meleeRange;
        anim.SetTrigger(ammo > 0 ? "Shoot" : "Hit");
        // Perform the raycast
        if (Physics.Raycast(ray, out hit, distance))
        {
            //Instantiate(decal, hit.point + (hit.normal * 0.01f), Quaternion.LookRotation(hit.normal), hit.collider.gameObject.transform);
            IDamageable damageable = hit.collider.gameObject.GetComponent<IDamageable>();
            // Hit object
            if(damageable != null)
            {
                damageable.TakeDamage(damage, transform);
            }

        }
        if(ammo > 0)
            ammo--;
    }
}
