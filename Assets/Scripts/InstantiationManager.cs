using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiationManager : MonoBehaviour {

	public GameObject sceneRoot;
	public GameObject ARToolkit;
	public ARAppStructure arAppStructure;

	void Start () {
		addMarkers ();
		//GameObject markerScene = Instantiate (Resources.Load("MarkerScene", typeof(GameObject))) as GameObject;
		//ARTrackedObject arTrackedObject = markerScene.GetComponent<ARTrackedObject> () as ARTrackedObject;
		//arTrackedObject.MarkerTag = "hiro";
		//markerScene.transform.parent = sceneRoot.transform;
	}

	void addMarkers(){
		foreach(string marker in arAppStructure.markers)
		{
			ARMarker arMarker = ARToolkit.AddComponent(typeof(ARMarker)) as ARMarker;
			arMarker.Tag = marker;
		}
	}
}
