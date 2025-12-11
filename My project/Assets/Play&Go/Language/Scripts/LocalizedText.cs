using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class LocalizedText : MonoBehaviour
{
    public string key;

    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();

        if (text == null)
        {
            Debug.LogError($"LocalizedText ERROR: No TMP_Text encontrado en: {gameObject.name}");
            return;
        }

        if (LanguageManager.Instance == null)
            return;

        UpdateText();
        LanguageManager.Instance.OnLanguageChanged += UpdateText;
    }

    private void Start()
    {
        if (LanguageManager.Instance != null)
        {
            UpdateText();
            LanguageManager.Instance.OnLanguageChanged += UpdateText;
        }
    }

    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            LanguageManager.Instance.LoadLanguage("es");
        }

        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            LanguageManager.Instance.LoadLanguage("en");
        }
    }

    private void OnDestroy()
    {
        if (LanguageManager.Instance != null)
            LanguageManager.Instance.OnLanguageChanged -= UpdateText;
    }

    private void UpdateText()
    {
        if (text == null)
            return;

        if (LanguageManager.Instance == null)
            return;

        text.text = LanguageManager.Instance.GetText(key);
    }
}



