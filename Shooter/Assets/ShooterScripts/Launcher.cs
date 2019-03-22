using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : Enemy {

	// time before note hit
	public float duration;
	float hitWindow = .5f;
	public MeshRenderer mat;
	Color startCol;
	public SphereCollider col;
	public float minForce, maxForce;
	
	float speed;
	float time = 0;
	Rigidbody rb;

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
		StartCoroutine(Launch());
		StartCoroutine(EnableCollider());
		rb = GetComponent<Rigidbody>();
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

	IEnumerator Launch()
	{
		time = 0;
		if (rb == null) rb = GetComponent<Rigidbody>();
		
		// auto despawn if aver hit window
		while (time < duration+hitWindow)
		{
			rb.AddForce(Vector3.up * Random.Range(minForce, maxForce));
			time += Time.deltaTime;
			yield return null;
		}
		Destroy(gameObject);
	}
}
