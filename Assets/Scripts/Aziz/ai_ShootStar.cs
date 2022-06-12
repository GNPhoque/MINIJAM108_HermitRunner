using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ai_ShootStar : MonoBehaviour
{
    [SerializeField] int numberOfSpikes;
    [SerializeField] int numberOfWaves;
    float spikeSpeed;
    [SerializeField] GameObject spike;
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
        player = GameObject.Find("Player");
        attackcurrentCooldown = attackCooldown;
        //animator.SetTrigger("Burst");
    }

    // Update is called once per frame
    void Update()
    {
        if (!player) return;
        if(Vector2.Distance(player.transform.position,transform.position) < distanceBeforeShoot && !isOnFire)
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

        /* marche po, je passe 
        Vector2 playerDirection = player.transform.position - transform.position;
        float playerDirectionAngle = Vector2.Angle(transform.position, playerDirection);
        Instantiate(spike, (Vector2)transform.position + (Vector2)(Quaternion.Euler(0, 0, playerDirectionAngle) * Vector2.right), Quaternion.Euler(0, 0, playerDirectionAngle), transform);
        */
    }
}
