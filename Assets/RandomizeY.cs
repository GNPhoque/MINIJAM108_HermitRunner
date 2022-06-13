using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeY : MonoBehaviour
{
    Transform[] enemies;

    void Awake()
    {
        enemies = GetComponentsInChildren<Transform>();
        foreach (Transform enemy in enemies)
        {
            if(enemy.GetComponent<ObstacleController>() == null)
            {
                enemy.transform.position = new Vector2(enemy.transform.position.x, Random.Range(-2.5f, 3.5f));
            }
        }
    }
}
