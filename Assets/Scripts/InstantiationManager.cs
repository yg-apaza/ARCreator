using PolyToolkit;
using System.Collections.Generic;
using UnityEngine;

public class InstantiationManager : MonoBehaviour {

	public GameObject sceneRoot;
	public GameObject ARToolkit;
	public ARAppStructure arAppStructure;
    private GameObject currentMarkerScene;

    void Start () {
        loadMarkers();
        loadInterfaces();
    }

    private void loadMarkers(){
		foreach(string marker in arAppStructure.markers)
		{
			ARMarker arMarker = ARToolkit.AddComponent(typeof(ARMarker)) as ARMarker;
            arMarker.Tag = marker;
            arMarker.MarkerType = MarkerType.Square;
            arMarker.PatternWidth = 0.08f;
            arMarker.PatternFilename = MarkerStore.markerStore[marker];
            TextAsset patternAsset = Resources.Load("ardata/markers/" + MarkerStore.markerStore[marker], typeof(TextAsset)) as TextAsset;
            arMarker.PatternContents = patternAsset.text;
            arMarker.Load();
		}
	}
    
    private void GetAssetCallback(PolyStatusOr<PolyAsset> result)
    {
        if (!result.Ok)
        {
            Debug.LogError("Failed to get assets. Reason: " + result.Status);
            return;
        }
        Debug.Log("Successfully got asset!");
        PolyImportOptions options = PolyImportOptions.Default();
        options.rescalingMode = PolyImportOptions.RescalingMode.FIT;
        options.desiredSize = 5.0f;
        options.recenter = true;
        PolyApi.Import(result.Value, options, ImportAssetCallback);
    }

    private void ImportAssetCallback(PolyAsset asset, PolyStatusOr<PolyImportResult> result)
    {
        if (!result.Ok)
        {
            Debug.LogError("Failed to import asset. :( Reason: " + result.Status);
            return;
        }
        Debug.Log("Successfully imported asset!");

        // Add to scene
		GameObject polyModel = result.Value.gameObject.transform.GetChild (0).gameObject;
		polyModel.SetActive (false);
		polyModel.transform.parent = currentMarkerScene.transform;
		changeLayerToARBackground (polyModel.transform);
		polyModel.transform.position = new Vector3(0f, 0f, 0f);
		polyModel.transform.localScale = new Vector3(0.015f, 0.015f, 0.015f);
    }

	private static void changeLayerToARBackground(Transform trans)
	{
		trans.gameObject.layer = 8;
		foreach(Transform child in trans)
		{            
			changeLayerToARBackground(child);
		}
	}

    private void loadInterfaces()
    {
        foreach (ARAppInterface _interface in arAppStructure.interfaces)
        {
            switch (_interface._action)
            {
                // TODO: Use Enums
                case 1:

                    switch(_interface._event)
                    {
                        case 1:
                            GameObject markerScene = addMarker(_interface._markerName);
                            addModel(markerScene, _interface._resourceName);
                            break;
                        default:
                            break;
                    }

                    break;
                default:
                    break;
            }
        }
    }

    private GameObject addMarker(string markerName)
    {
        GameObject markerScene = Instantiate(Resources.Load("MarkerScene", typeof(GameObject))) as GameObject;
        ARTrackedObject arTrackedObject = markerScene.GetComponent<ARTrackedObject>() as ARTrackedObject;
        arTrackedObject.MarkerTag = markerName;
        markerScene.transform.parent = sceneRoot.transform;
        return markerScene;
    }


    // TODO: Clone a 3D model if it's already in another MarkerScene
    private void addModel(GameObject markerScene, string resourceName)
    {
        ARAppResource arAppResource = getResource(resourceName);

        // TODO: Use static string
        if (arAppResource._type.Equals("poly"))
        {
            currentMarkerScene = markerScene;
            PolyApi.GetAsset("assets/" + arAppResource._url, GetAssetCallback);
        }
        else if (arAppResource._type.Equals("native"))
        {
            addNativeModel(arAppResource._url, markerScene);
        }
    }

    private ARAppResource getResource(string resourceName)
    {
        foreach(ARAppResource arAppResource in arAppStructure.resources)
        {
            if (arAppResource._name.Equals(resourceName))
                return arAppResource;
        }
        // TODO:  Handle if resource doesnt exits
        return null;
    }

    // TODO: Add getPolyModel
    private void addNativeModel(string name, GameObject markerScene)
    {
        GameObject primitiveModel = null;

        if (name.Equals("cube"))
        {
            primitiveModel = GameObject.CreatePrimitive(PrimitiveType.Cube);
            
        }
        else if (name.Equals("sphere"))
        {
            primitiveModel = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        }
        else if (name.Equals("cylinder"))
        {
            primitiveModel = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        }
        else if (name.Equals("capsule"))
        {
            primitiveModel = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        }

        primitiveModel.transform.position = new Vector3(0f, 0f, 0f);
        primitiveModel.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
        primitiveModel.layer = 8;
        primitiveModel.transform.parent = markerScene.transform;
    }
}