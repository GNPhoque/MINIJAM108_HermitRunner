using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
	new Transform transform;

	private void Start()
	{
		transform = GetComponent<Transform>();
	}

	private void Update()
	{
		transform.position -= Vector3.right * Time.deltaTime * StaticHelper.scrollSpeed;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			transform.position = StaticHelper.nextBackgroundPosition.position;
		}
	}
}
