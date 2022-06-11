using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundPooler : MonoBehaviour
{
	[SerializeField]
	GameObject backgroundPrefab;
	[SerializeField]
	Transform nextBackgroundPosition;
	[SerializeField]
	float scrollSpeed;
	[SerializeField]
	float backgroundRefreshRatio;

	List<GameObject> backgrounds;
	new Transform transform;
	private void Start()
	{
		backgrounds = new List<GameObject>();
		transform = GetComponent<Transform>();
		StaticHelper.scrollSpeed = scrollSpeed;
		StaticHelper.nextBackgroundPosition = nextBackgroundPosition;

		AddNewBackground();
		AddNewBackground();
		AddNewBackground();

		//StartCoroutine(CreateAndDestroyBackgrounds());
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space)) AddNewBackground();
	}

	void AddNewBackground()
	{
		GameObject newBackground = Instantiate(backgroundPrefab, nextBackgroundPosition.position, Quaternion.identity, transform);
		nextBackgroundPosition = newBackground.transform.GetChild(0).transform;
		StaticHelper.nextBackgroundPosition = nextBackgroundPosition;
		backgrounds.Add(newBackground);
	}

	void DestroyFirstBackground()
	{
		Destroy(backgrounds[0]);
		backgrounds.RemoveAt(0);
	}

	IEnumerator CreateAndDestroyBackgrounds()
	{
		while (true)
		{
			yield return new WaitForSeconds(backgroundRefreshRatio * scrollSpeed);
			AddNewBackground();
			DestroyFirstBackground();
		}
	}

	void CycleBackgrounds()
	{

	}
}