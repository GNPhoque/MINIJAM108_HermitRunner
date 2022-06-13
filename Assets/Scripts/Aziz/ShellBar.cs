using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShellBar : MonoBehaviour
{

    [SerializeField] GameObject player;
    //[SerializeField] Image mask;
    [SerializeField] Image fill;

    Slider slider;
    float barDurationLeft;
    float initialBarDuration;
    float currentFill;
    bool hasShell;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        if (!player) return;
        initialBarDuration = player.GetComponent<PlayerController>().shellDuration;
        hasShell = player.GetComponent<PlayerController>().HasShell;

        if(hasShell)
        {
            barDurationLeft -= Time.deltaTime;
        }



        if(currentFill < 0.25f)
        {
            fill.color = Color.red;
        }
        else if(currentFill <0.5f)
        {
            fill.color = new Vector4(255, 165, 0, 1);
        }
        else
        {
            fill.color = Color.cyan;
        }

        if(!hasShell)
        {
            fill.color = Color.black;
            barDurationLeft = initialBarDuration;
        }

        if (initialBarDuration != 0)
        {
            currentFill = barDurationLeft / initialBarDuration;
            slider.value = currentFill;
        }
    }
}
