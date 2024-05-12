using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConsumibleType
{
    Health, Food, Ammo
}
public class Consumible : MonoBehaviour, Item
{
    [SerializeField] private ConsumibleType type;
    [SerializeField] private int ammount;
    [SerializeField] private Transform player;
    public void pickUp()
    {
        switch (type)
        {
            case ConsumibleType.Health:
                player.GetComponent<PickUpItem>().vendas += ammount;
                break;
            case ConsumibleType.Food:
                player.GetComponent<PickUpItem>().menjar += ammount;
                break;
            case ConsumibleType.Ammo:
                Shooting s = player.GetComponent<Shooting>();
                if (s.enabled) s.maxAmmo += ammount;
                else return;
                break;
        }
        Destroy(gameObject);
    }
}
