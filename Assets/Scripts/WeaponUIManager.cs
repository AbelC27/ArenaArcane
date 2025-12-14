using UnityEngine;
using UnityEngine.UI;

public class WeaponUIManager : MonoBehaviour
{
    [Header("UI References")]
    public Image[] weaponSlots; // Drag Slot_1, Slot_2, Slot_3 here in order

    [Header("Visual Settings")]
    public Color selectedColor = Color.white;
    public Color unselectedColor = new Color(0.5f, 0.5f, 0.5f, 0.5f); // Gray and transparent
    public float selectedScale = 1.2f; // Active weapon gets 20% bigger
    public float defaultScale = 1.0f;

    void Start()
    {
        // Highlight the first weapon by default
        UpdateWeaponUI(0);
    }

    public void UpdateWeaponUI(int activeIndex)
    {
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            if (i == activeIndex)
            {
                // Highlight Selected
                weaponSlots[i].color = selectedColor;
                weaponSlots[i].rectTransform.localScale = Vector3.one * selectedScale;
            }
            else
            {
                // Dim Unselected
                weaponSlots[i].color = unselectedColor;
                weaponSlots[i].rectTransform.localScale = Vector3.one * defaultScale;
            }
        }
    }
}