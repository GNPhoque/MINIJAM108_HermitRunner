using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : CharacterController
{
	private void Update()
	{
		CheckCollisions(mask);
	}
}
