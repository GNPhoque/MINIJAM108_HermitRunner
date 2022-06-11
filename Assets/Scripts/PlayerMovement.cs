using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;

    Vector2 movementInput;

    void Update()
    {
        transform.position += (Vector3)movementInput * Time.deltaTime * moveSpeed;
    }

    public void UpdateMovementInput(Vector2 input)
	{
        movementInput = input;
	}
}
