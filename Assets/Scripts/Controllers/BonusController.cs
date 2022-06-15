using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BonusController : CharacterController
{

	public override void CheckCollisions(LayerMask mask)
	{
		List<Collider2D> col = new List<Collider2D>();
		Physics2D.OverlapCollider(collider, new ContactFilter2D() { layerMask = mask, useLayerMask = true }, col);
		Collider2D collided = col.FirstOrDefault(x => x != collider);
		if (collided)
		{
			PlayerController controller = collided?.GetComponent<PlayerController>();
			Debug.Log($"LOOTED");
			controller.EquipShell();
			Destroy(gameObject);
		}
	}
}
