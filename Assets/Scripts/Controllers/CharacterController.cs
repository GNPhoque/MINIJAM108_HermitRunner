using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class CharacterController : MonoBehaviour
{
	[SerializeField]
	protected float timeBeforeDestroy;
	[SerializeField]
	protected LayerMask mask;
	[SerializeField]
	protected new Collider2D collider;

	protected Animator animator;
	protected bool isDigging;
	protected bool isDefending;
	private bool hasShell;
	private bool logDamage;

	public bool HasShell { get => hasShell; set { hasShell = value; animator.SetBool("HasShell", value); HasShellChanged?.Invoke(value); } }

	public event Action<bool> HasShellChanged;
	public event Action OnDeath;

	protected virtual void Update()
	{
		CheckCollisions(mask);
	}

	//void OnDrawGizmosSelected()
	//{
	//	// Draw a yellow sphere at the transform's position
	//	Gizmos.color = Color.yellow;
	//	Gizmos.DrawSphere(transform.position, colliderRadius);
	//}

	public virtual void CheckCollisions(LayerMask mask)
	{
		logDamage = true;
		//Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, colliderRadius, mask);
		List<Collider2D> col = new List<Collider2D>();
		Physics2D.OverlapCollider(collider, new ContactFilter2D() { layerMask = mask, useLayerMask = true }, col);
		Collider2D collided = col.FirstOrDefault(x => x != collider);
		if (collided)
		{
			CharacterController controller = collided?.GetComponent<CharacterController>();
			TakeDamage();
			if (logDamage)
			{
				Debug.Log($"Damaged {name}, layer {gameObject.layer} from {(collided.name == "Player" ? collided.name : collided.transform.parent.name)}");
			}
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
			logDamage = false;
			return;
		}
		if(this is PlayerController)
		{
			//Debug.Break();
		}
		OnDeath?.Invoke();
		Destroy(gameObject, timeBeforeDestroy);
	}

	public void TakeDamage(int layer)
	{
		if (mask == (mask | (1 << layer)))
		{
			TakeDamage();
		}
	}
}