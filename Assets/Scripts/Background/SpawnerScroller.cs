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
		Instantiate(enemies[0], nextBackgroundPosition.position, Quaternion.identity, nextBackgroundPosition);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("SPAWNER TRIGGERED");
		if (collision.CompareTag("Player"))
		{
			transform.position += Vector3.right * 21.4f;
			Instantiate(SelectRandomEnemyPreset(), nextBackgroundPosition.position, Quaternion.identity, nextBackgroundPosition); 
		}
	}
	
	GameObject SelectRandomEnemyPreset()
	{
		return enemies[Random.Range(0, enemies.Length)];
	}
}