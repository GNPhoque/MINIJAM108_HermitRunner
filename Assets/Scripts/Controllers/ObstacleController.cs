using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleController : CharacterController
{
	new BoxCollider2D boxCollider;

	private void Start()
	{
		boxCollider = GetComponentInChildren<BoxCollider2D>();
	}

	public override void CheckCollisions(LayerMask mask)
	{
		Collider2D[] col = new Collider2D[1];
		Physics2D.OverlapCollider(boxCollider, new ContactFilter2D() { layerMask = mask, useLayerMask = true }, col);
		Collider2D collided = col.FirstOrDefault();
		if (collided)
		{
			CharacterController controller = collided?.GetComponent<CharacterController>();
			Debug.Log($"Damaged {name}, layer {gameObject.layer} from {(collided.name == "Player" ? collided.name : collided.transform.parent.name)}");
			TakeDamage();
			if (controller)
			{
				controller.TakeDamage(gameObject.layer);
				AudioClip clip = GameManager.instance.audioManager.sharkDies;
				GameManager.instance.audioSource.PlayOneShot(clip);
			}
		}
	}
}
