using System.Collections;
using UnityEngine;

public class FadeTransition : MonoBehaviour
{
    private float fadeDuration = 1f;

    public IEnumerator Fade(float start, float end)
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(start, end, t / fadeDuration);
            GetComponent<CanvasGroup>().alpha = alpha;
            yield return null;
        }

        GetComponent<CanvasGroup>().alpha = end;
    }
}
