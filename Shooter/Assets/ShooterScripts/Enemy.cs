using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour {

	public GameObject indicatorPrefab;
	public GameObject deathParticles, hitPlayerParticles;
	public bool canShoot, useIndicator;

	public virtual void Start () {
		if (useIndicator)
		{
			GameObject ind = Instantiate(indicatorPrefab, (RectTransform)GameObject.Find("HUD").transform);
			ind.GetComponent<Indicator>().SetTracker(gameObject);
		}
		//Destroy(ind,2);
	}

	private void OnTriggerStay(Collider other)
	{
		GameObject part;
		if ((other.tag == "Gun" && canShoot) || other.tag == "Shield")
		{
			part = Instantiate(hitPlayerParticles, transform.position, Quaternion.identity) as GameObject;
			Destroy(part, 1);
			Destroy(gameObject);
		}
		
	}
	// TODO: create virtual functions for before and after note hit
}
