using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapButton : MonoBehaviour
{
    public string pointId;

    [Header("Visuals")]
    public Image background;
    public TMP_Text label;

    public Color normalTextColor;
    public Color selectedTextColor;

    [Header("Background Sprites")]
    public Sprite normalSprite;   
    public Sprite selectedSprite; 

    public void SetSelected(bool selected)
    {
        if (background != null)
        {
            background.sprite = selected ? selectedSprite : normalSprite;
        }

        if (label != null)
        {
            label.color = selected ? selectedTextColor : normalTextColor;
        }
    }
}
