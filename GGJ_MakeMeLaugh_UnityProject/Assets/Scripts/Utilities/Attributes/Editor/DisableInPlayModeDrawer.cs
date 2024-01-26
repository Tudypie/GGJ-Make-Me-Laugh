using UnityEditor;
using UnityEngine;

namespace GameJam.Utilities.Attributes.Editor
{
    [CustomPropertyDrawer(typeof(DisableInPlayModeAttribute))]
    public class DisableInPlayModeDrawer : PropertyDrawer
    {
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (Application.isPlaying) GUI.enabled = false;

			EditorGUI.PropertyField(position, property, label);
			GUI.enabled = true;
		}
	}
}
