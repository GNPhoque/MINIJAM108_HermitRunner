using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_spike : MonoBehaviour
{
    [SerializeField] float speed;
    void Awake()
    {
        
    }   

    void Update()
    {
        transform.parent.transform.position += speed * (Vector3)transform.right * Time.deltaTime;
    }
}
