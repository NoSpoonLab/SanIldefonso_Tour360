using UnityEngine;
using UnityEngine.InputSystem;

public class Inputs : MonoBehaviour
{
    public View3DController view3DController;

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
            view3DController.LoadModel("Ayuntamiento");

        if (Keyboard.current.wKey.wasPressedThisFrame)
            view3DController.LoadModel("Capilla de San Juan Nepomuceno");

        if (Keyboard.current.eKey.wasPressedThisFrame)
            view3DController.LoadModel("Casa de la Máquina del Pulimento de Dowling");

        if (Keyboard.current.rKey.wasPressedThisFrame)
            view3DController.LoadModel("Cobertizos para la Leña");

        if (Keyboard.current.tKey.wasPressedThisFrame)
            view3DController.LoadModel("Fuente del Príncipe");

        if (Keyboard.current.yKey.wasPressedThisFrame)
            view3DController.LoadModel("Fábrica de Cristales Labrados y Entrefinos");

        if (Keyboard.current.uKey.wasPressedThisFrame)
            view3DController.LoadModel("Iglesia de los Dolores");

        if (Keyboard.current.iKey.wasPressedThisFrame)
                view3DController.LoadModel("Iglesia de Nuestra Señora del Rosario o de Cristo");

        if (Keyboard.current.oKey.wasPressedThisFrame)
            view3DController.LoadModel("Iglesia del Convento");

        if (Keyboard.current.pKey.wasPressedThisFrame)
            view3DController.LoadModel("Puerta de la Reina");

        if (Keyboard.current.aKey.wasPressedThisFrame)
                view3DController.LoadModel("Puerta de Segovia");

        if (Keyboard.current.sKey.wasPressedThisFrame)
            view3DController.LoadModel("Puerta del Horno");

        if (Keyboard.current.dKey.wasPressedThisFrame)
                view3DController.LoadModel("La Primera Casa del Pulimento");

        if (Keyboard.current.fKey.wasPressedThisFrame)
            view3DController.LoadModel("Palacio de Valsaín");

        if (Keyboard.current.gKey.wasPressedThisFrame)
            view3DController.LoadModel("Fábrica Antigua de Cristales Planos");

        if (Keyboard.current.hKey.wasPressedThisFrame)
            view3DController.LoadModel("Real Fábrica de Cristales Planos de Carlos III");


        if (Keyboard.current.zKey.wasPressedThisFrame)
            view3DController.PressButtonReset();

        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            view3DController.TeleportNext();

        if (Keyboard.current.mKey.wasPressedThisFrame)
            view3DController.PressButtonPlayPause();
    }
}
