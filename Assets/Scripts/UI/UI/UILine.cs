using DG.Tweening;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UILine : UIElement {
    private const float ANIM_DURATION = 0.15f;
    private const float RATIO = 0.5f;
    private const float THICKNESS = 10f;

    private RectTransform _rectTransform;


    public async Task Init(Vector2 origin, Vector2 destination) {
        gameObject.name = "UILine";
        _rectTransform = GetComponent<RectTransform>();

        Vector2 direction = destination - origin;

        if (direction.y != 0 || direction.x != 0) {
            Vector2 ori = origin;
            Vector2 desti = ori + Vector2.right * direction.x * RATIO;
            await CreateSegment(ori, desti);

            ori = desti;
            desti = ori + Vector2.up * direction.y;
            await CreateSegment(ori, desti);

            ori = desti;
            desti = destination;
            await CreateSegment(ori, desti);
        }
    }

    private async Task CreateSegment(Vector2 origin, Vector2 destination) {
        Vector2 direction = destination - origin;
        Vector2 absoluteDirection = new Vector2(Math.Abs(direction.x), Math.Abs(direction.y));
        bool horizontal = direction.y == 0;
        GameObject go = new GameObject();
        RectTransform rect = go.AddComponent<RectTransform>();
        Image image = go.AddComponent<Image>();
        image.color = _uiStyle.Light;
        rect.SetParent(_rectTransform);
        rect.localScale = Vector2.one;
        rect.pivot = new Vector2(horizontal ? (direction.x > 0 ? 0 : 1) : 0.5f, horizontal ? 0.5f : (direction.y > 0 ? 0 : 1));
        rect.position = origin;
        Vector2 size = new Vector2(THICKNESS, THICKNESS);
        Vector2 additionalSize = new Vector2(!horizontal ? 0 : absoluteDirection.x - THICKNESS / 2, horizontal ? 0 : absoluteDirection.y - THICKNESS / 2);

        rect.sizeDelta = size;
        await rect.DOSizeDelta(size + additionalSize, ANIM_DURATION).AsyncWaitForCompletion();
    }
}
