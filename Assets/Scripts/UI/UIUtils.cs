using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public static class UIUtils {
    private static RectTransform _dummy;

    private static GameObject GenerateUIElement(RectTransform parent, int siblingIndex) {
        GameObject go = new GameObject();
        RectTransform rect = go.AddComponent<RectTransform>();
        rect.SetParent(parent);
        rect.SetSiblingIndex(siblingIndex);
        rect.localPosition = Vector3.zero;
        rect.localScale = Vector3.one;
        rect.localRotation = Quaternion.identity;
        return go;
    }

    public static async Task<UILine> GenerateUILine(RectTransform parent, UIStyle uiStyle, int siblingIndex, Vector2 origin, Vector2 destination) {
        GameObject go = GenerateUIElement(parent, siblingIndex);
        UILine line = go.AddComponent<UILine>();

        line.SetUIStyle(uiStyle);
        await line.Init(origin, destination);

        return line;
    }

    public static async Task<UILayout> GenerateUILayout(RectTransform parent, UIStyle uiStyle, int siblingIndex, RectTransform[] elements, Vector2 margin) {
        GameObject go = GenerateUIElement(parent, siblingIndex);
        go.AddComponent<Image>();
        UILayout layout = go.AddComponent<UILayout>();

        layout.SetUIStyle(uiStyle);
        await layout.Init(elements, margin);

        return layout;
    }

    public static UITrail AddUITrail(RectTransform target, UIStyle uiStyle) {
        UITrail trail = target.gameObject.AddComponent<UITrail>();

        trail.SetUIStyle(uiStyle);
        trail.Init();

        return trail;
    }

    public static Vector2 GetPercentagePosition(Transform element, Vector2 destination) {
        return GetPercentagePosition(element as RectTransform, destination);
    }

    public static Vector2 GetPercentagePosition(RectTransform element, Vector2 destination) {
        if (_dummy == null) {
            GameObject go = GenerateUIElement(element.parent as RectTransform, 0);
            go.name = "Dummy";
            _dummy = go.transform as RectTransform;
        }

        _dummy.SetParent(element.parent);
        _dummy.anchorMin = destination;
        _dummy.anchorMax = destination;
        _dummy.anchoredPosition = Vector2.zero;

        return _dummy.localPosition;
    }


    //Extension methods
    public static Color SetAlpha(this Color color, float alpha) {
        color.a = alpha;
        return color;
    }

    public static Color GreyOut(this Color color) {
        return color * Color.grey;
    }
}
