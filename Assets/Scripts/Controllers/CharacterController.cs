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

	protected Animator animator;
	protected bool isDigging;
	protected bool isDefending;
	private bool hasShell;

	public bool HasShell { get => hasShell; set { hasShell = value; animator.SetBool("HasShell", value); } }

	void OnDrawGizmosSelected()
	{
		// Draw a yellow sphere at the transform's position
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(transform.position, colliderRadius);
	}

	public virtual void CheckCollisions(LayerMask mask)
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
		if (isDigging || HasShell || isDefending)
		{
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