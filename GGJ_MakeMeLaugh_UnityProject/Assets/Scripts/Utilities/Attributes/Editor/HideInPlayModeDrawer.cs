using UnityEditor;
using UnityEngine;

namespace GameJam.Utilities.Attributes.Editor
{
    [CustomPropertyDrawer(typeof(HideInPlayModeAttribute))]
    public class HideInPlayModeDrawer : PropertyDrawer
    {
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (!Application.isPlaying) EditorGUI.PropertyField(position, property, label);
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			if (!Application.isPlaying)
			{
				return EditorGUI.GetPropertyHeight(property, label);
			}
			else
			{
				return -EditorGUIUtility.standardVerticalSpacing; // We want to undo the spacing added before and after the property
			}
		}
	}
}
