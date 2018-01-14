using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour 
{
	public float horizontal;
	public bool isRunning, jumped;

	private void Update ()
	{
		horizontal = Input.GetAxis("Horizontal");

		isRunning = Input.GetKey(KeyCode.LeftShift);

		jumped = Input.GetKeyDown(KeyCode.Space);
	}

	public bool AreWeMoving ()
	{
		return Input.GetButton("Horizontal");
	}
}
