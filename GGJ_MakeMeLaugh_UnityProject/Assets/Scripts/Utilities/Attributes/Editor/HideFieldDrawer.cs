using System;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;

namespace GameJam.Utilities.Attributes.Editor
{
    [CustomPropertyDrawer(typeof(HideFieldAttribute))]
    public class HideFieldDrawer : PropertyDrawer
    {
		private UnityEventDrawer eventDrawer;
		
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var hideAttribute = attribute as HideFieldAttribute;

			var conditionalProperty = property.serializedObject.FindProperty(hideAttribute.conditionName);

			eventDrawer ??= new UnityEventDrawer();

			if (conditionalProperty != null && conditionalProperty.propertyType == SerializedPropertyType.Boolean)
			{
				bool conditionalValue = conditionalProperty.boolValue;

				DrawProperty(conditionalValue, position, property, label);
			}
			else if (conditionalProperty != null && conditionalProperty.propertyType == SerializedPropertyType.Integer)
			{
				bool conditionalValue = conditionalProperty.intValue == hideAttribute.enumValue;

				DrawProperty(conditionalValue, position, property, label);
			}
			else
			{
				EditorGUILayout.HelpBox($"The provided condition \"{hideAttribute.conditionName}\" is not a valid boolean or an enum", MessageType.Warning);
			}
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			var hideAttribute = attribute as HideFieldAttribute;

			var conditionalProperty = property.serializedObject.FindProperty(hideAttribute.conditionName);

			eventDrawer ??= new UnityEventDrawer();

			if (conditionalProperty != null && conditionalProperty.propertyType == SerializedPropertyType.Boolean)
			{
				bool conditionalValue = conditionalProperty.boolValue;

				return GetPropertyHeight(conditionalValue, property, label);
			}
			else if (conditionalProperty != null && conditionalProperty.propertyType == SerializedPropertyType.Integer)
			{
				bool conditionalValue = conditionalProperty.intValue == hideAttribute.enumValue;

				return GetPropertyHeight(conditionalValue, property, label);
			}

			return GetCorrectPropertyHeight(property, label);
		}

		private float GetPropertyHeight(bool conditionalValue, SerializedProperty property, GUIContent label)
		{
			if (!conditionalValue)
			{
				return GetCorrectPropertyHeight(property, label);
			}
			else
			{
				return -EditorGUIUtility.standardVerticalSpacing; // We want to undo the spacing added before and after the property
			}
		}

		private float GetCorrectPropertyHeight(SerializedProperty property, GUIContent label)
		{
			try
			{
				return eventDrawer.GetPropertyHeight(property, label);
			}
			catch (NullReferenceException)
			{
				return EditorGUI.GetPropertyHeight(property, label);
			}
		}

		private void DrawProperty(bool conditionalValue, Rect position, SerializedProperty property, GUIContent label)
		{
			if (!conditionalValue)
			{
				try
				{
					eventDrawer.OnGUI(position, property, label);
				}
				catch (NullReferenceException)
				{
					EditorGUI.PropertyField(position, property, label, true);
				}
			}
		}
	}
}
