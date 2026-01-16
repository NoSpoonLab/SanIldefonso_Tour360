using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public static class SceneHelper
{
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static IEnumerator LoadSceneCoroutine(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
    public static IEnumerator LoadSceneWithDelayAndFade(string sceneName, float delay, FadeTransition fade)
    {
        yield return new WaitForSeconds(delay);

        yield return fade.Fade(0f, 1f);

        SceneManager.LoadScene(sceneName);
    }
    public static IEnumerator LoadSceneWithFade(string sceneName, FadeTransition fade)
    {
        yield return fade.Fade(0f, 1f);

        SceneManager.LoadScene(sceneName);
    }
}
