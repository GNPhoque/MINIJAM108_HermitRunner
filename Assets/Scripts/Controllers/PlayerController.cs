using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : CharacterController
{
	#region MEMBERS
	[SerializeField]
	float defenseDuration;
	[SerializeField]
	float defenseCooldown;
	[SerializeField]
	public float shellDuration;
	[SerializeField]
	public float lazerLength;
	[SerializeField]
	public float lazerCooldown;
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
	public bool lazerReady = true;
	bool shootingLazer;
	bool defenseReady = true;
	AudioSource myAudio;

	#endregion


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
		myAudio = GetComponentInChildren<AudioSource>();
	}

	private void Start()
	{
		CinemachineShake.Instance.ShakeCamera(4, false);
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
		OnDeath += PlayerController_OnDeath;
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
		OnDeath -= PlayerController_OnDeath;
	}

	protected override void Update()
	{
		base.Update();
		if (lazer.activeSelf)
		{
			Vector3 pos = (GetMousePosition() - transform.position).normalized;
			pos = pos * lazerLength;
			line.SetPositions(new Vector3[] { transform.position, transform.position+pos});
			RefreshLazerCollider();
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
		if (isDefending || shootingLazer)
		{
			movement.UpdateMovementInput(obj.ReadValue<Vector2>()); 
			return;
		}
		movement.UpdateMovementInput(obj.ReadValue<Vector2>()); 
	}

	private void Dig_performed(InputAction.CallbackContext obj)
	{
		//Debug.Log("DIG");
	}

	private void Defense_performed(InputAction.CallbackContext obj)
	{

		if (isDefending || defenseReady==false) return;
		//Debug.Log("DEFENSE");
		float oldSpeed = GameManager.instance.ScrollSpeed;
		GameManager.instance.UpdateScrollSpeed(0f);
		isDefending = true;
		myAudio.Stop();
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

		AudioClip clip = GameManager.instance.audioManager.laserBeam;
		GameManager.instance.audioSource.PlayOneShot(clip);

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
		Vector3 pos1 = transform.position;
		Vector3 pos2 = (mousePosition - transform.position).normalized * lazerLength;
		
		line.SetPositions(new Vector3[] { pos1, pos1 + pos2 });

		oldPosition = pos1;
		oldMousePosition = mousePosition;
		
		lazer.SetActive(true);
		CinemachineShake.Instance.ShakeCamera(4,true);
		shootingLazer = true;
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
		shootingLazer = false;
		CinemachineShake.Instance.ShakeCamera(4, false);
		yield return new WaitForSeconds(lazerCooldown);
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

	#region DEFENSE
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
		defenseReady = false;
		myAudio.Play();
		yield return new WaitForSeconds(defenseCooldown);
		defenseReady = true;
	}

	public void EquipShell()
	{
		if (HasShell)
		{
			StopCoroutine(shellCoroutine);
		}
		HasShell = true;
		AudioClip[] clips = GameManager.instance.audioManager.pickupShell;
		AudioClip clip = clips[Random.Range(0, clips.Length)];
		GameManager.instance.audioSource.PlayOneShot(clip);

		shellCoroutine = StartCoroutine(StopShell());
	}

	IEnumerator StopShell()
	{
		yield return new WaitForSeconds(shellDuration);
		HasShell = false;
	}
	#endregion

	private void PlayerController_OnDeath()
	{
		//animator.SetBool("Death", true);
		GameManager.instance.LoseGame();
	}
}
