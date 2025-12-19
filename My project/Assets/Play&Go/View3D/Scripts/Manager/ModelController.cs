using UnityEngine;

public class ModelController : MonoBehaviour
{
    public View3DController view3DController;

    public void PressButton(string id)
    {
        view3DController.LoadModel(id);
    }
}
