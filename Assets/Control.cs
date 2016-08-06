using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Control : MonoBehaviour {
	
	public GameObject crossPrefab;
	public Vector3 target;

	private float distance = 80f;
	private float xSpeed = 250f;
	private float ySpeed = 120f;
	private float yMinLimit = -90f;
	private float yMaxLimit = 90f;
	private float x = 0f;
	private float y = 0f;

	private delegate void dUpdate ();
	private dUpdate update;
	private GameObject cross;


	void OnGUI () {
		string orthMod = "Off";
		if (camera.orthographic)
			orthMod = "On";
		if (GUI.Button (new Rect (10, 10, 120, 20), "(F2) ORTH - " + orthMod))
			switchOrthographic();

		string obsMod = "Off";
		if (update == updateFPS)
			obsMod = "On";
		if (GUI.Button (new Rect (140, 10, 110, 20), "(F3) OBS - " + obsMod))
			switchViewMethod ();
	}

	void Awake () {
		cross = Instantiate(crossPrefab) as GameObject;
	}

	void Start () {
		List<GameObject> stars = GetComponent<Map>().stars;
		if (stars.Count > 0)
				target = stars[0].transform.position;
			else
				target = new Vector3(500, 500, 500);

		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;

		update = updateTPS;
		camera.orthographic = false;
		cross.SetActive (false);
	}
	
	void Update () {
		updateMouse ();
		update ();
		
		if (Input.GetKeyDown (KeyCode.F2)) {
			switchOrthographic();
		}
		if (Input.GetKeyDown (KeyCode.F3))
			switchViewMethod ();
	}
	
	void switchOrthographic() {
		if (update == updateTPS) {
			camera.orthographic = !camera.orthographic;
		}
	}

	void switchViewMethod() {
		if (update == updateFPS) {
			update = updateTPS;
			cross.SetActive (false);
		} else {
			if (camera.orthographic)
				camera.orthographic = false;
			update = updateFPS;
			cross.SetActive (true);
		}
	}
	
	private void updateMouse () {
		float wheel = Input.GetAxis ("Mouse ScrollWheel");

		if (wheel < 0)
				distance *= 1.1f;
		if (wheel > 0)
				distance *= 0.9f;

		if (Input.GetMouseButton (1)) {
				x += Input.GetAxis ("Mouse X") * xSpeed * 0.02f;
				y -= Input.GetAxis ("Mouse Y") * ySpeed * 0.02f;
		}

		y = ClampAngle (y, yMinLimit, yMaxLimit);
	}
	
	private void updateFPS () {
		Quaternion rotation = Quaternion.Euler (y, x, 0f);
		var position = rotation * new Vector3 (0.0f, 0.0f, 1) + target;
		
		transform.rotation = rotation;
		transform.position = position;
	}
	
	private void updateTPS () {
		Quaternion rotation = Quaternion.Euler (y, x, 0f);
		var position = rotation * new Vector3 (0.0f, 0.0f, -distance) + target;

		transform.rotation = rotation;
		transform.position = position;

		if (camera.orthographic)
			camera.orthographicSize = distance;
	}

	static float ClampAngle (float angle, float min, float max) {
		if (angle < -360f)
			angle += 360f;
		if (angle > 360f)
			angle -= 360f;
		return Mathf.Clamp (angle, min, max);
	}
}
