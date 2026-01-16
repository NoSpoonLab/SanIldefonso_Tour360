using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    private FadeTransition fadeTransition;

    private void Awake()
    {
        fadeTransition = FindAnyObjectByType<FadeTransition>();

        StartCoroutine(fadeTransition.Fade(1f, 0f));
    }

    public void Update()
    {
        if (Keyboard.current.aKey.wasPressedThisFrame)
            PressButon("Tour360");

        if (Keyboard.current.aKey.wasPressedThisFrame)
            PressButon("View3D");
    }

    public void PressButon(string scene) //Add in inspector to BarrioAlto&BajoBtn in Interactable Unity Event Wrapper
    {
        StartCoroutine(SceneHelper.LoadSceneWithFade(scene, fadeTransition));
    }
}
