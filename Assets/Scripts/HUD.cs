using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Image staminaImg;
    public Image healthImg;
    public FPSController controller;
    public HealthController healthController;

    // Update is called once per frame
    void Update()
    {
        staminaImg.fillAmount = controller.CurrentStamina / controller.m_stamina;
        healthImg.fillAmount = healthController.CurrentHP / healthController.maxHP;
    }
}
