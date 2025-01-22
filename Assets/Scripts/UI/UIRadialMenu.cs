using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

public class UIRadialMenu : UIElement {
    private const float ANIM_DURATION = 0.15f;
    private const Ease ANIM_EASE = Ease.InOutCirc;

    private RectTransform _rectTransform;
    private RectTransform[] _elements;
    private float _angle;
    private float _distance;
    private bool _randomize;


    public async Task Init(RectTransform[] elements, float angle, float distance, bool randomize = false) {
        gameObject.name = "UIRadialMenu";
        _elements = elements == null ? new RectTransform[0] : elements;
        _angle = angle;
        _distance = distance;
        _randomize = randomize;
        _rectTransform = GetComponent<RectTransform>();

        await ShowElements();
    }

    private async Task ShowElements() {
        float startAngle = _angle / 2;
        startAngle += 90;
        float step = -_angle / (float)(_elements.Length - (_angle == 360 ? 0 : 1));

        int[] indexes = Utils.CreateRandomIntArray(_elements.Length);

        for (int i = 0; i < _elements.Length; i++) {
            RectTransform rect = _elements[i];
            rect.SetParent(_rectTransform);
            rect.localPosition = Vector2.zero;
            rect.localScale = Vector2.one;
        }

        for (int i = 0; i < indexes.Length; i++) {
            int index = _randomize ? indexes[i] : i;
            UITrail trail = UIUtils.AddUITrail(_elements[index], _uiStyle);

            trail.Move(GetPositionFromAngle(startAngle + step * index), true);
            await Task.Delay(Mathf.RoundToInt(ANIM_DURATION / (float)_elements.Length * 1000));
        }
    }

    private Vector2 GetPositionFromAngle(float angle) {
        angle *= Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angle) * _distance, Mathf.Sin(angle) * _distance);
    }

    private void OnDrawGizmos() {
        if (_elements == null || _elements.Length == 0)
            return;

        Gizmos.color = Color.yellow;

        foreach (RectTransform element in _elements) {
            Gizmos.DrawLine(transform.position, element.position);
        }
    }
}
