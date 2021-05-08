using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuButton : ButtonRay
{
	public TMP_Text buttonText;
	public Color selectedColor = Color.green;
	public Color normalColor = Color.white;

	public override void OnEnter(XRHand hand)
	{
		buttonText.color = selectedColor;
		base.OnEnter(hand);
	}
	public override void OnExit(XRHand hand)
	{
		buttonText.color = normalColor;
		base.OnExit(hand);
	}
}
