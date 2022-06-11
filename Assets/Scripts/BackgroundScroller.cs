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
		Debug.Log("TRIGGER ENTER");
		if (collision.CompareTag("Player"))
		{
			transform.position = StaticHelper.nextBackgroundPosition.position;
			StaticHelper.nextBackgroundPosition = nextBackgroundPosition;
		}
	}
}
