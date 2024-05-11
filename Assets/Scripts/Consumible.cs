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
    [SerializeField] private float ammount;
    [SerializeField] private Transform player;

    public void pickUp()
    {
        switch (type)
        {
            case ConsumibleType.Health:
                player.GetComponent<HealthController>().CurrentHP += ammount;
                break;
            case ConsumibleType.Food:
                player.GetComponent<FPSController>().Eat(ammount);
                HUD.instance.Eat(ammount);
                break;
            case ConsumibleType.Ammo:
                Shooting s = player.GetComponent<Shooting>();
                if (s.enabled) s.Ammo += (int)ammount;
                else return;
                break;
        }
        Destroy(gameObject);
    }
}
