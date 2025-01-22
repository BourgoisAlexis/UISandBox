using UnityEngine;

public abstract class UIElement : MonoBehaviour {
    [SerializeField] protected UIStyle _uiStyle;

    public void SetUIStyle(UIStyle uiStyle) {
        _uiStyle = uiStyle;
    }
}
