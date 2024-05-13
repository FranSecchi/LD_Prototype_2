using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HUD : MonoBehaviour
{
    public static HUD instance;
    public Image staminaImg;
    public Image healthImg;
    public TextMeshProUGUI eatTxt;
    public TextMeshProUGUI vendasTxt;
    public TextMeshProUGUI menjarTxt;
    public FPSController controller;
    public HealthController healthController;
    public int nextScene;
    private int vendes = 0;
    private int menjar = 0;

    public int Vendes { get => vendes; set => vendes = value; }
    public int Menjar { get => menjar; set => menjar = value; }

    private void Awake()
    {
        instance = this;
        eatTxt.text = "";
    }
    // Update is called once per frame
    void Update()
    {
        staminaImg.fillAmount = controller.CurrentStamina / controller.m_stamina;
        healthImg.fillAmount = healthController.CurrentHP / healthController.maxHP;
        vendasTxt.text = "Vendas: "+vendes.ToString();
        menjarTxt.text = "Comida: " + menjar.ToString();
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
        eatTxt.text = "";
    }
    public void OnChangeScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
