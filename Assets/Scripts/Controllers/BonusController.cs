using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BonusController : CharacterController
{
	private void Update()
	{
		CheckCollisions(mask);
	}

	public override void CheckCollisions(LayerMask mask)
	{
		Debug.DrawLine(transform.position, transform.position + Vector3.right * colliderRadius, Color.red);
		Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, colliderRadius, mask);
		Collider2D collided = col.FirstOrDefault(x => x.gameObject != gameObject);
		if (collided)
		{
			PlayerController controller = collided?.GetComponent<PlayerController>();
			Debug.Log($"LOOTED");
			controller.EquipShell();
			Destroy(gameObject);
		}
	}
}
