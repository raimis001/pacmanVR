using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonTouch : ITouchable
{
	public int buttonID;

	public UnityEvent<ButtonTouch, XRHand> OnAction;
	public UnityEvent<ButtonTouch, XRHand> OnButtonHand;
	public UnityEvent<ButtonTouch, XRHand> OnButtonEnter;
	public UnityEvent<ButtonTouch, XRHand> OnButtonExit;

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
			OnHandAction(hand);
	}

	public virtual void OnHandAction(XRHand hand)
	{
		OnAction.Invoke(this, hand);
	}
}
