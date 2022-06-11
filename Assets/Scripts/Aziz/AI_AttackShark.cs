using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_AttackShark : MonoBehaviour
{
    [SerializeField] AnimationCurve sharkAttackCurve;
    [SerializeField] float attackMaxDuration;
    float attackTime = 0.01f;
    bool attackStarted;

    [SerializeField] float attackSize;

    Vector2 initialPos;

    void Awake()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
        transform.Translate(initialPos + sharkAttackCurve.Evaluate(attackTime/ attackMaxDuration) * Vector2.up* attackSize);
    }
}
