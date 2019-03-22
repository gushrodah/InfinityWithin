using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SonicBloom.Koreo;

// keeps track of all board objects and determines which Neg will be the next the activate
public class NoteManager : MonoBehaviour {

	// set at beginning for auto populating env with notes
	public int noteCount;
	// TODO: make private - manually reacting notes for now
	public List<Note> noteList;
	public GameObject notePrefab;
	//just for debugging what note is next
	public string curNote;
	// orgin for note spawning
	public Transform noteOrigin;

	// onBeat event, choose note in list to activate
	public Action<Transform> OnBeatHit;
	// -sets Notes.OnAbsorbed

	private float beatPadding = .25f;
	private float beatTimer = 0f;

	// koreograph stuff
	[EventID]
	public string eventID;
	Koreography playingKoreo;

	int index = 0;

	void Start()
	{
		// Register for Koreography Events.  This sets up the callback.
		Koreographer.Instance.RegisterForEvents(eventID, ActivateCurrentNote);

		playingKoreo = Koreographer.Instance.GetKoreographyAtIndex(0);

		// instantiate and add all notes to list
		noteList = new List<Note>();
		Vector3 newPos = noteOrigin.position;
		for (int i = 0; i < noteCount; i++)
		{
			// TODO: set note to random position
			newPos += new Vector3(UnityEngine.Random.Range(-1,1), UnityEngine.Random.Range(-.5f, .5f), UnityEngine.Random.Range(-1, 1));
			GameObject tempNote = Instantiate(notePrefab, newPos, Quaternion.identity, transform);
			Note note = tempNote.GetComponent<Note>();
			noteList.Add(note);
		}

		curNote = noteList[0].name;
		// sub to events
		OnBeatHit += SuccessfulBeatHit;
	}

	void OnDestroy()
	{
		// Sometimes the Koreographer Instance gets cleaned up before hand.
		//  No need to worry in that case.
		if (Koreographer.Instance != null)
		{
			Koreographer.Instance.UnregisterForAllEvents(this);
		}
	}
	
	void Update () {
		// on event start beattimer
	}

	// check to see if absorb or missed
	public bool IsOnBeat()
	{
		// pressed after event
		if (beatTimer < beatPadding)
		{
			return true;
		}
		// before beat (wait for beat event)
		else
		{
			return false;
		}
	}

	private void SuccessfulBeatHit(Transform _transform)
	{
		if (noteList.Count > 0)
		{
			// set note to launch at hand
			noteList[0].OnAbsorbed(_transform);
			// remove that note from list
			noteList.RemoveAt(0);
			curNote = noteList[0].name;
		}
		else
		{
			Debug.Log("Note notes in list");
		}
	}

	// plays the indicator
	private void ActivateCurrentNote(KoreographyEvent evt)
	{
		noteList[0].Activate();
	}
}
