using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : CharacterController
{
	[SerializeField]
	float defenseDuration;
	[SerializeField]
	public float shellDuration;
	[SerializeField]
	Vector2 noShellColliderOffset;
	[SerializeField]
	Vector2 noShellColliderSize;
	[SerializeField]
	Vector2 hasShellColliderOffset;
	[SerializeField]
	Vector2 hasShellColliderSize;

	PlayerControls inputs;
	PlayerMovement movement;
	GameObject lazer;
	LineRenderer line;
	PolygonCollider2D polyCollider;
	BoxCollider2D boxCollider;
	Vector3 oldPosition;
	Vector3 oldMousePosition;
	Coroutine shellCoroutine;
	bool lazerReady = true;


	#region MONOBEHAVIOUR
	private void Awake()
	{
		animator = GetComponentInChildren<Animator>();
		inputs = new PlayerControls();
		boxCollider = GetComponent<BoxCollider2D>();
		movement = GetComponent<PlayerMovement>();

		lazer = transform.GetChild(0).gameObject;
		line = lazer.GetComponent<LineRenderer>();
		polyCollider = lazer.AddComponent<PolygonCollider2D>();
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

		HasShellChanged += UpdateShellCollider;
	}

	private void OnDisable()
	{
		inputs.Crab.Defense.performed -= Defense_performed;
		inputs.Crab.Dig.performed -= Dig_performed;
		inputs.Crab.Move.performed -= Move_performed;
		inputs.Crab.Roll.performed -= Roll_performed;
		inputs.Crab.Shoot.performed -= Shoot_performed;
		inputs.Crab.Disable();

		HasShellChanged -= UpdateShellCollider;
	}

	private void OnDestroy()
	{
		GameManager.instance.LoseGame();
	}

	private void Update()
	{
		CheckCollisions(mask);
		if (lazer.activeSelf)
		{
			Vector3 positionDifference = transform.position - oldPosition;
			Vector3 newMousePosition = oldMousePosition + positionDifference;
			line.SetPositions(new Vector3[] { transform.position, newMousePosition });
			oldMousePosition = newMousePosition;
			oldPosition = transform.position;
		}
	}
	#endregion

	#region INPUTS
	private void Shoot_performed(InputAction.CallbackContext obj)
	{
		ShootLazer();
	}

	private void Roll_performed(InputAction.CallbackContext obj)
	{
		//Debug.Log("ROLL");
		movement.Roll();
	}

	private void Move_performed(InputAction.CallbackContext obj)
	{
		//Debug.Log("MOVE " + obj.ReadValue<Vector2>());
		movement.UpdateMovementInput(obj.ReadValue<Vector2>());
	}

	private void Dig_performed(InputAction.CallbackContext obj)
	{
		//Debug.Log("DIG");
	}

	private void Defense_performed(InputAction.CallbackContext obj)
	{
		if (isDefending) return;
		//Debug.Log("DEFENSE");
		float oldSpeed = GameManager.instance.ScrollSpeed;
		GameManager.instance.UpdateScrollSpeed(0f);
		isDefending = true;
		StartCoroutine(StopDefense(oldSpeed));
	} 
	#endregion

	#region LAZER
	private void ShootLazer()
	{
		if (!lazerReady) return;
		Vector3 mousePosition = GetMousePosition();
		//Debug.Log("SHOOT at " + mousePosition);
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
		oldPosition = transform.position;
		oldMousePosition = mousePosition;
		lazer.SetActive(true);
		lazerReady = false;
	}

	private void RefreshLazerCollider()
	{
		//Mesh mesh = new Mesh();
		//line.BakeMesh(mesh, true);
		//meshCollider.sharedMesh = mesh;
		polyCollider.SetPath(0, CalculateColliderPoints().ConvertAll(x => (Vector2)transform.InverseTransformPoint(x)));
	}

	IEnumerator StopLazer()
	{
		yield return new WaitForSeconds(1f);
		lazer.SetActive(false);
		yield return new WaitForSeconds(9f);
		lazerReady = true;
	}

	private List<Vector2> CalculateColliderPoints()
	{
		Vector3[] positions = new Vector3[line.positionCount];
		line.GetPositions(positions);
		//Get The Width of the Line
		float width = line.startWidth;

		// m = (y2 - y1) / (x2 - x1)
		float m = (positions[1].y - positions[0].y) / (positions[1].x - positions[0].x);
		float deltaX = (width / 2f) * (m / Mathf.Pow(m * m + 1, 0.5f));
		float deltaY = (width / 2f) * (1 / Mathf.Pow(1 + m * m, 0.5f));

		//Calculate Vertex Offset from Line Point
		Vector3[] offsets = new Vector3[2];
		offsets[0] = new Vector2(-deltaX, deltaY);
		offsets[1] = new Vector2(deltaX, -deltaY);

		List<Vector2> colliderPoints = new List<Vector2> {
			positions[0] + offsets[0],
			positions[1] + offsets[0],
			positions[1] + offsets[1],
			positions[0] + offsets[1]
		};

		return colliderPoints;
	}
	#endregion

	void UpdateShellCollider(bool hasShell)
	{
		if (hasShell)
		{
			boxCollider.offset = noShellColliderOffset;
			boxCollider.size = noShellColliderSize;
		}
		else
		{
			boxCollider.offset = hasShellColliderOffset;
			boxCollider.size = hasShellColliderSize;
		}
	}

	IEnumerator StopDefense(float oldSpeed)
	{
		yield return new WaitForSeconds(defenseDuration);
		GameManager.instance.UpdateScrollSpeed(oldSpeed);
		isDefending = false;
	}

	public void EquipShell()
	{
		if (HasShell)
		{
			StopCoroutine(shellCoroutine);
		}
		HasShell = true;
		shellCoroutine = StartCoroutine(StopShell());
	}

	IEnumerator StopShell()
	{
		yield return new WaitForSeconds(shellDuration);
		HasShell = false;
	}
}
