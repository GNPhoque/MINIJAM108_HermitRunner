using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
	[SerializeField]
	Transform nextBackgroundPosition;
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
			//Debug.Log(transform.position);
			transform.position = StaticHelper.nextBackgroundPosition.position + Vector3.right * 21.4f;
			//StaticHelper.nextBackgroundPosition = nextBackgroundPosition;
			StaticHelper.nextBackgroundPosition = transform;
			//Debug.Log(transform.position);
			//Debug.Break();
		}
	}
}
