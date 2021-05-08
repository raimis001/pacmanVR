using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ITouchable : MonoBehaviour
{
	public Collider collision { get; set; }
  public virtual void OnEnter(XRHand hand)
	{

	}

	public virtual void OnExit(XRHand hand)
	{

	}

	public virtual void OnHand(XRHand hand)
	{

	}
    
}
