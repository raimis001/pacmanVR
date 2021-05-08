using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IButton 
{
	public void OnAction(ButtonTouch button, XRHand hand);
	public void OnButtonHand(ButtonTouch button, XRHand hand);
	public void OnButtonEnter(ButtonTouch button, XRHand hand);
	public void OnButtonExit(ButtonTouch button, XRHand hand);
}
