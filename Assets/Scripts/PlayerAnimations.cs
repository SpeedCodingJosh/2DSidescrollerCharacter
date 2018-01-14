using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour 
{
	private Animator anim;

	private void Start ()
	{
		anim = GetComponent<Animator>();
	}

	public void SetValue (string key, object value = null)
	{
		if(value == null) // No paramenter sent
		{
			anim.SetTrigger(key);
		}
		else if(value.GetType() == typeof(float))
		{
			anim.SetFloat(key, (float)value);
		}
		else if(value.GetType() == typeof(int))
		{
			anim.SetInteger(key, (int)value);
		}
		else if(value.GetType() == typeof(bool))
		{
			anim.SetBool(key, (bool)value);
		}
	}
}
