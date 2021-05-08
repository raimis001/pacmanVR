using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRMove : MonoBehaviour
{
	static XRMove instance;

	[Header("Move")]
	public float moveSpeed = 3;
	public bool moveX = true;
	public bool moveY = false;

	[Header("Rotate")]
	public float rotAngle = 10;
	public float rotTime = 0.1f;
	public float rotDelay = 0.05f;

	bool isRotate;


	Rigidbody rigi;
	Transform cameraTransform;

	private void Awake()
	{
		instance = this;

		rigi = GetComponent<Rigidbody>();
		cameraTransform = Camera.main.transform;
	}

	private void FixedUpdate()
	{
		Vector3 axis = new Vector3(moveX ? XRManager.LeftHand.joystick.x : 0, 0, moveY ? XRManager.LeftHand.joystick.y : 0);
		Vector3 step = Quaternion.AngleAxis(cameraTransform.eulerAngles.y, Vector3.up) * axis;
		step.y = 0;

		rigi.velocity = step * moveSpeed;
	}

	internal static void Teleport(Vector3 teleportPosition)
	{
		Vector3 pos = teleportPosition - instance.cameraTransform.localPosition;
		pos.y = instance.transform.position.y;

		instance.transform.position = pos;
	}

	private void Update()
	{
		Rotate();
	}

	void Rotate()
	{
		if (isRotate)
			return;
		if (Mathf.Abs(XRManager.RightHand.joystick.x) < 0.75f)
			return;

		StartCoroutine(Rotate(XRManager.RightHand.joystick.x));
	}
	public void RotateAround(float angle)
	{
		//Debug.Log("Rotate to " + angle.ToString("0.00"));
		transform.RotateAround(cameraTransform.position, Vector3.up, angle);
	}
	IEnumerator Rotate(float direction)
	{
		isRotate = true;
		int steps = Mathf.CeilToInt(rotTime / Time.deltaTime);
		float angle = (rotAngle / steps) * Mathf.Sign(direction);

		for (int i = 0; i < steps; i++)
		{
			transform.RotateAround(cameraTransform.position, Vector3.up,  angle);
			yield return null;
		}

		yield return new WaitForSeconds(rotDelay);
		isRotate = false;
	}

}
