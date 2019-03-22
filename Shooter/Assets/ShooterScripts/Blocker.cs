using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : Enemy {

	public float duration;
	float hitWindow = .5f;
	
	public float scaleRate;
	
	GameObject player;
	float time = 0;

	public GameObject beam;
	// to change mat transparency
	//MeshRenderer mesh;

	private void Start()
	{
		base.Start();
		// despawn after time
		StartCoroutine(ActivateBeam());
		Invoke("Despawn", duration + hitWindow);

		/*mesh = Indicator.GetComponent<MeshRenderer>();
		Color c = mesh.material.color;
		mesh.material.color = new Color(c.r, c.g, c.b, 0);*/
		//col.enabled = false;
		
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update () {

		// adjust transparency
		/*Color c = mesh.material.color;
		float newAlpha = c.a + (duration * Time.deltaTime);
		mesh.material.color = new Color(c.r, c.g, c.b, newAlpha);*/
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			//GameObject hitObj = Instantiate(Hit, transform.position, Quaternion.identity);
			//Destroy(hitObj, 1);
			Destroy(gameObject);
		}
	}

	IEnumerator ActivateBeam()
	{
		yield return new WaitForSeconds(duration);
		beam.transform.LookAt(player.transform);
		beam.SetActive(true);
	}

	void Despawn()
	{
		Destroy(gameObject);
	}
}
