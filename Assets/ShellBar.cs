using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShellBar : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] Image mask;
    int barDurationLeft;
    int initialBarDuration;
    float currentFill;

    void Awake()
    {
        
    }

    void Update()
    {
        //barDurationLeft = player.GetComponent<Script>().data;
        //initialBarDuration = player.GetComponent<Script>().data;

        if (initialBarDuration != 0)
        {
            currentFill = barDurationLeft / initialBarDuration;
            mask.fillAmount = currentFill;
        }

        if(currentFill < 0.25)
        {
            mask.color = Color.red;
        }
        else if(currentFill <0.5)
        {
            mask.color = new Vector4(255, 165, 0, 1);
        }
        else
        {
            mask.color = Color.cyan;
        }
    }
}
