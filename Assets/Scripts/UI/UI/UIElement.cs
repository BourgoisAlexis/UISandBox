using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIElement : MonoBehaviour {
    protected const float ANIM_DURATION = 0.15f;
    protected const Ease ANIM_EASE = Ease.InOutCubic;

    [SerializeField] protected UIStyle _uiStyle;

    protected RectTransform _rectTransform;
    protected Image _background;
    protected Image _icon;
    protected TextMeshProUGUI _text;


    protected virtual void Awake() {
        _rectTransform = GetComponent<RectTransform>();
        Image[] images = GetComponentsInChildren<Image>();

        if (images.Length > 0)
            _background = images[0];

        if (images.Length > 1)
            _icon = images[1];

        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    protected virtual void OnEnable() {
        if (_uiStyle != null)
            SetUIStyle(_uiStyle);
    }

    public virtual void SetUIStyle(UIStyle uiStyle) {
        if (uiStyle == _uiStyle)
            return;

        _uiStyle = uiStyle;
    }
}
