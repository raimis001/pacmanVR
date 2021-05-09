using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Flags]
public enum PacmanDirection
{
	Up = 1 << 0,
	Left = 1 << 1,
	Righ = 1 << 2,
	Down = 1 << 3
}
public class Pacman : MonoBehaviour
{
	private int _score;
	public int Score
	{
		get => _score;
		set
		{
			_score = value;
			if (pointText)
				pointText.text = _score.ToString();
		}
	}

	public Transform playerPoint;
	public XRMove playerPosition;

	public Animator anim;

	public float speed = 1;

	public Transform rotatePoint;
	public float rotateSpeed = 0.5f;


	public Transform ghostPoint;

	[Header("Sprites")]
	public Transform referenceSprite;
	public Transform referenceParent;
	public Transform refencePellet;

	[Header("Corners")]
	public Transform cornerBlinky;
	public Transform cornerPinky;
	public Transform cornerInky;
	public Transform cornerClyde;

	[Header("UI")]
	public Renderer leftArrow;
	public Renderer rightArrow;
	public Renderer straightArrow;
	public TMP_Text pointText;
	public Renderer radioPanel;
	public TMP_Text modeText;

	[Header("Audio")]
	public AudioSource audioRadio;
	public AudioClip clipScatter;
	public AudioClip clipChase;
	public AudioSource audioHit;
	public AudioSource audioRotate;
	public AudioSource auidoMove;

	[Header("Debug")]
	public bool moving;
	public bool prepareRight;
	public bool prepareLeft;
	public bool debugStart;

	internal GhostMode mode = GhostMode.Scatter;
	readonly float[] times = new float[] { 20, 50, 5, 10 };
	float playTime = 0;

	public static bool GameStarted = false;

	internal PacmanDirection direction = PacmanDirection.Righ;


	//Vector3 moveVector =>
	//	direction switch
	//	{
	//		PacmanDirection.Up => new Vector3(1, 0, 0),
	//		PacmanDirection.Left => new Vector3(0, 0, -1),
	//		PacmanDirection.Righ => new Vector3(0, 0, 1),
	//		PacmanDirection.Down => new Vector3(-1, 0, 0),
	//		_ => throw new System.NotImplementedException(),
	//	};
	PacmanActor currentActor;
	bool makeChoice;
	private void Start()
	{
		modeText.text = "stop";
	}
	private void Update()
	{
		if (!GameStarted)
		{
#if UNITY_EDITOR
			if (debugStart)
			{
				GameStarted = true;
				Debug.Log("Start game, mode: " + mode);
			}
#endif
			return;
		}

		playTime += Time.deltaTime;
		if (playTime >= times[(int)mode])
		{
			mode = mode == GhostMode.Chase ? GhostMode.Scatter : GhostMode.Chase;
			playTime = 0;
			ChangeMode();
		}


		MovePlayer();

		if (moving)
		{
			transform.Translate(Vector3.forward * Time.deltaTime * speed);
			if (!auidoMove.isPlaying)
				auidoMove.Play();
		} else
		{
			if (auidoMove.isPlaying)
				auidoMove.Stop();
		}

		if (!currentActor || makeChoice)
			return;

		if (!ReachTarget(rotatePoint.position, currentActor.transform.position))
			return;

		transform.position = new Vector3(currentActor.transform.position.x, transform.position.y, currentActor.transform.position.z);
		moving = false;

		//Debug.LogFormat("Can rotate dir: {0} UP {1}", direction, currentActor.up);
		MakeChoice();
	}

	float reachDistance = Mathf.Infinity;
	bool ReachTarget(Vector3 point, Vector3 target)
	{
		Vector2 start = new Vector2(point.x, point.z);
		Vector2 end = new Vector2(target.x, target.z);

		float dist = Vector2.Distance(start, end);
		if (dist >= reachDistance)
			return true;

		reachDistance = dist;
		return false;

	}
	void MakeChoice()
	{
		moving = false;
		makeChoice = false;

		if (!prepareLeft && !prepareRight)
		{
			if (currentActor.direction.HasFlag(direction))
			{
				makeChoice = true;
				moving = true;
				return;
			}

			return;
		}

		PacmanDirection dir = direction;
		if (direction == PacmanDirection.Righ)
		{
			if (prepareLeft && currentActor.up)
				dir = PacmanDirection.Up;
			if (prepareRight && currentActor.down)
				dir = PacmanDirection.Down;
		}
		else if (direction == PacmanDirection.Left)
		{
			if (prepareLeft && currentActor.down)
				dir = PacmanDirection.Down;
			if (prepareRight && currentActor.up)
				dir = PacmanDirection.Up;
		}
		else if (direction == PacmanDirection.Up)
		{
			if (prepareLeft && currentActor.left)
				dir = PacmanDirection.Left;
			if (prepareRight && currentActor.right)
				dir = PacmanDirection.Righ;
		}
		else if (direction == PacmanDirection.Down)
		{
			if (prepareLeft && currentActor.right)
				dir = PacmanDirection.Righ;
			if (prepareRight && currentActor.left)
				dir = PacmanDirection.Left;
		}

		if (dir == direction)
		{
			if (currentActor.direction.HasFlag(direction))
			{
				makeChoice = true;
				moving = true;
				return;
			}
			return;
		}

		makeChoice = true;
		prepareRight = false;
		prepareLeft = false;
		EnableArrows();

		StartCoroutine(RotatePacman(dir));
	}

