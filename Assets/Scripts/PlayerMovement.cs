using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    AnimationCurve rollCurve;
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float rollSpeedMultiplier;
    [SerializeField]
    float rollDuration;

    bool isRolling;
    float rollEndTime;
    float timeSinceRollStarted;
    Vector2 movementInput;
    new Transform transform;

	private void Start()
	{
        transform = GetComponent<Transform>();
	}

	void Update()
    {
		if (isRolling)
		{
			MoveRoll();
		}
		else
		{
			Vector3 nextPos = transform.position + (Vector3)movementInput * Time.deltaTime * moveSpeed;
			if (nextPos.y < GameManager.instance.playerMaxY && nextPos.y > GameManager.instance.playerMinY)
				transform.position = nextPos;
		}
    }

	private void MoveRoll()
	{
		if (rollEndTime > Time.time)
		{
			timeSinceRollStarted += Time.deltaTime;
			transform.position += new Vector3(rollCurve.Evaluate(timeSinceRollStarted), 0f, 0f) * Time.deltaTime * moveSpeed * rollSpeedMultiplier;
		}
		else
		{
			isRolling = false;
			transform.position = new Vector3(-6f, transform.position.y, transform.position.z);
		}
	}

	public void UpdateMovementInput(Vector2 input)
	{
        movementInput = input;
	}

    public void Roll()
	{
        if (isRolling) return;
        isRolling = true;
        timeSinceRollStarted = 0f;
        rollEndTime = Time.time + rollDuration;
    }
}
