using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class UIButtonAbstract<T> : UIElement, IInteractable {
    #region Variables
    public T OnClick;

    protected bool _hover;
    protected virtual Color _default => _uiStyle.MainColor;
    protected virtual Color _hovered => _uiStyle.Light;
    #endregion


    public override void SetUIStyle(UIStyle uiStyle) {
        base.SetUIStyle(uiStyle);

        _hover = false;

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
        _hover = true;
        UpdateVisual();
    }

    public void OnPointerExit(PointerEventData eventData) {
        _hover = false;
        UpdateVisual();
    }

    protected void UpdateVisual() {
        if (_hover) {
            _background?.DOColor(_hovered, ANIM_DURATION).SetEase(ANIM_EASE);
            _icon?.DOColor(_default, ANIM_DURATION).SetEase(ANIM_EASE);
            _text?.DOColor(_default, ANIM_DURATION).SetEase(ANIM_EASE);
            transform.DOScale(1.1f, ANIM_DURATION).SetEase(ANIM_EASE);
        }
        else {
            _background?.DOColor(_default, ANIM_DURATION).SetEase(ANIM_EASE);
            _icon?.DOColor(_hovered, ANIM_DURATION).SetEase(ANIM_EASE);
            _text?.DOColor(_hovered, ANIM_DURATION).SetEase(ANIM_EASE);
            transform.DOScale(1f, ANIM_DURATION).SetEase(ANIM_EASE);
        }
    }

    public abstract void OnPointerClick(PointerEventData eventData);
}