	IEnumerator RotatePacman(PacmanDirection dir)
	{
		audioRotate.Play();

		float angle = 0;
		if (dir == PacmanDirection.Righ)
			angle = 0;
		if (dir == PacmanDirection.Left)
			angle = 180;
		if (dir == PacmanDirection.Up)
			angle = 270;
		if (dir == PacmanDirection.Down)
			angle = 90;

		Vector3 cAngle = transform.eulerAngles;
		Vector3 a = cAngle;
		float pDelta = Mathf.DeltaAngle(playerPosition.transform.eulerAngles.y, angle) / (rotateSpeed / Time.deltaTime);
		float time = rotateSpeed;
		while (time > 0)
		{
			a.y = Mathf.LerpAngle(cAngle.y, angle, 1 - time / rotateSpeed);
			transform.eulerAngles = a;

			playerPosition.RotateAround(pDelta);

			yield return null;
			time -= Time.deltaTime;
		}
		playerPosition.RotateAround(pDelta);

		a.y = angle;
		transform.eulerAngles = a;
		direction = dir;
		moving = true;

	}

	private void LateUpdate()
	{
		float d = 17.5f / 150f;
		float dx = transform.position.z * d;
		float dy = transform.position.x * d;
		if (referenceSprite)
			referenceSprite.localPosition = new Vector3(dx, -dy, 0);



		if (!playerPoint || !playerPosition)
			return;

		playerPosition.transform.position = playerPoint.position;
	}

	void MovePlayer()
	{
		Vector3 delta = new Vector3(XRManager.LeftHand.joystick.x, 0, XRManager.LeftHand.joystick.y);
		if (delta.magnitude < 0.1f)
			return;
		playerPoint.Translate(delta * Time.deltaTime * 0.1f);
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (!collision.gameObject.CompareTag("Eats"))
			return;

		audioHit.Play();
		Score += 10;
		anim.SetTrigger("Eat");
		Destroy(collision.gameObject);
	}

	bool isTeleported;
	Collider teleportCollider;
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Ghost"))
		{
			Die();
		}

		if (!other.CompareTag("Teleport"))
			return;
		if (isTeleported)
			return;

		teleportCollider = other;

		isTeleported = true;
		PacmanTeleport teleport = other.GetComponentInParent<PacmanTeleport>();
		transform.position = new Vector3(teleport.targetPoint.position.x, transform.position.y, teleport.targetPoint.position.z);
	}
	private void Die()
	{
		throw new NotImplementedException();
	}

	private void OnTriggerStay(Collider other)
	{
		if (!other.CompareTag("Actor") || makeChoice)
			return;

		if (!currentActor)
		{
			reachDistance = Mathf.Infinity;
			//	Debug.Log("Enter actor");
		}

		currentActor = other.GetComponent<PacmanActor>();
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Teleport"))
		{
			if (other != teleportCollider)
				isTeleported = false;

			return;
		}

		if (!other.CompareTag("Actor"))
			return;

		currentActor = null;
		makeChoice = false;
		reachDistance = Mathf.Infinity;
		//Debug.Log("Exit actor");
	}

	public void OnDirectionSwitch(ButtonTouch button, XRHand hand)
	{
		switch (button.buttonID)
		{
			case 0:
				prepareLeft = false;
				prepareRight = false;
				if (!GameStarted)
				{
					GameStarted = true;
					moving = true;
					modeText.text = mode.ToString();

				}

				break;
			case 1:
				prepareLeft = !prepareLeft;
				prepareRight = false;
				break;
			case 2:
				prepareLeft = false;
				prepareRight = !prepareRight;
				break;
		}
		EnableArrows();
	}

	void EnableArrows()
	{
		if (moving)
			straightArrow.material.EnableKeyword("_EMISSION");
		else
			straightArrow.material.DisableKeyword("_EMISSION");

		if (prepareLeft)
			leftArrow.material.EnableKeyword("_EMISSION");
		else
			leftArrow.material.DisableKeyword("_EMISSION");

		if (prepareRight)
			rightArrow.material.EnableKeyword("_EMISSION");
		else
			rightArrow.material.DisableKeyword("_EMISSION");

	}

	bool isRadio;
	public void OnRadioSwith(ButtonTouch button, XRHand hand)
	{
		isRadio = !isRadio;
		if (!radioPanel)
			return;

		if (isRadio)
			radioPanel.material.EnableKeyword("_EMISSION");
		else
			radioPanel.material.DisableKeyword("_EMISSION");

		ChangeMode(0);
	}

	internal void ChangeMode(float delay = 0)
	{
		//Debug.Log("Change mode to: " + mode);
		modeText.text = mode.ToString();

		audioRadio.Stop();
		audioRadio.clip = mode == GhostMode.Chase ? clipChase : clipScatter;

		if (!isRadio)
			return;

		if (delay > 0)
			audioRadio.Play((ulong)delay);
		else
			audioRadio.Play();

	}

	public void CreatePellet(PacmanPellet pellet)
	{
		pellet.refence = Instantiate(refencePellet, referenceParent);
		pellet.refence.gameObject.SetActive(true);
	}

}
