using UnityEngine;

public class ChangeLanguage : MonoBehaviour
{
    public void ChangeLanguageButton(string language)
    {
        LanguageManager.Instance.LoadLanguage(language);
    }
}
