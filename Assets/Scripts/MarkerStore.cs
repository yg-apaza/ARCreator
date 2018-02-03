using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerStore {

	public static Dictionary<string, string> markerStore;

	public MarkerStore(){
		markerStore = new Dictionary<string, string> ();
		markerStore.Add ("hiro", "patt.hiro");
		markerStore.Add ("kanji", "patt.kanji");
	}
}
