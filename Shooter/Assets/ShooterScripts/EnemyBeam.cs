using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeam : MonoBehaviour {
	
	private void OnParticleCollision(GameObject other)
	{
		if (other.tag == "Player")
		{
			other.GetComponent<Damaged>().Damage();
		}
		else
		{
			other.GetComponent<Shield>().OnBlocked.Invoke();
		}
	}
}
