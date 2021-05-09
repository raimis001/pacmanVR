using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanActor : MonoBehaviour
{
	public bool up = true;
	public bool left = false;
	public bool right = false;
	public bool down = true;

	[SerializeField]
	private GameObject upArrow;
	[SerializeField]
	private GameObject leftArrow;
	[SerializeField]
	private GameObject rightArrow;
	[SerializeField]
	private GameObject downArrow;


	public PacmanDirection direction { get; set; }

	private void OnValidate()
	{
		if (upArrow) upArrow.SetActive(up);
		if (leftArrow) leftArrow.SetActive(left);
		if (rightArrow) rightArrow.SetActive(right);
		if (downArrow) downArrow.SetActive(down);
	}

	private void Start()
	{
		direction = 0;
		if (up)
			direction |= PacmanDirection.Up;
		if (down)
			direction |= PacmanDirection.Down;
		if (right)
			direction |= PacmanDirection.Righ;
		if (left)
			direction |= PacmanDirection.Left;

		upArrow.SetActive(up);
		leftArrow.SetActive(left);
		rightArrow.SetActive(right);
		downArrow.SetActive(down);
	}
#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(transform.position, new Vector3(5, 1, 5));

		UnityEditor.Handles.color = new Color(0.259434f, 0.9613943f, 1, 0.1f);

		float angle = 45;
		if (up)
			UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, Quaternion.AngleAxis(-90 - angle * 0.5f, Vector3.up) * transform.forward, angle, 2);
		if (left)
			UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, Quaternion.AngleAxis(180 - angle * 0.5f, Vector3.up) * transform.forward, angle, 2);
		if (right)
			UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, Quaternion.AngleAxis(0 - angle * 0.5f, Vector3.up) * transform.forward, angle, 2);
		if (down)
			UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, Quaternion.AngleAxis(90 - angle * 0.5f, Vector3.up) * transform.forward, angle, 2);
	}
#endif
}
