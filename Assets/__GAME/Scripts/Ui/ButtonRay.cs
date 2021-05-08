using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonRay : IRaycaster
{
	public UnityEvent<ButtonRay, XRHand> OnAction;
	public UnityEvent<ButtonRay, XRHand> OnButtonHand;
	public UnityEvent<ButtonRay, XRHand> OnButtonEnter;
	public UnityEvent<ButtonRay, XRHand> OnButtonExit;

	public override void OnEnter(XRHand hand)
	{
		OnButtonEnter.Invoke(this, hand);
	}
	public override void OnExit(XRHand hand)
	{
		OnButtonExit.Invoke(this, hand);
	}

	public override void OnHand(XRHand hand)
	{
		OnButtonHand.Invoke(this, hand);
		if (hand.triggerDown)
			OnAction.Invoke(this, hand);
	}
}
