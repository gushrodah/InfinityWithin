using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour {

	public List<Note> NoteList;

	public Transistor left, right;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// TODO: on right trigger pull
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Absorb(Hand.RIGHT);
		}
	}

	void Absorb(Hand _hand)
	{
		foreach (Note note in NoteList)
		{
			if (_hand == Hand.RIGHT)
			{
				note.OnAbsorbed(right.transform);
			}
			if (_hand == Hand.LEFT)
			{
				note.OnAbsorbed(left.transform);
			}
		}

		if (_hand == Hand.LEFT)
		{
			left.ChangeCharge(100);
		}
		else if (_hand == Hand.RIGHT)
		{
			right.ChangeCharge(100);
		}
	}
}
