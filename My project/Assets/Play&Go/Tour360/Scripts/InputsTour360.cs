using UnityEngine;
using UnityEngine.InputSystem;

public class InputsTour360 : MonoBehaviour
{
    public Tour360Controller tour360Controller;
    public AudioGuideController audioGuideController;

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            tour360Controller.PressButtonBack("Menu");

        if (Keyboard.current.aKey.wasPressedThisFrame)
            tour360Controller.LoadPoint("Plaza de España");

        if (Keyboard.current.bKey.wasPressedThisFrame)
            tour360Controller.LoadPoint("Interior del teatro");

        if (Keyboard.current.cKey.wasPressedThisFrame)
            tour360Controller.LoadPoint("Plaza de los Dolores");

        if (Keyboard.current.dKey.wasPressedThisFrame)
            tour360Controller.LoadPoint("Calle de la Reina");

        if (Keyboard.current.pKey.wasPressedThisFrame)
            audioGuideController.PressButtonPlayPause();
    }
}
