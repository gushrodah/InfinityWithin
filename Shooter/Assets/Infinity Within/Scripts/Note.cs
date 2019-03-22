using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NoteState
{
	POS,NEG,NEUTRAL
}

// two states : neg  or pos 
[RequireComponent(typeof(ParticleSystem))]
public class Note : MonoBehaviour {

	// TODO: change to auto update speed to get to hand on next note
	public float absorbSpeed;

	public NoteState state = NoteState.NEG;
	// controls all child particles
	private ParticleSystem parentPart;

	// aborbed particles to hand
	public ParticleSystem PosParts, negParts, neutralParts, indicatorParts;
	public GameObject absorbParts;
	
	void Start () {
		parentPart = GetComponent<ParticleSystem>();
	}

	private void ChangeState(NoteState _state)
	{
		if (_state == NoteState.NEUTRAL)
		{
			negParts.Stop();
			neutralParts.gameObject.SetActive(true);
			neutralParts.Play();
			PosParts.Stop();
		}
		else if (_state == NoteState.POS)
		{
			negParts.Stop();
			neutralParts.Stop();
			PosParts.gameObject.SetActive(true);
			PosParts.Play();
		}
	}

	public void OnAbsorbed(Transform _endPos)
	{
		StartCoroutine( ToPos(_endPos));

		// TODO: enable after getting seek to work 
		ChangeState(NoteState.NEUTRAL);
	}

	//TOD
	// move transfer particle to hand
	//change to Coroutine 
	private IEnumerator ToPos(Transform _endPos)
	{
		// instantiate neutral particle
		GameObject g = Instantiate(absorbParts, transform.position, Quaternion.identity);
		//TODO: change speed to hit exactly on next beat
		while (Vector3.Distance(_endPos.position, g.transform.position) > .1f)
		{
			Vector3 dir = _endPos.position - g.transform.position;
			dir.Normalize();                                    // normalization is obligatory
			g.transform.position += dir * absorbSpeed * Time.deltaTime; // using deltaTime and speed is obligatory

			yield return null;
		}
		Destroy(g);
		yield return 0;
	}

	public void Activate()
	{
		indicatorParts.Play();
	}
}
