using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	float _scrollSpeed;

	public static GameManager instance;

	public float ScrollSpeed { get => _scrollSpeed; set { _scrollSpeed = value; UpdateScrollSpeed(); } }

	private void OnValidate()
	{
		ScrollSpeed = _scrollSpeed;
		UpdateScrollSpeed();
	}

	private void Start()
	{
		instance = this;
		StaticHelper.nextBackgroundPosition = transform;
		StaticHelper.score = 0;
		UpdateScrollSpeed();
		InvokeRepeating("SpeedUp", 20f, 20f);
	}

	//private void Update()
	//{
	//	Time.timeScale *= 1 + (Time.deltaTime / 100f);
	//	//ScrollSpeed *= 1 + (Time.deltaTime / 100f);
	//}

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

	void SpeedUp()
	{
		Time.timeScale *= 1.05f;
	}

	public void LoseGame()
	{
		Time.timeScale = 0f;
		Debug.Log("GAME OVER");
	}
}
