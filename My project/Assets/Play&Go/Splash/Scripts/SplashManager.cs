using UnityEngine;

public class SplashManager : MonoBehaviour
{
    private string _sceneToGo = "Menu";
    private int _delay = 3;

    void Start()
    {
        StartCoroutine(SceneHelper.LoadSceneCoroutine(_sceneToGo, _delay));
    }
}
