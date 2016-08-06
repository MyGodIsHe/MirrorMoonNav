using UnityEngine;
using System.Collections;

public class MapGUI : MonoBehaviour {
	
	private string searchField = string.Empty;
	private bool isActive = false;
	private Rect fieldRect;
	private int fieldWidth = 120;
	private int fieldHeight = 20;
	private Map mapComponent;
	private Control controlComponent;
	
	void Awake () {
		Vector3 screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0));
		fieldRect = new Rect (screenCenter.x - fieldWidth / 2,
		                      screenCenter.y - fieldHeight / 2,
		                      fieldWidth, fieldHeight);
		mapComponent = GetComponent<Map>();
		controlComponent= GetComponent<Control>();
	}
	
	void OnGUI (){
		if (isActive) {
			GUI.SetNextControlName ("SearchField");
			searchField = GUI.TextField(fieldRect, searchField, 6);
			GUI.FocusControl ("SearchField");


			Event e = Event.current;
			if (e.keyCode == KeyCode.Return) {
				if (searchField != string.Empty) {
					GameObject star = mapComponent.Search(searchField);
					if (star != null) {
						controlComponent.target = star.transform.position;
						searchField = string.Empty;
						isActive = false;
					}
				}
			} else if (e.keyCode == KeyCode.Escape) {
				isActive = false;
			}
		}

		if (GUI.Button (new Rect (260, 10, 130, 20), "(ENTER) SEARCH"))
			isActive = !isActive;
	}
	
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return))
			isActive = !isActive;
		if (Input.GetKeyDown (KeyCode.Escape))
			isActive = false;
	}
}
