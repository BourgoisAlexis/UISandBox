using UnityEngine;
using UnityEngine.UI;

public class UIView : UIElement {
    #region Variables
    [SerializeField] protected UIButton _backButton;

    private Image _background;
    private bool _initialized;
    #endregion


    protected virtual void Init(params object[] parameters) {
        _backButton?.OnClick.AddListener(Back);
        _background = GetComponent<Image>();
        _background.sprite = _uiStyle.Sprite;
        _background.type = _uiStyle.ImageType;
        _background.color = _uiStyle.Dark;
        _initialized = true;
    }

    public virtual void Show(params object[] parameters) {
        if (!_initialized)
            Init(parameters);
    }

    public virtual void Back() {
        FindAnyObjectByType<UIViewManager>().Back();
    }
}
