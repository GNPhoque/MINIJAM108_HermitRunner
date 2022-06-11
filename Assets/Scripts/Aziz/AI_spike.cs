using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_spike : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody2D rb2d;
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        
    }   

    void Update()
    {
        rb2d.MovePosition(rb2d.position + speed * (Vector2)transform.right * Time.fixedDeltaTime);
    }
}
