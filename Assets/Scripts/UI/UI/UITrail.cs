using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UITrail : UIElement {
    private GameObject _go;
    private RectTransform _rect;
    private Image _image;


    public void Init() {
        _go = new GameObject();
        _rect = _go.AddComponent<RectTransform>();
        _image = _go.AddComponent<Image>();

        _go.name = $"{gameObject.name}_trail";

        _rect.SetParent(transform.parent);
        _rect.SetSiblingIndex(_rectTransform.GetSiblingIndex());
        _go.SetActive(false);
    }

    public async Task Move(Vector2 destination, bool local) {
        _go.SetActive(true);
        _rect.localPosition = _rectTransform.localPosition;
        _rect.localScale = Vector2.one;
        _rect.sizeDelta = _rectTransform.sizeDelta;

        _image.color = _uiStyle.Light;
        _image.sprite = _background.sprite;
        _image.type = _background.type;

        Task t = Anim(_rectTransform, destination, local);
        await Task.Delay(Mathf.RoundToInt(ANIM_DURATION / 4 * 1000));
        await Anim(_rect, destination, local);
        _go.SetActive(false);
    }

    private async Task Anim(RectTransform rect, Vector2 destination, bool local) {
        if (local)
            await rect.DOLocalMove(destination, ANIM_DURATION).SetEase(ANIM_EASE).AsyncWaitForCompletion();
        else
            await rect.DOMove(destination, ANIM_DURATION).SetEase(ANIM_EASE).AsyncWaitForCompletion();
    }
}
