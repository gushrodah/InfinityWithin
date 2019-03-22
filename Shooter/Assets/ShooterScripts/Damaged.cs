using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damaged : MonoBehaviour {

	public UnityEvent OnDamged; 

	MeshRenderer mr;

	private void Start()
	{
		mr = GetComponent<MeshRenderer>();
	}

	private void OnTriggerStay(Collider other)
	{
		Damage();
	}

	public void Damage()
	{
		mr.enabled = true;
		StartCoroutine(Disable());
		OnDamged.Invoke();
	}

	IEnumerator Disable()
	{
		yield return new WaitForSeconds(.1f);
		mr.enabled = false;
	}
}
