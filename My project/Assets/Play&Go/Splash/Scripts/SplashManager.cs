using UnityEngine;

public class SplashManager : MonoBehaviour
{
    private FadeTransition fadeTransition;
    private string _sceneToGo = "Menu";
    private int _delay = 3;

    void Start()
    {
        fadeTransition = FindAnyObjectByType<FadeTransition>();

        StartCoroutine(SceneHelper.LoadSceneWithDelayAndFade(_sceneToGo, _delay,fadeTransition));
    }
}
