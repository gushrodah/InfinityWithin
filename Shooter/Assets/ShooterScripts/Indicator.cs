using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour {

	public GameObject objToTrack;
	// indicator vars
	private Camera camera;
	private RectTransform rectTrans;
	private Image img;

	private void Start()
	{
		camera = Camera.main;
		//Debug.Log(camera.name);
		rectTrans = (RectTransform)GameObject.Find("HUD").transform;
		//start offscreen
		transform.localPosition = new Vector2(-10,-10);
		if (!objToTrack) Debug.Log("didnt set obj to track on " + gameObject.name);
		img = GetComponent<Image>();
		img.enabled = false;
	}

	public void SetTracker(GameObject _g)
	{
		objToTrack = _g;
	}

	void Update () {

		if (objToTrack != null)
		{
			//adust position
			Vector3 ViewportPosition = camera.WorldToViewportPoint(new Vector3(objToTrack.transform.position.x, objToTrack.transform.position.y, objToTrack.transform.position.z));
			if (ViewportPosition.x > -.5f && ViewportPosition.x < 1.5 &&
				ViewportPosition.y > -.5f && ViewportPosition.y < 1.5)
			{
				if (ViewportPosition.z > 0)
				{
					Vector2 WorldObject_ScreenPosition = new Vector2(
					((ViewportPosition.x * rectTrans.rect.width) - (rectTrans.rect.width * 0.5f)),
					((ViewportPosition.y * rectTrans.rect.height) - (rectTrans.rect.height * 0.5f)));
					transform.localPosition = WorldObject_ScreenPosition;
				}
			}
			// adjust size
			float distance = (objToTrack.transform.position - camera.transform.position).magnitude;
			//Debug.Log(distance);
			if (distance > 0)
			{
				distance *= .1f;
				transform.localScale = new Vector3(1 / distance, 1 / distance, 1 / distance);
			}
			img.enabled = true;
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
