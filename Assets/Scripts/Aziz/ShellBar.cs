using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShellBar : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] Image fill;

    PlayerController playerController;
    Slider slider;
    float barDurationLeft;
    [SerializeField] float initialBarDuration;
    float currentFill;
    bool hasShell;

    void Awake()
    {
        slider = GetComponent<Slider>();
        playerController = player.GetComponent<PlayerController>();
        initialBarDuration = playerController.shellDuration;
    }

    private void OnEnable()
	{
		playerController.HasShellChanged += PlayerController_HasShellChanged;
	}
	private void OnDisable()
	{
		playerController.HasShellChanged -= PlayerController_HasShellChanged;
    }

    private void PlayerController_HasShellChanged(bool obj)
	{
        hasShell = obj;
		if (!obj)
		{
            fill.color = Color.black;
            barDurationLeft = initialBarDuration;
        }
		else
		{
            barDurationLeft = initialBarDuration;
		}
    }

    void Update()
    {
        if (!player) return;

        if (hasShell)
        {
            barDurationLeft -= Time.deltaTime;

            if (currentFill < 0.25f)
            {
                fill.color = Color.red;
            }
            else if (currentFill < 0.5f)
            {
                fill.color = new Vector4(255, 165, 0, 1);
            }
            else
            {
                fill.color = Color.cyan;
            }
        }
        currentFill = barDurationLeft / initialBarDuration;
        slider.value = currentFill;
    }
}
