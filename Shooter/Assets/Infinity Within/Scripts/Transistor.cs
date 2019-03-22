using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.Events;

public enum Hand { LEFT, RIGHT }

public class Transistor : MonoBehaviour {

	// music manager
	public NoteManager manager;

	//controlelr stuff
	public SteamVR_Input_Sources source;
	public SteamVR_Action_Boolean TriggerAction, TouchpadAction;

	// 100% and can shoot
	public int charge = 0;
	public Hand hand;

	// particles that will shoot out 
	public ParticleSystem LightParticles;

	public UnityEvent<Hand> OnChargedShot;
	private bool chargeReady = false;
	
	void Start () {
		
	}
	
	void Update () {
		// TODO : check absorb neg particles
		if (TriggerAction.GetStateDown(source))
		{
			// call event that successfully hit
			manager.OnBeatHit.Invoke(transform);

			//increment charge
			ChangeCharge(100);
		}
		// check shoot positive particles
		if (chargeReady && TouchpadAction.GetStateDown(source))
		{
			Shoot();
		}
	}

	// change to called internall only (accessible only to intro)
	public void ChangeCharge(int _amnt)
	{
		charge += _amnt;
		// particle effect
		// physical meter

		// rechedmax?
		if (charge >= 100)
		{
			chargeReady = true;
		}
	}

	//TODO: shoot particles
	private void Shoot()
	{
		LightParticles.Play();
		StartCoroutine(StopShooting());
		Debug.Log("shoooot");
	}

	IEnumerator StopShooting()
	{
		yield return new WaitForSeconds(4);
		ResetCharge();
	}

	void ResetCharge()
	{
		Debug.Log("stop");
		charge = 0;
		chargeReady = false;
		LightParticles.Stop();
	}
}
