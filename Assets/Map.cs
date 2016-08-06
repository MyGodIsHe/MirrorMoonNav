using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class Map : MonoBehaviour {
	
	public GameObject starPrefab;
	public GameObject messagePrefab;

	public List<GameObject> stars;

	private string seed;
	
	void Start () {
		seed = PlayerPrefs.GetString ("Seed");
		string hash = Utils.getHash (seed + Consts.secretKey);
		string url = string.Format ("full_star.php?Seed={0}&hash={1}", seed, hash);
		string data = Utils.urlopen(Consts.baseUrl + url);
		foreach (string pair in data.Split(',')) {
			string name = pair.Substring(16, 6).Trim();
			string pos = pair.Substring(22);
			float x = Convert.ToSingle(pos.Substring(0, 3));
			float y = Convert.ToSingle(pos.Substring(3, 3));
			float z = Convert.ToSingle(pos.Substring(6));

			GameObject star = Instantiate(starPrefab) as GameObject;
			star.GetComponent<Star>().title = name;
			star.transform.position = new Vector3(x, y, z);
			stars.Add(star);
		}
	}

	public GameObject Search (string title) {
		title = title.ToUpper();
		foreach (GameObject star in stars) {
			if (star.GetComponent<Star>().title == title) {
				return star;
			}
		}
		return null;
	}
}
