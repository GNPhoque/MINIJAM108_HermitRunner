using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeY : MonoBehaviour
{
    CharacterController[] enemies;

    void Awake()
    {
        enemies = GetComponentsInChildren<CharacterController>();
        foreach (CharacterController enemy in enemies)
        {
            if(enemy is ObstacleController)
            {
                continue;
            }
            enemy.transform.position = new Vector2(enemy.transform.position.x, Random.Range(-2.5f, 3.5f));
        }
    }
}
