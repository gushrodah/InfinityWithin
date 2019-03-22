using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : Enemy {

	public float duration;
	float hitWindow = .5f;
	
	public SphereCollider col;
	
	public float speed;
	GameObject player;
	float time = 0;
	
	// to change mat transparency
	//MeshRenderer mesh;

	private void Start()
	{
		base.Start();
		// despawn after time
		StartCoroutine(Despawn());
		StartCoroutine(EnableCollider());

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

	// not working for some reason
	IEnumerator EnableCollider()
	{
		yield return new WaitForSeconds(duration - hitWindow);
		col.enabled = true;
	}

	IEnumerator Despawn()
	{
		yield return new WaitForSeconds(duration);
		time = 0;
		while (time < duration)
		{
			float distance = Vector3.Distance(player.transform.position, transform.position);

			// will only move while the distance is bigger than 1.0 units
			if (distance > .01f)
			{
				Vector3 dir = player.transform.position - transform.position;
				dir.Normalize();                                    // normalization is obligatory
				transform.position += dir * speed * Time.deltaTime; // using deltaTime and speed is obligatory
			}
			time += Time.deltaTime;
			yield return null;
		}
		Destroy(gameObject);
	}
}
