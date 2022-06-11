using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{

	private void Update()
	{
		CheckCollisions(mask);
	}
}
