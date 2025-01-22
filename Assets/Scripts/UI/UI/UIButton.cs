using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UIButton : UIElement, IInteractable {
    #region Variables
    private const float ANIM_DURATION = 0.15f;
    private const Ease ANIM_EASE = Ease.Linear;

    public UnityEvent OnClick;

    private Image _image;
    private Image _icon;
    private TextMeshProUGUI _text;
    private Color _hovered => _uiStyle.MainColor;
    private Color _default => _uiStyle.Light;
    #endregion


    private void Start() {
        Image[] images = GetComponentsInChildren<Image>();

        _image = images[0];

        if (images.Length > 1)
            _icon = images[1];

        _text = GetComponentInChildren<TextMeshProUGUI>();

        ReInit();
    }

    private void OnEnable() {
        ReInit();
    }


    private void ReInit() {
        if (_image != null)
            _image.color = _hovered;

        if (_icon != null)
            _icon.color = _default;

        if (_text != null) {
            _text.color = _default;
            _text.font = _uiStyle.Font;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        _image?.DOColor(_default, ANIM_DURATION).SetEase(ANIM_EASE);
        _icon?.DOColor(_hovered, ANIM_DURATION).SetEase(ANIM_EASE);
        _text?.DOColor(_hovered, ANIM_DURATION).SetEase(ANIM_EASE);
        transform.DOScale(1.1f, ANIM_DURATION).SetEase(ANIM_EASE);
    }

    public void OnPointerExit(PointerEventData eventData) {
        _image?.DOColor(_hovered, ANIM_DURATION).SetEase(ANIM_EASE);
        _icon?.DOColor(_default, ANIM_DURATION).SetEase(ANIM_EASE);
        _text?.DOColor(_default, ANIM_DURATION).SetEase(ANIM_EASE);
        transform.DOScale(1f, ANIM_DURATION).SetEase(ANIM_EASE);
    }

    public void OnPointerClick(PointerEventData eventData) {
        OnClick?.Invoke();
    }
}
