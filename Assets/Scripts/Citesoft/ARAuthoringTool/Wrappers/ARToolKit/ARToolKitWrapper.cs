using System;
using Citesoft.ARAuthoringTool.Core;
using Citesoft.ARAuthoringTool.Util;
using UnityEngine;

namespace Citesoft.ARAuthoringTool.Wrappers.ARToolKit
{
    public class ARToolKitWrapper : ARFramework
    {
        public GameObject ARToolkit;
        public GameObject sceneRoot;

        public override void AddPolyModelOnPredefinedMarker(string markerName, string polyId)
        {
            GameObject markerScene = CreateMarkerScene(markerName);
            PolyUtil.GetPolyResult("assets/" + polyId, (polyResult) => ShowPolyModel(polyResult, markerScene));
        }

        public override void AddNativeModelOnPredefinedMarker(string markerName, string nativeModelName)
        {
            GameObject markerScene = CreateMarkerScene(markerName);
            ShowNativeModel(nativeModelName, markerScene);
        }

        private GameObject CreateMarkerScene(string markerName)
        {
            GameObject markerScene = Instantiate(Resources.Load("MarkerScene", typeof(GameObject))) as GameObject;
            ARTrackedObject arTrackedObject = markerScene.GetComponent<ARTrackedObject>() as ARTrackedObject;
            arTrackedObject.MarkerTag = markerName;
            markerScene.transform.parent = sceneRoot.transform;
            return markerScene;
        }

        private void ShowPolyModel(GameObject polyResult, GameObject container)
        {
            polyResult.transform.parent = container.transform;
            ChangeLayerToARBackground(polyResult.transform);
        }

        private static void ChangeLayerToARBackground(Transform trans)
        {
            trans.gameObject.layer = 8;
            foreach (Transform child in trans)
            {
                ChangeLayerToARBackground(child);
            }
        }

        private void ShowNativeModel(string name, GameObject markerScene)
        {
            GameObject primitiveModel = null;


            // TODO: Use static strings or enums
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

        public override void LoadPredefinedMarker(string markerId)
        {
            ARMarker arMarker = ARToolkit.AddComponent(typeof(ARMarker)) as ARMarker;
            arMarker.Tag = markerId;
            arMarker.MarkerType = MarkerType.Square;
            arMarker.PatternWidth = 0.08f;
            arMarker.PatternFilename = PreloadedMarker.markerStore[markerId];
            TextAsset patternAsset = Resources.Load("markers/" + PreloadedMarker.markerStore[markerId], typeof(TextAsset)) as TextAsset;

            arMarker.PatternContents = patternAsset.text;

            arMarker.Load();
        }

        public override void Setup()
        {
            throw new NotImplementedException();
        }
    }
}