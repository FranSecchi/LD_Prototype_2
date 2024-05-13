using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Heal : MonoBehaviour
{
    private FPSController contr;
    [SerializeField] private KeyCode heal;
    [SerializeField] private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        contr = GetComponent<FPSController>();
        GetComponent<HealthController>().CurrentHP = GetComponent<HealthController>().maxHP* 0.1f;
        contr.CantMove(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(heal))
        {
            text.enabled = false;
            contr.CantMove(true);
            GetComponent<HealthController>().CurrentHP = GetComponent<HealthController>().maxHP;
            Destroy(this);
        }
    }
}
