using UnityEngine;

namespace Citesoft.ARAuthoringTool.Core.Template
{
    [CreateAssetMenu(fileName = "arAppStructure", menuName = "ARAppStructure")]
    public class ARAppStructure : ScriptableObject
    {
		public string _id;
		public string author;
		public string title;
        public string description;
        public string framework;
        public string[] markers;
        public ARAppResource[] resources;
        public ARAppInterface[] interfaces;
    }
}