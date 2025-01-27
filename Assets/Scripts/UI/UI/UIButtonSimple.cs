using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIButtonSimple : UIButtonAbstract<UnityEvent> {
    public override void OnPointerClick(PointerEventData eventData) {
        OnClick.Invoke();
    }
}
