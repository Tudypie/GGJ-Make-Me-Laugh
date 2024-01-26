using UnityEngine;
using GameJam.Utilities;

namespace GameJam.EventSystem
{
	[CreateAssetMenu(fileName = "VoidEvent", menuName = "ScriptableObjects/VoidEvent")]
	public class VoidEvent : BaseGameEvent<Void>
	{
		public void Invoke() => Invoke(new Void()); 
	}
}
