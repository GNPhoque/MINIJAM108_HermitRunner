using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	float scrollSpeed;

	public static GameManager instance;

	public float ScrollSpeed { get => scrollSpeed; set { scrollSpeed = value; UpdateScrollSpeed(); } }

	private void OnValidate()
	{
		ScrollSpeed = scrollSpeed;
		UpdateScrollSpeed();
	}

	private void Start()
	{
		instance = this;
		StaticHelper.nextBackgroundPosition = transform;
		UpdateScrollSpeed();
	}

	[ContextMenu("UpdateScrollSpeed")]
	void UpdateScrollSpeed()
	{
		StaticHelper.scrollSpeed = ScrollSpeed;
	}

	public void UpdateScrollSpeed(float speed)
	{
		ScrollSpeed = speed;
		StaticHelper.scrollSpeed = ScrollSpeed;
	}
}
