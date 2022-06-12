using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScroller : Scroller
{
	[SerializeField]
	GameObject[] enemies;

	protected override void Start()
	{
		base.Start();
		Instantiate(SelectRandomEnemyPreset(), transform.position, Quaternion.identity, transform);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			transform.position += Vector3.right * 21.4f;
			Instantiate(SelectRandomEnemyPreset(), transform.position, Quaternion.identity, transform); 
		}
	}
	
	GameObject SelectRandomEnemyPreset()
	{
		return enemies[Random.Range(0, enemies.Length)];
	}
}