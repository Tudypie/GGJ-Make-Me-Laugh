using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace GameJam.Utilities.Components
{
    public class ScrollRectWithoutDrag : ScrollRect
    {
		public override void OnBeginDrag(PointerEventData eventData) { }
		public override void OnDrag(PointerEventData eventData) { }
		public override void OnEndDrag(PointerEventData eventData) { }
	}
}
