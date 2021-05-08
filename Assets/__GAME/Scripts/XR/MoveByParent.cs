using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByParent : MonoBehaviour
{
  public Transform follow;
	public bool axisX = true;
	public bool axisY = false;
	public bool axisZ = true;

	private void LateUpdate()
	{
		if (!follow)
			return;

		transform.position = new Vector3(axisX ? follow.position.x : transform.position.x,axisY ? follow.position.y : transform.position.y,axisZ ? follow.position.z : transform.position.z);
		//transform.eulerAngles = new Vector3(0, follow.eulerAngles.y, 0);
	}
}
