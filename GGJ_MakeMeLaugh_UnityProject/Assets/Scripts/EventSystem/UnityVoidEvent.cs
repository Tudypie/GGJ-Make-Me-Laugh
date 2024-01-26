using System;
using UnityEngine.Events;
using Void = GameJam.Utilities.Void;

namespace GameJam.EventSystem
{
	[Serializable]
	public class UnityVoidEvent : UnityEvent<Void> { }
}
