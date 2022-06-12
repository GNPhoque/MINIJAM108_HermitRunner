using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererAnimator : MonoBehaviour
{
	[SerializeField]
	Texture[] lazerBody;
	[SerializeField]
	Material material;

	LineRenderer lr;

	private void Awake()
	{
		lr = GetComponent<LineRenderer>();
	}

	private void OnEnable()
	{
		StartCoroutine(ChangeTextures());
	}

	IEnumerator ChangeTextures()
	{
		material.SetTexture("_MainTex", lazerBody[0]);
		yield return new WaitForSeconds(.2f);
		material.SetTexture("_MainTex", lazerBody[1]);
		yield return new WaitForSeconds(.2f);
		material.SetTexture("_MainTex", lazerBody[2]);
	}
}
