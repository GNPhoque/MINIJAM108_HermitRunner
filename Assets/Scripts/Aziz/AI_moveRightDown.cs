using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_moveRightDown : MonoBehaviour
{
    //like crabs

    [SerializeField] float moveSpeed;
    Rigidbody2D rb2d;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + new Vector2(0, rb2d.position.y + moveSpeed * Time.deltaTime));
    }
}
