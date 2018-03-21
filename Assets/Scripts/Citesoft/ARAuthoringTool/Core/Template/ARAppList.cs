using UnityEngine;

namespace Citesoft.ARAuthoringTool.Core.Template
{
	[CreateAssetMenu(fileName = "arAppList", menuName = "ARAppList")]
	public class ARAppList : ScriptableObject
	{
		public ARAppSummary[] arApps;
	}
}