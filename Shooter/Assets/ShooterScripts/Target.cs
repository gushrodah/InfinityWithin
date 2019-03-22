using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : Enemy {

	// time before note hit
	public float duration;
	float hitWindow = .5f;
	public MeshRenderer mat;
	Color startCol;
	public SphereCollider col;
	public bool randomizeDirection;
	
	float speed;
	float time = 0;

	Vector3 endPos;
	// to change mat transparency
	//MeshRenderer mesh;

	private void Start()
	{
		endPos = new Vector3(transform.position.x + Random.Range(-5, 6), transform.position.y + Random.Range(-5, 6), transform.position.z);
		speed = Random.Range(1, 4) ;
		startCol = mat.material.color;
		//indicator stuff
		base.Start();
		// despawn after time
		StartCoroutine(Despawn());
		StartCoroutine(EnableCollider());
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
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

		bool gotDir = false;
		Vector3 dir = Vector3.zero ;
		// auto despawn if aver hit window
		while (time < duration+hitWindow)
		{
			if (!gotDir)
			{
				//get direction
				if (randomizeDirection)
				{
					dir = endPos - transform.position;
					dir.Normalize();
				}
				else
				{
					dir = Vector3.up;
				}
			}
			//update pos
			transform.position += dir * speed * Time.deltaTime;

			//update material indicator
			mat.material.color = Color.Lerp(startCol, Color.white, time / duration);

			time += Time.deltaTime;
			yield return null;
		}
		Destroy(gameObject);
	}
}
