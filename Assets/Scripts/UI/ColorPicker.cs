using UnityEngine;
using UnityEngine.Events;

public class ColorPicker : MonoBehaviour
{
    [SerializeField] private ColorSlot slotPrefab;
    [SerializeField] private Transform slotsHolder;
    [SerializeField] private Color[] colors;

    [System.Serializable] public class ColorUnityEvent : UnityEvent<Color> { }
    [Space(20)]
    public ColorUnityEvent OnColorChanged;

    private bool isInitialized;
    private ColorSlot[] slots;

    private void Start()
    {
        if (!isInitialized)
            Populate();
    }

    #region Callbacks
    private void OnSlotClicked(Color color)
    {
        OnColorChanged?.Invoke(color);
    }
    #endregion

    public void Populate()
    {
        slots = new ColorSlot[colors.Length];
        
        ColorSlot currSlot;
        for (int i = 0; i < colors.Length; i++)
        {
            currSlot = Instantiate(slotPrefab, slotsHolder);
            currSlot.Initialize(colors[i]);
            slots[i] = currSlot;
            slots[i].OnSlotClicked.AddListener(OnSlotClicked);
        }

        isInitialized = true;
    }
    
    public Color GetColor(int i)
    {
        if(i >= 0 && i < colors.Length)
            return colors[i];

        return Color.white;
    }
}
