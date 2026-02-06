using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToggleVisualSwitcher : MonoBehaviour
{
     [Header("Language")]
    [SerializeField] private string languageCode; // "es", "en"

    [Header("Toggle")]
    [SerializeField] private Toggle toggle;

    [Header("Visuals")]
    [SerializeField] private Image targetImage;
    [SerializeField] private TMP_Text targetText;

    [Header("Sprites")]
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite inactiveSprite;

    [Header("Text Colors")]
    [SerializeField] private Color activeTextColor;
    [SerializeField] private Color inactiveTextColor;

    private void Start()
    {
        // Sincronizar estado inicial
        SyncWithLanguageManager();

        // Escuchar cambios
        toggle.onValueChanged.AddListener(OnToggleChanged);
        LanguageManager.Instance.OnLanguageChanged += SyncWithLanguageManager;
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnToggleChanged);

        if (LanguageManager.Instance != null)
            LanguageManager.Instance.OnLanguageChanged -= SyncWithLanguageManager;
    }

    private void SyncWithLanguageManager()
    {
        bool isActiveLanguage = LanguageManager.Instance.currentLanguage == languageCode;

        toggle.SetIsOnWithoutNotify(isActiveLanguage);
        UpdateVisual(isActiveLanguage);
    }

    private void OnToggleChanged(bool isOn)
    {
        UpdateVisual(isOn);

        if (isOn && LanguageManager.Instance.currentLanguage != languageCode)
        {
            LanguageManager.Instance.LoadLanguage(languageCode);
        }
    }

    private void UpdateVisual(bool isActive)
    {
        if (targetImage != null)
            targetImage.sprite = isActive ? activeSprite : inactiveSprite;

        if (targetText != null)
            targetText.color = isActive ? activeTextColor : inactiveTextColor;
    }
}
