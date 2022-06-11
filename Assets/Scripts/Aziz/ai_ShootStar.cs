using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ai_ShootStar : MonoBehaviour
{
    [SerializeField] int numberOfSpikes;
    [SerializeField] int numberOfWaves;
    float spikeSpeed;
    [SerializeField] GameObject spike;
    [SerializeField] float distanceFromCenter;
    [SerializeField] float distanceBeforeShoot;

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > 3)
        {
            ShootStarSpikes();
        }

    }

    void ShootStarSpikes()
    {
        for (int i = 0; i < numberOfSpikes; i++)
        {
            float spawnAngle = (360 / numberOfSpikes)*i+1;

            Vector2 spawnPos = (Vector2)transform.position + (Vector2)(Quaternion.Euler(0, 0, spawnAngle) * Vector2.right);

            Instantiate(spike, spawnPos, Quaternion.Euler(0,0, spawnAngle));
        }
    }
}
