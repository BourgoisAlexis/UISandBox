using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UIView : UIElement {
    #region Variables
    [SerializeField] protected UIButtonSimple _backButton;

    private bool _initialized;
    private Vector2 _sizeDelta;
    #endregion

    protected override void Awake() {
        base.Awake();
        _sizeDelta = _rectTransform.sizeDelta;
        gameObject.AddComponent<RectMask2D>();
    }

    protected virtual void Init(params object[] parameters) {
        _backButton?.OnClick.AddListener(Back);
        
        _initialized = true;
    }

    public override void SetUIStyle(UIStyle uiStyle) {
        base.SetUIStyle(uiStyle);
        _background.sprite = _uiStyle.Sprite;
        _background.type = _uiStyle.ImageType;
        _background.color = _uiStyle.Dark;
    }

    public virtual async Task Show(bool instant, params object[] parameters) {
        gameObject.SetActive(true);

        if (!_initialized)
            Init(parameters);

        float duration = instant ? 0 : ANIM_DURATION;
        await _rectTransform.DOSizeDelta(new Vector2(_sizeDelta.x, _sizeDelta.y), duration).SetEase(ANIM_EASE).AsyncWaitForCompletion();
    }

    public virtual async Task Hide(bool instant) {
        float height = -_rectTransform.rect.height + _rectTransform.sizeDelta.y;
        float duration = instant ? 0 : ANIM_DURATION;
        await _rectTransform.DOSizeDelta(new Vector2(_sizeDelta.x, height), duration).SetEase(ANIM_EASE).AsyncWaitForCompletion();

        gameObject.SetActive(false);
    }

    public virtual void Back() {
        FindAnyObjectByType<UIViewManager>().Back();
    }
}
