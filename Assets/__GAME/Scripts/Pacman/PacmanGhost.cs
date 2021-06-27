using UnityEngine;

public enum GhostMode
{
	Scatter, Chase, Frightened, Prison
}
public enum GhostType
{
	Blinky, Pinky, Inky, Clyde
}



public class PacmanGhost : MonoBehaviour
{
	public GhostType type;
	public float speed = 5;

	public Transform referenceSprite;

	PacmanDirection direction = PacmanDirection.Righ;

	bool moving = true;
	bool makeChoice = false;
	bool started = false;
	bool paused = false;

	PacmanActor actor;
	Pacman pacman;

	Vector3 startPosition;
	Quaternion startRotation;
	int scoreDelta = 0;

	Transform corner =>
		type switch
		{
			GhostType.Blinky => pacman.cornerBlinky,
			GhostType.Pinky => pacman.cornerPinky,
			GhostType.Inky => pacman.cornerInky,
			GhostType.Clyde => pacman.cornerClyde,
			_ => throw new System.NotImplementedException(),
		};

	private void Start()
	{
		pacman = FindObjectOfType<Pacman>();
		startPosition = transform.position;
		startRotation = transform.rotation;
	}

	float pinkyDelay;
	private void Update()
	{
		if (!Pacman.GameStarted)
			return;

		if (paused)
			return;

		if (!started)
		{
			switch (type)
			{
				case GhostType.Blinky:
					started = true;
					break;
				case GhostType.Pinky:
					if (pinkyDelay > 10)
						started = true;
					else
						pinkyDelay += Time.deltaTime;
					break;
				case GhostType.Inky:
					if (pacman.Score - scoreDelta > 30)
						started = true;

					break;
				case GhostType.Clyde:
					if (pacman.Score - scoreDelta > 80)
						started = true;

					break;
			}

			if (started)
			{
				transform.position = pacman.ghostPoint.position;
				RotateGhost(PacmanDirection.Righ);
			}

			return;
		}

		if (moving)
			transform.Translate(Vector3.forward * speed * Time.deltaTime);

		if (!actor || makeChoice)
			return;

		if (!ReachTarget(transform.position, actor.transform.position))
			return;

		MakeChoice();
	}



	void MakeChoice()
	{
		moving = false;
		PacmanDirection targetDir = GetDirection();

		bool CheckDir(PacmanDirection checkFlag, PacmanDirection denyFlag)
		{
			if (direction == denyFlag)
				return false;

			if (!targetDir.HasFlag(checkFlag))
				return false;

			if (!actor.direction.HasFlag(checkFlag))
				return false;
			
			if (direction != checkFlag)
				RotateGhost(checkFlag);

			return true;

		}
		//Debug.Log("Make choce");

		moving = true;
		makeChoice = true;
		if (CheckDir(PacmanDirection.Up, PacmanDirection.Down))
			return;
		if (CheckDir(PacmanDirection.Down, PacmanDirection.Up))
			return;
		if (CheckDir(PacmanDirection.Left, PacmanDirection.Righ))
			return;
		if (CheckDir(PacmanDirection.Righ, PacmanDirection.Left))
			return;

		if (actor.direction.HasFlag(direction))
			return;

		PacmanDirection dir = direction;

		if (direction == PacmanDirection.Up || direction == PacmanDirection.Down)
		{
			if (actor.left)
				dir = PacmanDirection.Left;
			if (actor.right)
				dir = PacmanDirection.Righ;
		}
		else
		{
			if (actor.up)
				dir = PacmanDirection.Up;
			if (actor.down)
				dir = PacmanDirection.Down;
		}

		RotateGhost(dir);
	}

	void RotateGhost(PacmanDirection dir)
	{
		float angle = dir switch
		{
			PacmanDirection.Up => 270,
			PacmanDirection.Left => 180,
			PacmanDirection.Righ => 0,
			PacmanDirection.Down => 90,
			_ => 0,
		};

		transform.eulerAngles = new Vector3(transform.eulerAngles.x, angle, transform.eulerAngles.z);
		direction = dir;
	}
	private void LateUpdate()
	{
		float d = 17.5f / 150f;
		float dx = transform.position.z * d;
		float dy = transform.position.x * d;
		if (referenceSprite)
			referenceSprite.localPosition = new Vector3(dx, -dy, 0);
	}
	PacmanDirection GetDirection()
	{
		Vector2 point = new Vector2(transform.position.x, transform.position.z);
		Vector2 target = new Vector2(corner.position.x, corner.position.z);
		
		if (pacman.mode == GhostMode.Chase) 
		{
			float dx = (pacman.direction == PacmanDirection.Up ? -1 : pacman.direction == PacmanDirection.Down ? 1 : 0);
			float dy = (pacman.direction == PacmanDirection.Left ? -1 : pacman.direction == PacmanDirection.Righ ? 1 : 0);
			switch (type)
			{
				case GhostType.Blinky:
					target = new Vector2(pacman.transform.position.x, pacman.transform.position.z);
					break;
				case GhostType.Pinky:
					target = new Vector2(pacman.transform.position.x + dx * 15, pacman.transform.position.z + dy * 15);
					break;
				case GhostType.Inky:
					target = new Vector2(pacman.transform.position.x + dx * 12, pacman.transform.position.z + dy * 12) * 2;
					break;
				case GhostType.Clyde:
					if (Vector2.Distance(point, target) > 40)
						target = new Vector2(pacman.transform.position.x, pacman.transform.position.z);
					break;
			}
		}

		PacmanDirection result = 0;

		if (target.x < point.x)
			result |= PacmanDirection.Up;
		if (target.x > point.x)
			result |= PacmanDirection.Down;
		if (target.y > point.y)
			result |= PacmanDirection.Righ;
		if (target.y < point.y)
			result |= PacmanDirection.Left;

		return result;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (CheckTeleport(other))
			return;

		if (!other.CompareTag("Actor"))
			return;
	
		//Debug.Log("Enter actor");
		reachDistance = Mathf.Infinity;
		makeChoice = false;
	}
	private void OnTriggerStay(Collider other)
	{
		if (!other.CompareTag("Actor"))
			return;

		actor = other.GetComponent<PacmanActor>();
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
		
		//Debug.Log("Exit actor");
		actor = null;
		makeChoice = false;

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

	bool isTeleported;
	Collider teleportCollider;
	private bool CheckTeleport(Collider other)
	{
		if (!other.CompareTag("Teleport"))
			return false;

		if (isTeleported)
			return true;

		teleportCollider = other;

		isTeleported = true;
		PacmanTeleport teleport = other.GetComponentInParent<PacmanTeleport>();
		transform.position = new Vector3(teleport.targetPoint.position.x, transform.position.y, teleport.targetPoint.position.z);
		return true;
	}

	public void GotoRest(bool restore = true)
	{
		direction = PacmanDirection.Righ;
		transform.SetPositionAndRotation(startPosition, startRotation);
		RotateGhost(PacmanDirection.Righ);
		paused = true;
		started = false;
		
		if (restore)
			Invoke(nameof(Restore), 5f);
	}

	public void Restore()
	{
		//transform.position = pacman.ghostPoint.position;
		scoreDelta = pacman.Score;
		pinkyDelay = 0;
		paused = false;
	
	}

	public void Reset()
	{
		scoreDelta = 0;
		pinkyDelay = 0;
		paused = false;
		started = false;
	}
}
