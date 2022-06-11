using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class CharacterController : MonoBehaviour
{
	[SerializeField]
	protected float colliderRadius;
	[SerializeField]
	protected LayerMask mask;

	protected bool isDigging;
	protected bool isDefending;
	protected bool hasShell;

	public void CheckCollisions(LayerMask mask)
	{
		Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, colliderRadius, mask);
		CharacterController controller = col.FirstOrDefault(x => x.gameObject != gameObject)?.GetComponent<CharacterController>();
		if (controller)
		{
			TakeDamage();
			controller.TakeDamage(gameObject.layer);
		}
	}

	public void TakeDamage()
	{
		if (isDigging)
		{
			return;
		}
		if (hasShell)
		{
			if (isDefending)
			{
				return;
			}
			hasShell = false;
			return;
		}
		Destroy(gameObject);
	}

	public void TakeDamage(int layer)
	{
		if (mask == (mask | (1 << layer)))
		{
			TakeDamage();
		}
	}
}