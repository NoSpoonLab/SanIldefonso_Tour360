using UnityEngine;

public class MapController : MonoBehaviour
{
    public Tour360Controller Tour360Controller;

    public void PressButton(string point) //Add in inspector to Map in Interactable Unity Event Wrapper
    {
        Tour360Controller.LoadPoint(point);
    }
}
