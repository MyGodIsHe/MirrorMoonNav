using UnityEngine;
using System.Collections;

public class Message : MonoBehaviour {

	public float showTime = 5;

	private float startTime = 0;

	// Update is called once per frame
	void Update () {
		startTime += Time.deltaTime;
		if (startTime > showTime)
			Destroy (gameObject);
	}
}
