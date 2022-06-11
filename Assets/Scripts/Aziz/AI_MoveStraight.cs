using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_MoveStraight : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform target;
    Vector2 fixedTarget;
    Vector2 direction;
    bool actionStarted;

    void Awake()
    {
        direction = target.position - transform.position;
        direction.Normalize();
        fixedTarget = target.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            actionStarted = true;
        }
    }

    void FixedUpdate()
    {
        if (actionStarted)
        {
            MoveStraight();
        }

        if(Vector2.Distance(transform.position, fixedTarget) < 0.1f)
        {
            Destroy(gameObject);
        }
    }

    void MoveStraight()
    {
        transform.Translate((Vector2)transform.position + direction * speed * Time.deltaTime);
    }
}
