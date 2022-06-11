using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
	PlayerControls inputs;
	PlayerMovement movement;
	GameObject lazer;
	LineRenderer line;
	MeshCollider meshCollider;

	private void Awake()
	{
		inputs = new PlayerControls();
		movement = GetComponent<PlayerMovement>();

		lazer = transform.GetChild(0).gameObject;
		line = lazer.GetComponent<LineRenderer>();
		meshCollider = lazer.AddComponent<MeshCollider>();
	}

	private void OnEnable()
	{
		inputs.Crab.Defense.performed += Defense_performed;
		inputs.Crab.Dig.performed += Dig_performed;
		inputs.Crab.Move.performed += Move_performed;
		inputs.Crab.Move.canceled += Move_performed;
		inputs.Crab.Roll.performed += Roll_performed;
		inputs.Crab.Shoot.performed += Shoot_performed;
		inputs.Crab.Enable();
	}

	private void OnDisable()
	{
		inputs.Crab.Defense.performed -= Defense_performed;
		inputs.Crab.Dig.performed -= Dig_performed;
		inputs.Crab.Move.performed -= Move_performed;
		inputs.Crab.Roll.performed -= Roll_performed;
		inputs.Crab.Shoot.performed -= Shoot_performed;
		inputs.Crab.Disable();
	}

	private void Shoot_performed(InputAction.CallbackContext obj)
	{
		ShootLazer();
	}

	private void Roll_performed(InputAction.CallbackContext obj)
	{
		Debug.Log("ROLL");
	}

	private void Move_performed(InputAction.CallbackContext obj)
	{
		Debug.Log("MOVE "+obj.ReadValue<Vector2>());
		movement.UpdateMovementInput(obj.ReadValue<Vector2>());
	}

	private void Dig_performed(InputAction.CallbackContext obj)
	{
		Debug.Log("DIG");
	}

	private void Defense_performed(InputAction.CallbackContext obj)
	{
		Debug.Log("DEFENSE");
	}

	#region LAZER
	private void ShootLazer()
	{
		Vector3 mousePosition = GetMousePosition();
		Debug.Log("SHOOT at " + mousePosition);
		ShowLazer(mousePosition);
		RefreshLazerCollider();
		StartCoroutine(StopLazer());
	}

	private static Vector3 GetMousePosition()
	{
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
		mousePosition.z = 1f;
		return mousePosition;
	}

	private void ShowLazer(Vector3 mousePosition)
	{
		line.SetPositions(new Vector3[] { transform.position, mousePosition });
		lazer.SetActive(true);
	}

	private void RefreshLazerCollider()
	{
		Mesh mesh = new Mesh();
		line.BakeMesh(mesh, true);
		meshCollider.sharedMesh = mesh;
	}

	IEnumerator StopLazer()
	{
		yield return new WaitForSeconds(1f);
		lazer.SetActive(false);
	} 
	#endregion
}
