using UnityEngine;

namespace GameJam.Utilities
{
	[CreateAssetMenu(fileName = "TextArea", menuName = "ScriptableObjects/TextAreaAsset")]
	public class TextAreaAsset : ScriptableObject
	{
		[TextArea(10, 20)] public string textArea;
	}
}
