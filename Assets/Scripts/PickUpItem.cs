using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] private Transform pitch;
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask itemLayer;
    [SerializeField] private KeyCode key;
    [SerializeField] private KeyCode heal;
    [SerializeField] private KeyCode eat;
    public int vendas;
    public int menjar = 0;
    private RaycastHit r;
    private HUD hud;
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.green;
        Gizmos.DrawLine(pitch.transform.position, pitch.transform.position + pitch.transform.forward * maxDistance);
    }
    private void Start()
    {
        hud = HUD.instance;
    }
    private void Update()
    {
        if (Input.GetKey(key) || Input.GetKeyDown(key))
        {
            if(Physics.Raycast(pitch.position, pitch.forward, out r, maxDistance, itemLayer))
            {
                PickUp(r.transform.GetComponent<Item>());
            }
        }
        if (Input.GetKeyDown(heal) && vendas > 0)
        {
            Use(ConsumibleType.Health, 10f);
        }
        if (Input.GetKeyDown(eat) && menjar > 0)
        {
            Use(ConsumibleType.Food, 2f);
        }
        hud.Vendes = vendas;
        hud.Menjar = menjar;
    }

    internal void Use(ConsumibleType type, float ammount)
    {
        switch (type)
        {
            case ConsumibleType.Food:
                menjar--;
                StartCoroutine(DoEat(ammount));
                break;
            case ConsumibleType.Health:
                vendas--;
                GetComponent<HealthController>().CurrentHP += ammount;
                break;

        }
    }
    private void PickUp(Item item)
    {
        item.pickUp();
    }
    private IEnumerator DoEat(float ammount)
    {
        GetComponent<FPSController>().HasEaten = true;
        yield return new WaitForSeconds(ammount);
        GetComponent<FPSController>().HasEaten = false;
    }
}
