using UnityEngine;

namespace GameJam.Utilities.Attributes
{
	public class HideFieldAttribute : PropertyAttribute
    {
        public readonly string conditionName;
		public readonly int enumValue;

		/// <summary>
		/// Attribute to hide fields in the inspector based on a boolean condition
		/// </summary>
		/// <param name="conditionName">The name of the boolean condition to evaluate</param>
		public HideFieldAttribute(string conditionName) => this.conditionName = conditionName;

		/// <summary>
		/// Attribute to hide fields in the inspector based on a enum condition
		/// </summary>
		/// <param name="conditionName">The name of the enum condition to evaluate</param>
		/// <param name="enumValue">The value of the enum as an integer</param>
		public HideFieldAttribute(string conditionName, int enumValue)
		{
			this.conditionName = conditionName;
			this.enumValue = enumValue;
		}
	}
}
