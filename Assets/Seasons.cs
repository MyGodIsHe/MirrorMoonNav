using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class Seasons : MonoBehaviour {

	private List<Season> seasons;

	private readonly int boxPadding = 20;
	private readonly int buttonWidth = 40;
	private readonly int buttonHeight = 20;
	private readonly int buttonPadding = 10;

	void Start() {
		string availableSeasons = Utils.urlopen(Consts.availableUrl).Split('\n')[6];
		string hash = Utils.getHash (availableSeasons + Consts.secretKey);
		string url = string.Format ("seasons.php?Seasons={0}&hash={1}", availableSeasons, hash);
		string data = Utils.urlopen (Consts.baseUrl + url);
		seasons = new List<Season>();
		foreach (string pair in data.Split(',')) {
			string[] values = pair.Split ('_');
			int number = int.Parse(values[0]);
			seasons.Add(new Season(number, values[1]));
		}
		seasons.Sort(delegate(Season x, Season y) {
			if (x.number < y.number)
				return -1;
			if (x.number > y.number)
				return 1;
			return 0;
		});
	}

	void OnGUI () {
		GUI.Box(new Rect(boxPadding, boxPadding,
		                 Screen.width - boxPadding*2, Screen.height - boxPadding*2),
		        "Seasons");
		int cols = Convert.ToInt32((Screen.width - boxPadding * 2 - buttonPadding) / (buttonWidth + buttonPadding));
		int rows = (int)Math.Ceiling((float)seasons.Count / cols);
		var enumSeasons = seasons.GetEnumerator();
		if (!enumSeasons.MoveNext ()) return;
		for (int j = 0; j < rows; j++) {
			for (int i = 0; i < cols; i++) {
				Season season = enumSeasons.Current;
				
				if (GUI.Button (new Rect (boxPadding + buttonPadding + i * (buttonWidth + buttonPadding),
			                              15 + boxPadding + buttonPadding + j * (buttonHeight + buttonPadding),
	                                      buttonWidth, buttonHeight),
				                season.number.ToString ())) {
					PlayerPrefs.SetString ("Seed", season.seed);
					Application.LoadLevel (1);
				}
				if (!enumSeasons.MoveNext())
					break;
			}
		}
	}
	
	void Update () {
	
	}

	public struct Season {
		public int number;
		public string seed;

		public Season(int p1, string p2) {
			number = p1;
			seed = p2;
		}
	}
}
