using Citesoft.ARAuthoringTool.Core.Template;
using UnityEngine;

namespace Citesoft.ARAuthoringTool.Core
{
    public abstract class ARFramework : MonoBehaviour
    {
        public ARAppStructure arAppStructure;

        // TODO: Try-catch not-implemented methods
        private void Start()
        {
            // Setup();

            // Load markers
            foreach (string marker in arAppStructure.markers)
            {
                LoadPredefinedMarker(marker);
            }

            //Load interfaces
            foreach (ARAppInterface arAppInterface in arAppStructure.interfaces)
            {
                switch (arAppInterface.interfaceAction)
                {
                    // TODO: Use Enums
                    case 1:

                        switch (arAppInterface.interfaceEvent)
                        {
                            case 1:
                                // TODO: Handle for other resources
                                AddModelOnPredefinedMarker(arAppInterface);
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

        private void AddModelOnPredefinedMarker(ARAppInterface arAppInterface)
        {
            ARAppResource arAppResource = GetResource(arAppInterface.resourceName);

            // TODO: Use static string
            if (arAppResource.type.Equals("poly"))
            {
                AddPolyModelOnPredefinedMarker(arAppInterface.markerName, arAppResource.url);
            }
            else if (arAppResource.type.Equals("native"))
            {
                AddNativeModelOnPredefinedMarker(arAppInterface.markerName, arAppResource.url);
            }
        }

        // TODO: Move to Framework core util
        private ARAppResource GetResource(string resourceName)
        {
            foreach (ARAppResource arAppResource in arAppStructure.resources)
            {
                if (arAppResource.name.Equals(resourceName))
                    return arAppResource;
            }
            // TODO:  Handle if resource doesn't exists
            return null;
        }

        abstract public void Setup();
        abstract public void LoadPredefinedMarker(string markerId);
        abstract public void AddPolyModelOnPredefinedMarker(string markerName, string polyId);
        abstract public void AddNativeModelOnPredefinedMarker(string markerName, string nativeModelName);
    }
}