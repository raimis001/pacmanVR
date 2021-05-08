using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonThrottle : ButtonTouch
{
	public Animator anim;
	public Renderer signal;

	public bool IsOn { get; set; }

	public override void OnEnter(XRHand hand)
	{
		base.OnEnter(hand);
	}

	public override void OnHandAction(XRHand hand)
	{
		anim.SetTrigger("Switch");
		IsOn = !IsOn;
		Invoke(nameof(EnableArrow), 0.2f);
		base.OnHandAction(hand);
	}

	void EnableArrow()
	{
		if (IsOn)
			signal.material.EnableKeyword("_EMISSION");
		else
			signal.material.DisableKeyword("_EMISSION");
	}
}
