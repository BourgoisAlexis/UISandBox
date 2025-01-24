using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UIButton : UIElement, IInteractable {
    #region Variables
    public UnityEvent OnClick;

    private Color _default => _uiStyle.MainColor;
    private Color _hovered => _uiStyle.Light;
    #endregion


    public override void SetUIStyle(UIStyle uiStyle) {
        base.SetUIStyle(uiStyle);

        if (_background != null)
            _background.color = _default;

        if (_icon != null)
            _icon.color = _hovered;

        if (_text != null) {
            _text.color = _hovered;
            _text.font = _uiStyle.Font;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        _background?.DOColor(_hovered, ANIM_DURATION).SetEase(ANIM_EASE);
        _icon?.DOColor(_default, ANIM_DURATION).SetEase(ANIM_EASE);
        _text?.DOColor(_default, ANIM_DURATION).SetEase(ANIM_EASE);
        transform.DOScale(1.1f, ANIM_DURATION).SetEase(ANIM_EASE);
    }

    public void OnPointerExit(PointerEventData eventData) {
        _background?.DOColor(_default, ANIM_DURATION).SetEase(ANIM_EASE);
        _icon?.DOColor(_hovered, ANIM_DURATION).SetEase(ANIM_EASE);
        _text?.DOColor(_hovered, ANIM_DURATION).SetEase(ANIM_EASE);
        transform.DOScale(1f, ANIM_DURATION).SetEase(ANIM_EASE);
    }

    public void OnPointerClick(PointerEventData eventData) {
        OnClick?.Invoke();
    }
}
