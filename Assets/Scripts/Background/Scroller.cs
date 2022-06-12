using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Scroller : MonoBehaviour
{
	[SerializeField]
	Transform nextBackgroundPosition;
	new Transform transform;

	protected virtual void Start()
	{
		transform = GetComponent<Transform>();
	}

	private void Update()
	{
		transform.position -= Vector3.right * Time.deltaTime * StaticHelper.scrollSpeed;
	}
}
