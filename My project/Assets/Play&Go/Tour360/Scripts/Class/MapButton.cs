using UnityEngine;
using UnityEngine.UI;

public class MapButton : MonoBehaviour
{
    public string pointId;

    [Header("Visuals")]
    public Image background;
    public Color normalColor = Color.white;
    public Color selectedColor = Color.yellow;

    public void SetSelected(bool selected)
    {
        if (background != null)
            background.color = selected ? selectedColor : normalColor;
    }
}
