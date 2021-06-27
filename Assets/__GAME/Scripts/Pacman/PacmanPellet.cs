using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanPellet : MonoBehaviour
{

	internal Transform refence;

	Pacman pacman;

	private void Awake()
	{
		pacman = FindObjectOfType<Pacman>();
		pacman.pellets.Add(this);


	}
	void OnEnable()
	{
		pacman.CreatePellet(this);
		Pos();
	}

	private void OnDisable()
	{
		if (refence)
			refence.gameObject.SetActive(false);

	}

	// Update is called once per frame
	void Pos()
	{
		float d = 17.5f / 150f;
		float dx = transform.position.z * d;
		float dy = transform.position.x * d;

		if (refence)
			refence.localPosition = new Vector3(dx, -dy, 0);

	}
}
