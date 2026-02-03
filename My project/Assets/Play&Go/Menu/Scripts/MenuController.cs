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
        if (Keyboard.current.sKey.wasPressedThisFrame)
            PressButon("Tour360");

        if (Keyboard.current.aKey.wasPressedThisFrame)
            PressButon("View3D");

        if (Keyboard.current.eKey.wasPressedThisFrame)
            LanguageManager.Instance.LoadLanguage("es");

        if (Keyboard.current.iKey.wasPressedThisFrame)
            LanguageManager.Instance.LoadLanguage("en");
    }

    public void PressButon(string scene) //Add in inspector to BarrioAlto&BajoBtn in Interactable Unity Event Wrapper
    {
        StartCoroutine(SceneHelper.LoadSceneWithFade(scene, fadeTransition));
    }
}
