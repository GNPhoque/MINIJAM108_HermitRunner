using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : CharacterController
{
	private void Update()
	{
		CheckCollisions(mask);
	}

}
