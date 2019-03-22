using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt_v2 : Enemy {

	// time before note hit
	public float duration;
	float hitWindow = .5f;
	
	public SphereCollider col;
	
	public float speed;
	GameObject player;
	float time = 0;
	int offset;
	
	// to change mat transparency
	//MeshRenderer mesh;

	private void Start()
	{
		// spawn left or right
		offset = Random.Range(-1, 1);
		//indicator stuff
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
		time = 0;
		// auto despawn if aver hit window
		while (time < duration+hitWindow)
		{
			// null check 4 sometimes happens
			if (player == null)
			{
				player = GameObject.FindGameObjectWithTag("Player");
			}
			float distance = Vector3.Distance(player.transform.position, transform.position);

			// will only move while the distance is bigger than 1.0 units
			if (distance > .01f)
			{
				
				Vector3 pos = new Vector3((player.transform.position.x + ((offset+.5f)*.8f)), player.transform.position.y -.2f, player.transform.position.z);
				//get direction
				Vector3 dir = pos - transform.position;
				dir.Normalize(); 
				//update pos
				transform.position += dir * speed * Time.deltaTime; 
			}
			time += Time.deltaTime;
			yield return null;
		}
		Destroy(gameObject);
	}
}
