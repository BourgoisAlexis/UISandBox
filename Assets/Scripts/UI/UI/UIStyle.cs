using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "UIStyle", menuName = "UI/UIStyle")]
public class UIStyle : ScriptableObject {
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public Image.Type ImageType { get; private set; }
    [field: SerializeField] public TMP_FontAsset Font { get; private set; }
    [field: SerializeField] public Color Dark { get; private set; }
    [field: SerializeField] public Color Light { get; private set; }
    [field: SerializeField] public Color MainColor { get; private set; }
    [field: SerializeField] public Color BackgroundColor { get; private set; }
    [field: SerializeField] public Color Transparent { get; private set; }
}
