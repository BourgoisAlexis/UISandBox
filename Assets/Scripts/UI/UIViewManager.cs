using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UIViewManager : MonoBehaviour {
    #region Variables
    [SerializeField] private List<UIView> _views = new List<UIView>();
    [SerializeField] private List<UIButtonTab> _tabs = new List<UIButtonTab>();

    private int _currentViewIndex;
    #endregion


    private void Start() {
        Init();
    }

    public async void Init(params object[] parameters) {
        _currentViewIndex = -1;

        for (int i = 0; i < _views.Count; i++)
            await HideView(i, true);

        for (int i = 0; i < _tabs.Count; i++) {
            _tabs[i].SetIndex(i);
            _tabs[i].OnClick.AddListener(ShowView);
        }

        ShowView(0, false, parameters);
    }

    public void ShowView(int index) {
        ShowView(index, false);
    }

    private async void ShowView(int index, bool instant, params object[] parameters) {
        if (index == _currentViewIndex || index < 0 || index >= _views.Count) {
            Debug.LogError($"ShowView index is {index}");
            return;
        }

        if (_currentViewIndex >= 0 && _currentViewIndex < _views.Count)
            await HideView(_currentViewIndex, false);

        await _views[index].Show(instant, parameters);
        _tabs[index].SetSelected(true);

        _currentViewIndex = index;
    }

    private async Task HideView(int index, bool instant) {
        _tabs[index].SetSelected(false);
        await _views[index].Hide(instant);
    }

    public void Back() {
        if (_currentViewIndex > 0)
            ShowView(_currentViewIndex - 1, false);
    }
}
