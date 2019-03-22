using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shield : MonoBehaviour {

	public float duration;
	public bool holdToActivate;

	public UnityEvent OnBlocked;
	public GameObject partSys;
	Material mat;

	private void Start()
	{
		OnBlocked.AddListener(Blocked);
		mat = GetComponent<MeshRenderer>().material;
	}

	public void Acitvate()
	{
		gameObject.SetActive(true);
		if (!holdToActivate)
		{
			Invoke("Deactivate", duration);
		}
	}

	public void Deactivate()
	{
		gameObject.SetActive(false);
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Block")
			OnBlocked.Invoke();
	}

	void Blocked()
	{
		GameObject g = Instantiate(partSys, transform.position, Quaternion.identity);
		Destroy(g, 1);

		// light up shield for a sec
		Color c = mat.color;
		mat.color = new Color(c.r, c.g, c.b, c.a + .5f);
		Invoke("ChangeBackMat", duration);
	}

	void ChangeBackMat()
	{
		Color c = mat.color;
		mat.color = new Color(c.r, c.g, c.b, c.a - .5f);
	}
}
