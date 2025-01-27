using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;

public class UIButtonTab : UIButtonAbstract<UnityEvent<int>> {
    private int _index;
    private bool _value;

    protected override Color _default => _value ? base._default : base._default.GreyOut();
    protected override Color _hovered => _value ? base._hovered : base._hovered.GreyOut();

    public void SetIndex(int index) {
        _index = index;
    }

    public void SetSelected(bool value) {
        _value = value;
        UpdateVisual();
    }

    public override void OnPointerClick(PointerEventData eventData) {
        OnClick?.Invoke(_index);
    }
}
