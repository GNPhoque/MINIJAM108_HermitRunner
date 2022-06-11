using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleController : CharacterController
{
	BoxCollider2D collider;

	private void Start()
	{
		collider = GetComponentInChildren<BoxCollider2D>();
	}

	private void Update()
	{
		CheckCollisions(mask);
	}

	public override void CheckCollisions(LayerMask mask)
	{
		Debug.DrawLine(transform.position, transform.position + Vector3.right * colliderRadius, Color.red);
		Collider2D[] col = new Collider2D[1];
		Physics2D.OverlapCollider(collider,new ContactFilter2D() { layerMask = mask },col);
		Collider2D collided = col.FirstOrDefault();
		if (collided)
		{
			CharacterController controller = collided?.GetComponent<CharacterController>();
			Debug.Log($"Damaged {name}, layer {gameObject.layer} from {(collided.name == "Player" ? collided.name : collided.transform.parent.name)}");
			TakeDamage();
			if (controller)
			{
				controller.TakeDamage(gameObject.layer);
			}
		}
	}
}
