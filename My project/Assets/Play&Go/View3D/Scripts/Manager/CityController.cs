using UnityEngine;

public class CityController : MonoBehaviour
{
    public GameObject modelSanIldefonso;
    public View3DController view3DController;

    public void PressButton(string id) //Add in button Point in the markers of the MaquetaSanIldefonso
    {
        view3DController.LoadModel(id);
    }
}
