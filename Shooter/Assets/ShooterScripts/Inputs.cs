using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.Events;

public class Inputs : MonoBehaviour {

	public UnityEvent OnFire, OnShield, OnShieldOff;
	public CapsuleCollider col;
	public bool vrMode;
	public SteamVR_Input_Sources source;

	private Shield shield;

	public SteamVR_Action_Boolean TriggerAction;

	private void Start()
	{
		DisableCol();
		shield = FindObjectOfType<Shield>();
	
	}
	
	void Update() {
		if (vrMode)
		{
			if (TriggerAction.GetStateDown(source))
			{
				OnFire.Invoke();
				Fire();
			}
			/*
			if (SteamVR_Input._default.inActions.Teleport.GetStateDown(SteamVR_Input_Sources.Any) ||
				SteamVR_Input._default.inActions.GrabGrip.GetStateDown(SteamVR_Input_Sources.Any))
			{
				OnShield.Invoke();
			}
			if (SteamVR_Input._default.inActions.Teleport.GetLastStateUp(SteamVR_Input_Sources.Any) ||
				SteamVR_Input._default.inActions.GrabGrip.GetLastStateUp(SteamVR_Input_Sources.Any))
			{
				OnShieldOff.Invoke();
			}
			if (SteamVR_Input._default.inActions.GrabPinch.GetStateDown(SteamVR_Input_Sources.Any))
			{
				OnFire.Invoke();
				Fire();
			}*/
		}
	}

	public void Fire()
	{
		EnableCol();
		Invoke("DisableCol", .2f);
	}

	private void EnableCol()
	{
		col.enabled = true;
	}

	private void DisableCol()
	{
		col.enabled = false;
	}
}
