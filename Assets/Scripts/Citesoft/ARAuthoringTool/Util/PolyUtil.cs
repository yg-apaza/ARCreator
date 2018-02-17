using PolyToolkit;
using UnityEngine;

namespace Citesoft.ARAuthoringTool.Util
{
    public static class PolyUtil
    {
        public delegate void GetPolyResultCallback(GameObject asset);

        public static void GetPolyResult(string assetId, GetPolyResultCallback callback)
        {
            PolyApi.GetAsset(assetId, (getAssetResult) => GetAssetCallback(getAssetResult, callback));
        }

        private static void GetAssetCallback(PolyStatusOr<PolyAsset> getAssetResult, GetPolyResultCallback callback)
        {
            if (!getAssetResult.Ok)
            {
                Debug.LogError("Failed to get assets. Reason: " + getAssetResult.Status);
                return;
            }

            Debug.Log("Successfully got asset!");
            PolyImportOptions options = PolyImportOptions.Default();
            options.rescalingMode = PolyImportOptions.RescalingMode.FIT;
            options.desiredSize = 5.0f;
            options.recenter = true;

            PolyApi.Import(getAssetResult.Value, options, (asset, importResult) => ImportAssetCallback(asset, importResult, callback));
        }

        private static void ImportAssetCallback(PolyAsset asset, PolyStatusOr<PolyImportResult> result, GetPolyResultCallback callback)
        {
            if (!result.Ok)
            {
                Debug.LogError("Failed to import asset. :( Reason: " + result.Status);
                return;
            }
            Debug.Log("Successfully imported asset!");

            // Default transformations
            GameObject polyResult = result.Value.gameObject.transform.GetChild(0).gameObject;
            polyResult.SetActive(false);
            // TODO: Doesn't work for all models
            polyResult.transform.position = new Vector3(0f, 0f, 0f);
            polyResult.transform.rotation = Quaternion.Euler(270, 0, 0);
            polyResult.transform.localScale = new Vector3(0.015f, 0.015f, 0.015f);

            callback(polyResult);
        }
    }
}