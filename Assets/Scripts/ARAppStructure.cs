using UnityEngine;

[CreateAssetMenu(fileName = "arAppStructure", menuName = "ARAppStructure")]
public class ARAppStructure : ScriptableObject
{
	public string title;
	public string description;
	public string[] markers;
	public ARAppResource[] resources;
	public ARAppInterface[] interfaces;
}
