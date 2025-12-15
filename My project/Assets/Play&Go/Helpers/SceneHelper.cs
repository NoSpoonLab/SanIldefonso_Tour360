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
}
