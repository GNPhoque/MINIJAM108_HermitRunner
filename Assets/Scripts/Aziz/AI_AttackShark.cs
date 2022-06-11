using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_AttackShark : MonoBehaviour
{
    [SerializeField] AnimationCurve sharkAttackCurve;
    [SerializeField] float attackMaxDuration;
    [SerializeField] float attackRange;
    float attackTime = 0.01f;
    bool attackStarted;

    [SerializeField] float attackSize;

    Vector2 initialPos;

    GameObject player;

    void Awake()
    {
        initialPos = transform.position;
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (!player) return;
        if (transform.position.x - player.transform.position.x < attackRange)
        {
            attackStarted = true;
        }

        if(attackStarted)
        {
            attackTime += Time.deltaTime;
            SharkAttack();
        }

        if((attackTime/ attackMaxDuration) > 1)
        {
            attackStarted = false;
        }
    }

    void SharkAttack()
    {
        transform.position = new Vector3(transform.position.x, initialPos.y + sharkAttackCurve.Evaluate(attackTime/ attackMaxDuration)* attackSize,0);
    }
}
