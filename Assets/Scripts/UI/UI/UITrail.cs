using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UITrail : UIElement {
    private const float ANIM_DURATION = 0.2f;
    private const Ease ANIM_EASE = Ease.InOutCirc;

    private RectTransform _rect;
    private Image _image;

    private GameObject _trail;
    private RectTransform _trailRect;
    private Image _trailImage;


    public void Init() {
        _rect = GetComponent<RectTransform>();
        _image = GetComponent<Image>();

        _trail = new GameObject();
        _trail.name = $"{gameObject.name}_trail";

        _trailRect = _trail.AddComponent<RectTransform>();
        _trailImage = _trail.AddComponent<Image>();

        _trailRect.SetParent(transform.parent);
        _trailRect.SetSiblingIndex(_rect.GetSiblingIndex());
        _trail.SetActive(false);
    }

    public async Task Move(Vector2 destination, bool local) {
        _trail.SetActive(true);
        _trailRect.localPosition = _rect.localPosition;
        _trailRect.localScale = Vector2.one;
        _trailRect.sizeDelta = _rect.sizeDelta;

        _trailImage.color = _uiStyle.Light;
        _trailImage.sprite = _image.sprite;
        _trailImage.type = _image.type;

        Task t = Anim(_rect, destination, local);
        await Task.Delay(Mathf.RoundToInt(ANIM_DURATION / 4 * 1000));
        await Anim(_trailRect, destination, local);
        _trail.SetActive(false);
    }

    private async Task Anim(RectTransform rect, Vector2 destination, bool local) {
        if (local)
            await rect.DOLocalMove(destination, ANIM_DURATION).SetEase(ANIM_EASE).AsyncWaitForCompletion();
        else
            await rect.DOMove(destination, ANIM_DURATION).SetEase(ANIM_EASE).AsyncWaitForCompletion();
    }
}
