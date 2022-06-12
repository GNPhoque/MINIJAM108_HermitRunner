using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSwap : MonoBehaviour
{
    [SerializeField] Sprite musicOn;
    [SerializeField] Sprite musicOff;
    [SerializeField] GameObject imageReceiver;
    Toggle toggle;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
    }

    public void ChangeSprite()
    {
        if(toggle.isOn)
        {
            imageReceiver.GetComponent<Image>().sprite = musicOn;
        }
        if(!toggle.isOn)
        {
            imageReceiver.GetComponent<Image>().sprite = musicOff;
        }
    }
}
