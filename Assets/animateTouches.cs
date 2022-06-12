using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class animateTouches : MonoBehaviour
{
    [SerializeField] Sprite image1;
    [SerializeField] Sprite image2;
    [SerializeField] float timeToSwap;
    float swapTimer;
   
    private void Awake()
    {
        GetComponent<Image>().sprite = image1;
    }
    void Update()
    {
        swapTimer += Time.deltaTime;
        if(swapTimer>= timeToSwap)
        {
            GetComponent<Image>().sprite = image2;
        }
        if(swapTimer>= timeToSwap*2)
        {
            GetComponent<Image>().sprite = image1;
            swapTimer = 0f;
        }
    }
}
