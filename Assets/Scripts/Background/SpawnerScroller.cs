using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScroller : Scroller
{
	[SerializeField]
	GameObject enemies;

	protected override void Start()
	{
		base.Start();
		Instantiate(enemies, transform.position, Quaternion.identity, transform);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			transform.position += Vector3.right * 21.4f;
			Instantiate(enemies, transform.position, Quaternion.identity, transform); 
		}
	}
}