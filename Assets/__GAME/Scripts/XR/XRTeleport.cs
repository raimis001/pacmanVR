using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class XRTeleport : MonoBehaviour
{
	public int segments;
	public float segmentLenght;
	public LayerMask layer;

	public LineRenderer line;

	public GameObject marker;

	public TMP_Text debugText;
	public TMP_Text debugText1;

	public Material allowMaterial;
	public Material denyMaterial;
	public Renderer blockRenderer;

	public Vector2 gridSize = new Vector2(4.5f, 9f);
	Vector2 gridHalfSize;

	

	Vector3 teleportPosition;
	bool isTeleport;

	public List<GameObject> levels;

	private void Start()
	{
		marker.SetActive(false);
		gridHalfSize = gridSize * 0.5f;
	}

	private void Update()
	{

		isTeleport = FindTeleport(out teleportPosition);
		if (!isTeleport)
		{
			line.positionCount = 0;
			marker.SetActive(false);
			return;
		}



		if (debugText1)
			debugText1.text = teleportPosition.ToString();

	}


	bool FindTeleport(out Vector3 pos)
	{
		line.positionCount = segments + 1;
		Vector3 position = line.transform.position;
		line.SetPosition(0, position);
		float angle = 1f / (segments - 1);

		for (int i = 0; i < segments; i++)
		{
			Vector3 direction = (line.transform.forward + Vector3.down * angle * i).normalized * segmentLenght;

			if (Physics.Raycast(position, direction, out RaycastHit hit, segmentLenght))
			{
				line.positionCount = i + 2;
				line.SetPosition(i + 1, hit.point);

				pos = hit.point;

				return layer == (layer | (1 << hit.collider.gameObject.layer));
			}

			position += direction;
			line.SetPosition(i + 1, position);

		}
		pos = Vector3.zero;
		return false;
	}
}
