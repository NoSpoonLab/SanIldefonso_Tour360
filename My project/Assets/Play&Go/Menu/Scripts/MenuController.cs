using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public void PressButon(string scene) //Add in inspector to BarrioAlto&BajoBtn in Interactable Unity Event Wrapper
    {
        SceneHelper.LoadScene(scene);
    }
}
