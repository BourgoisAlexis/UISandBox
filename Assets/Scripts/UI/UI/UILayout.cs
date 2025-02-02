using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

public class UILayout : UIElement {
    private RectTransform[] _elements;
    private Vector2 _margin;
    private bool _randomize;


    public override void SetUIStyle(UIStyle uiStyle) {
        base.SetUIStyle(uiStyle);

        _background.color = _uiStyle.BackgroundColor;
        _background.sprite = _uiStyle.Sprite;
        _background.type = _uiStyle.ImageType;
    }

    public async Task Init(RectTransform[] elements, Vector2 margin, bool randomize = false) {
        gameObject.name = "UILayout";
        _elements = elements == null ? new RectTransform[0] : elements;
        _margin = margin;
        _randomize = randomize;

        Vector2 size = await SetSize();
        await Task.Delay(Mathf.RoundToInt(ANIM_DURATION * 1000));
        await ShowElements(size);
    }

    private async Task<Vector2> SetSize() {
        float height = _margin.y;
        float width = 50;

        foreach (RectTransform element in _elements) {
            height += element.rect.height;
            height += _margin.y;
        }

        foreach (RectTransform element in _elements) {
            float w = element.rect.width;
            if (w > width)
                width = w;
        }

        width += _margin.x * 2;

        Vector2 size = new Vector2(width, height);

        _rectTransform.sizeDelta = new Vector2(width, 0);
        await _rectTransform.DOSizeDelta(size, ANIM_DURATION).SetEase(ANIM_EASE).AsyncWaitForCompletion();

        return size;
    }

    private async Task ShowElements(Vector2 parentSize) {
        float pos = -parentSize.y / 2 + _margin.y;

        int[] indexes = Utils.CreateRandomIntArray(_elements.Length);
        float[] delays = new float[indexes.Length];
        GameObject[] objects = new GameObject[indexes.Length];

        for (int i = 0; i < _elements.Length; i++) {
            RectTransform rect = _elements[i];
            rect.SetParent(_rectTransform);
            pos += rect.rect.height / 2;
            rect.localPosition = Vector2.down * pos;
            pos += _margin.y;
            pos += rect.rect.height / 2;

            rect.gameObject.SetActive(false);

            delays[i] = ANIM_DURATION / _elements.Length;
            objects[i] = rect.gameObject;
        }

        for (int i = 0; i < indexes.Length; i++) {
            int index = _randomize ? indexes[i] : i;
            objects[index].SetActive(true);
            await Task.Delay(Mathf.RoundToInt(delays[index] * 1000));
        }
    }
}
