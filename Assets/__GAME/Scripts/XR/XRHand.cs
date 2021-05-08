using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HandKind
{
	Left, Right
}

public class XRHand : MonoBehaviour
{
	public HandKind kind;
	[Header("Raycast")]
	public LayerMask raycastLayer;
	public Transform raycastPoint;
	public float raycastLenght;
	public bool alwaysShowLine;
	public Transform reaycastEnd;
	public LineRenderer raycastLine;
	private Ray raycast => new Ray(raycastPoint.position, raycastPoint.forward);

	[Header("Touchable")]
	public LayerMask touchLayer;
	public Transform touchPoint;
	public float touchRadius = 0.3f;

	public Vector2 joystick =>
		kind == HandKind.Left ?
			XRManager.Input.Left.joystick.ReadValue<Vector2>() :
			XRManager.Input.Right.joystick.ReadValue<Vector2>();

	public bool triggerDown =>
		kind == HandKind.Left ?
		XRManager.Input.Left.triggerDown.triggered :
		XRManager.Input.Right.triggerDown.triggered;

	ITouchable touchable;
	IRaycaster raycaster;
	readonly List<ITouchable> touchables = new List<ITouchable>();
	
	private void Update()
	{
		OperateRaycast();
		OperateTouchable();
	}

	void OperateRaycast()
	{
		void disableRay()
		{
			if (raycaster)
				raycaster.OnExit(this);
			if (raycastLine)
				raycastLine.positionCount = 0;
			raycaster = null;

			if (alwaysShowLine && raycastLine)
			{
				raycastLine.useWorldSpace = true;
				raycastLine.positionCount = 2;
				raycastLine.SetPosition(0, raycastPoint.position);
				raycastLine.SetPosition(1, raycastPoint.position + raycastPoint.forward * raycastLenght);
			}
			if (reaycastEnd)
				reaycastEnd.gameObject.SetActive(false);
		}

		if (raycastLayer == 0)
		{
			disableRay();
			return;
		}


		if (!Physics.Raycast(raycast, out RaycastHit hit, raycastLenght, raycastLayer))
		{
			disableRay();
			return;
		}
		IRaycaster c = hit.collider.GetComponentInParent<IRaycaster>();
		if (!c)
		{
			disableRay();
			return;
		}
		if (raycastLine)
		{
			raycastLine.useWorldSpace = true;
			raycastLine.positionCount = 2;
			raycastLine.SetPosition(0, raycastPoint.position);
			raycastLine.SetPosition(1, hit.point);
			if (reaycastEnd)
			{
				reaycastEnd.position = hit.point;
				reaycastEnd.gameObject.SetActive(true);
			}

		}

		c.collision = hit.collider;

		if (c == raycaster)
		{
			raycaster.OnHand(this);
			return;
		}

		if (raycaster)
			raycaster.OnExit(this);

		raycaster = c;
		raycaster.OnEnter(this);

	}

	void OperateTouchable()
	{
		void ExitTouch()
		{
			if (!touchable)
				return;

			touchable.OnExit(this);
			touchPoint.GetComponent<Renderer>().material.color = Color.white;
			touchable = null;
		}

		if (touchLayer == 0)
			return;

		Collider[] touches = Physics.OverlapSphere(touchPoint.position, touchRadius, touchLayer);
		if (touches.Length < 1)
		{
			ExitTouch();
			return;
		}

		touchables.Clear();
		foreach (Collider c in touches)
		{
			ITouchable touch = c.gameObject.GetComponentInParent<ITouchable>();
			if (!touch)
				continue;
			touch.collision = c;
			touchables.Add(touch);
		}
		if (touchables.Count < 1)
		{
			ExitTouch();
			return;
		}

		touchables.Sort((t1, t2) => (Vector3.Distance(touchPoint.position, t1.transform.position).CompareTo(Vector3.Distance(touchPoint.position, t2.transform.position))));

		if (touchable == touchables[0])
		{
			touchable.OnHand(this);
			touchPoint.GetComponent<Renderer>().material.color = Color.red;
			return;
		}

		if (touchable)
			ExitTouch();

		touchable = touchables[0];
		touchable.OnEnter(this);
	}

	private void OnDrawGizmos()
	{
		if (!touchPoint)
			return;

		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(touchPoint.position, touchRadius);
	}
}
