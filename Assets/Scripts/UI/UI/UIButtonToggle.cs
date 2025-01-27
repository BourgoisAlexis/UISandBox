using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIButtonToggle : UIButtonAbstract<UnityEvent<bool>> {
    private bool _value;

    protected override Color _default => _value ? base._default : base._default.GreyOut();
    protected override Color _hovered => _value ? base._hovered : base._hovered.GreyOut();

    protected override void OnEnable() {
        base.OnEnable();
    }

    public override void OnPointerClick(PointerEventData eventData) {
        _value = !_value;
        OnClick?.Invoke(_value);
        UpdateVisual();
    }
}
