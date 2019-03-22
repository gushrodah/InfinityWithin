using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SonicBloom.Koreo.Demos
{
	// spawn note hits at a randomized position
	public class Spawner : MonoBehaviour
	{
		[EventID]
		public string eventID;
		public GameObject prefab;
		public bool shouldRotate;
		public bool shouldRandomize;
		public List<GameObject> posList;
		public List<GameObject> noteList;

		// TODO: set to player's arm length
		public float xRange,yRange,zRange;
		Koreography playingKoreo;
		
		int index = 0;

		void Start()
		{
			// Register for Koreography Events.  This sets up the callback.
			Koreographer.Instance.RegisterForEvents(eventID, Spawn);

			playingKoreo = Koreographer.Instance.GetKoreographyAtIndex(0);
			noteList = new List<GameObject>();
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

		// called on note event
		void Spawn(KoreographyEvent evt)
		{
			if (index >= posList.Count)
			{
				GameObject g;
				if (shouldRandomize)
				{
					g = Instantiate(prefab, GetRandomPos(), Quaternion.identity);
					
				}
				else
				{
					g = Instantiate(prefab, transform.position, Quaternion.identity);
				}

				if (shouldRotate)
				{
					g.transform.Rotate(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
				}
				noteList.Add(g);
			}
			else
			{
				posList[index].SetActive(true);
				index++;
			}
			
		}

		Vector3 GetRandomPos()
		{
			float x = Random.Range(-xRange, xRange);
			float y = Random.Range(-yRange, yRange);
			float z = Random.Range(-zRange, zRange);
			Vector3 newPos = transform.position + new Vector3(x, y, z);
			return newPos;
		}

		
	}
}
