using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRManager : MonoBehaviour
{
	public static XRHand LeftHand;
	public static XRHand RightHand;

	public XRHand leftHand;
	public XRHand rightHand;

	public static Controllers Input;
	Controllers input;

	private void Awake()
	{
		input = new Controllers();
		Input = input;
		LeftHand = leftHand;
		RightHand = rightHand;
	}

	private void OnEnable()
	{
		input.Enable();
	}

	private void OnDisable()
	{
		input.Disable();
	}
}
