using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorSpawner : MonoBehaviour
{
	[SerializeField]
	GameObject[] decors;

	[SerializeField]
	float minSpawnTime;
	[SerializeField]
	float maxSpawnTime;

	private void Start()
	{
		SpawnDecor();
	}

	private void SpawnDecor()
	{
		StartCoroutine(InstantiateDecor(GetRandomDecor(), GetRandomTime()));
	}

	float GetRandomTime() 
	{
		return Random.Range(minSpawnTime, maxSpawnTime);
	}

	GameObject GetRandomDecor()
	{
		return decors[Random.Range(0, decors.Length)];
	}

	IEnumerator InstantiateDecor(GameObject decor, float time)
	{
		yield return new WaitForSeconds(time);
		Instantiate(decor, transform.position + (Vector3.up * Random.Range(-1f, 0f)), Quaternion.identity, transform);
		SpawnDecor();
	}
}
