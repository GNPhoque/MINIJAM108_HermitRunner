using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserBar : MonoBehaviour
{
    [SerializeField] GameObject player;
    PlayerController playerController;
    [SerializeField] Image fill;

    Slider slider;
    float barDurationLeft;
    float laserInitialCooldown;
    float laserTimer;
    bool laserReady;
    float currentFill;

    void Awake()
    {
        slider = GetComponent<Slider>();
        playerController = player.GetComponent<PlayerController>();
        laserInitialCooldown = playerController.lazerCooldown;
        laserReady = playerController.lazerReady;
    }

    void Update()
    {
        laserReady = playerController.lazerReady;
        if (laserReady)
        {
            slider.value = 1;
            laserTimer = laserInitialCooldown;
            fill.color = Color.cyan;

        }
        if (!laserReady)
        {
            if(slider.value == 1)
            {
                slider.value = 0;
                laserTimer = 0;
                fill.color = Color.red;
            }

            laserTimer += Time.deltaTime;
            slider.value = laserTimer / laserInitialCooldown;
        }
    }
}
