using System.Collections.Generic;
using UnityEngine;
using static OVRInput;

public class MapController : MonoBehaviour
{
    public Tour360Controller tour360Controller;

    [Header("Map Buttons")]
    public List<MapButton> mapButtons;

    private void OnEnable()
    {
        if (tour360Controller != null)
            tour360Controller.OnPointChanged += UpdateSelection;
    }

    public void PressButton(string point) //Add in inspector to Map in Interactable Unity Event Wrapper
    {
        tour360Controller.LoadPoint(point);

        UpdateSelection(point);
    }

    void UpdateSelection(string activePointId)
    {
        foreach (var btn in mapButtons)
        {
            btn.SetSelected(btn.pointId == activePointId);
        }
    }

    void OnDestroy()
    {
        if (tour360Controller != null)
            tour360Controller.OnPointChanged -= UpdateSelection;
    }
}
