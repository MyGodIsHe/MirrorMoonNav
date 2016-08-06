using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour {

	public string title;
	public GameObject guiTitlePrefab;

	private GameObject guiTitle;
	private Control control;
	private float defScale = 20;
	private float radius;
	
	void Awake () {
		control = Camera.main.GetComponent<Control>();
		radius = GetComponent<SphereCollider>().radius;
	}
	
	void Update () {
		transform.rotation = Camera.main.transform.rotation;
		if (Camera.main.orthographic) {
			float t = Vector3.Distance (control.target, transform.position) / 200;
			float scale = Mathf.Lerp (defScale * 2f, defScale * 0.5f, t);
			transform.localScale = new Vector3 (scale, scale, scale);
		} else {
			transform.localScale = new Vector3 (defScale, defScale, defScale);
		}
		if (guiTitle) {
			Vector3 textPosition = transform.position + Camera.main.transform.right * defScale * radius;
			Vector3 screenPoint = Camera.main.WorldToScreenPoint (textPosition);
			guiTitle.transform.position = Camera.main.ScreenToViewportPoint (screenPoint);
		}
	}
	
	void OnBecameVisible() {
		if (guiTitle)
			guiTitle.SetActive (true);
	}

	void OnBecameInvisible() {
		if (guiTitle)
			guiTitle.SetActive (false);
	}

	void OnDestroy() {
		if (guiTitle != null)
			Destroy(guiTitle);
	}

	void OnMouseDown() {
		if (guiTitle == null) {
			guiTitle = Instantiate(guiTitlePrefab) as GameObject;
			guiTitle.guiText.text = title;
		} else {
			Destroy(guiTitle);
		}
	}
}
