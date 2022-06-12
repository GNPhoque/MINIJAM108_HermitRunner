using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	float _scrollSpeed;
	[SerializeField]
	TMPro.TMP_Text gameoverText;
	[SerializeField]
	GameObject losePanel;
	[SerializeField]
	public float playerMaxY;
	[SerializeField]
	public float playerMinY;

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
		InvokeRepeating("SpeedUp", 10f, 10f);
		Time.timeScale = 1f;
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
		Debug.Log($"TimeScale : {Time.timeScale}");
	}

	public void LoseGame()
	{
		Time.timeScale = 0f;
		losePanel.SetActive(true);
		if (StaticHelper.score > StaticHelper.highscore) StaticHelper.highscore = StaticHelper.score;
		gameoverText.text = $"You lose\n\nSCORE : {StaticHelper.score.ToString("00000")}\nHIGHSCORE : {StaticHelper.highscore.ToString("00000")}";
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
