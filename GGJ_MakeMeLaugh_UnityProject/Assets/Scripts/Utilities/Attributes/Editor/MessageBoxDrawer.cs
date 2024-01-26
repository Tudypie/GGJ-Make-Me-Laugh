using UnityEditor;
using UnityEngine;

namespace GameJam.Utilities.Attributes.Editor
{
    [CustomPropertyDrawer(typeof(MessageBoxAttribute))]
    public class MessageBoxDrawer : PropertyDrawer
    {
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var messageBox = attribute as MessageBoxAttribute;

			var conditionalProperty = property.serializedObject.FindProperty(messageBox.conditionName);

			if (conditionalProperty != null && conditionalProperty.propertyType == SerializedPropertyType.Boolean)
			{
				bool conditionalValue = conditionalProperty.boolValue;

				if (conditionalValue) EditorGUILayout.HelpBox(messageBox.message, (MessageType)messageBox.messageType);
			}
			else
			{
				EditorGUILayout.HelpBox($"The provided condition \"{messageBox.conditionName}\" is not a valid boolean", MessageType.Warning);
			}

			if (messageBox.drawProperty) DrawDefaultProperty(position, property, label);
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => EditorGUI.GetPropertyHeight(property, label, true);

		private void DrawDefaultProperty(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginChangeCheck();
			EditorGUI.PropertyField(position, property, label, true);

			if (EditorGUI.EndChangeCheck()) property.serializedObject.ApplyModifiedProperties();
		}
	}
}
