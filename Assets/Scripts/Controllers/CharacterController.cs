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
		Debug.DrawLine(transform.position, transform.position + Vector3.right * colliderRadius,Color.red);
		Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, colliderRadius, mask);
		Collider2D collided = col.FirstOrDefault(x => x.gameObject != gameObject);
		if (collided)
		{
			CharacterController controller = collided?.GetComponent<CharacterController>();
			Debug.Log($"Damaged {name}, layer {gameObject.layer} from {(collided.name=="Player"?collided.name:collided.transform.parent.name)}");
			TakeDamage();
			if (controller)
			{
				controller.TakeDamage(gameObject.layer);
			} 
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