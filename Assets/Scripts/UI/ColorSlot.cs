using UnityEngine;
using UnityEngine.UI;
using static ColorPicker;

public class ColorSlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Button btn;

    [Space(20)]
    public ColorUnityEvent OnSlotClicked;

    private Color color;

    public void Initialize(Color c)
    {
        icon.color = c;
        color = c;

        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => OnSlotClicked?.Invoke(color));
    }

    public Color GetColor()
    {
        return color;
    }
}
