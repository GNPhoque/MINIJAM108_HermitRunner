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
    [SerializeField] float attackCooldown;
    float attackcurrentCooldown;

    //GameObject graph;
    //GameObject physics;
    GameObject player;

    Animator animator;

    bool isShooting;
    bool isOnFire;


    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        animator.SetTrigger("Burst");
        player = GameObject.Find("Player");
        attackcurrentCooldown = attackCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(player.transform.position,transform.position)< distanceBeforeShoot && !isOnFire)
        {
            isShooting = true;
        }

        if(isShooting)
        {
            attackcurrentCooldown += Time.deltaTime;
            if(attackcurrentCooldown > attackCooldown)
            {
                ShootStarSpikes();
                animator.SetTrigger("Attack");
                attackcurrentCooldown = 0;
            }
        }

        if(isOnFire)
        {
            isShooting = false;
            animator.SetBool("OnFire", true);
        }
    }

    void ShootStarSpikes()
    {
        for (int i = 0; i < numberOfSpikes; i++)
        {
            float spawnAngle = (360 / numberOfSpikes)*i+1;

            Vector2 spawnPos = (Vector2)transform.position + (Vector2)(Quaternion.Euler(0, 0, spawnAngle) * Vector2.right);

            Instantiate(spike, spawnPos, Quaternion.Euler(0,0, spawnAngle), transform);
        }
    }
}
