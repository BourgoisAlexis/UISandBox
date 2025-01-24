using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TestButton : MonoBehaviour {
    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private UIStyle _uiStyle;
    [SerializeField] private UIView _view;
    [SerializeField] private UIRadialMenu _radial;

    private List<RectTransform> _buttons = new List<RectTransform>();

    public async void Test() {
        int delay = 500;
        RectTransform parent = _view.GetComponent<RectTransform>();

        await _view.Hide();
        await Task.Delay(delay);

        await _view.Show();
        await Task.Delay(delay);

        UITrail trail0 = UIUtils.AddUITrail(transform as RectTransform, _uiStyle);
        await trail0.Move(UIUtils.GetPercentagePosition(trail0.transform, new Vector2(0.9f, 0.9f)), true);
        await Task.Delay(delay);

        for (int i = 0; i < 10; i++) {
            GameObject go = Instantiate(_buttonPrefab, parent);
            RectTransform rect = go.GetComponent<RectTransform>();
            _buttons.Add(rect);
            rect.localPosition = Vector2.left * 10000;
        }

        await _radial.Init(_buttons.ToArray(), 360, 200, true);
        await Task.Delay(delay);

        Vector2 margin = new Vector2(30, 300);
        UILayout layout1 = await UIUtils.GenerateUILayout(parent, _uiStyle, 0, null, margin);
        UITrail trail1 = UIUtils.AddUITrail(layout1.transform as RectTransform, _uiStyle);
        await trail1.Move(UIUtils.GetPercentagePosition(trail1.transform, new Vector2(0.1f, 0.5f)), true);

        UILayout layout2 = await UIUtils.GenerateUILayout(parent, _uiStyle, 0, null, margin);
        UITrail trail2 = UIUtils.AddUITrail(layout2.transform as RectTransform, _uiStyle);
        await trail2.Move(UIUtils.GetPercentagePosition(trail2.transform, new Vector2(0.5f, 0.7f)), true);

        UILayout layout3 = await UIUtils.GenerateUILayout(parent, _uiStyle, 0, null, margin);
        UITrail trail3 = UIUtils.AddUITrail(layout3.transform as RectTransform, _uiStyle);
        await trail3.Move(UIUtils.GetPercentagePosition(trail3.transform, new Vector2(0.9f, 0.3f)), true);

        await UIUtils.GenerateUILine(parent, _uiStyle, 0, layout1.transform.position, layout2.transform.position);
        await UIUtils.GenerateUILine(parent, _uiStyle, 0, layout2.transform.position, layout3.transform.position);
    }
}
