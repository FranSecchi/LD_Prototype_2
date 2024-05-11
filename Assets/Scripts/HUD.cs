using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public static HUD instance;
    public Image staminaImg;
    public Image healthImg;
    public TextMeshProUGUI eatTxt;
    public FPSController controller;
    public HealthController healthController;
    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        staminaImg.fillAmount = controller.CurrentStamina / controller.m_stamina;
        healthImg.fillAmount = healthController.CurrentHP / healthController.maxHP;
    }
    internal void Eat(float ammount)
    {
        StartCoroutine(DoEat(ammount));
    }
    private IEnumerator DoEat(float ammount)
    {
        float elapsed = 0f;
        while(elapsed < ammount)
        {
            eatTxt.text = Mathf.RoundToInt(ammount - elapsed).ToString();
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
