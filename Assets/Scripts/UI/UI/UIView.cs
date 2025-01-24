using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UIView : UIElement {
    #region Variables
    [SerializeField] protected UIButton _backButton;

    private bool _initialized;
    private Vector2 _sizeDelta;
    #endregion

    protected override void Awake() {
        base.Awake();
        _sizeDelta = _rectTransform.sizeDelta;
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

    public virtual async Task Show(params object[] parameters) {
        if (!_initialized)
            Init(parameters);

        await _rectTransform.DOSizeDelta(new Vector2(_sizeDelta.x, _sizeDelta.y), ANIM_DURATION).SetEase(ANIM_EASE).AsyncWaitForCompletion();
    }

    public virtual async Task Hide() {
        float height = -_rectTransform.rect.height + _rectTransform.sizeDelta.y;
        await _rectTransform.DOSizeDelta(new Vector2(_sizeDelta.x, height), ANIM_DURATION).SetEase(ANIM_EASE).AsyncWaitForCompletion();
    }

    public virtual void Back() {
        FindAnyObjectByType<UIViewManager>().Back();
    }
}
