using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;

    void Update()
    {
        transform.position += Vector3.up * Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
    }
}
