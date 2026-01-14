using TMPro;
using UnityEngine;

public class LocalizedTextContent : MonoBehaviour
{
    public TMP_Text descriptionText;

    private ILocalizedDescribable currentItem;

    private void OnEnable()
    {
        if (LanguageManager.Instance != null)
        {
            LanguageManager.Instance.OnLanguageChanged += UpdateUI;
            UpdateUI();
        }
    }

    private void OnDisable()
    {
        if (LanguageManager.Instance != null)
        {
            LanguageManager.Instance.OnLanguageChanged -= UpdateUI;
        }
    }

    public void SetItem(ILocalizedDescribable item)
    {
        currentItem = item;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (descriptionText == null) return;

        if (currentItem == null)
        {
            descriptionText.text = "";
            return;
        }

        if (LanguageManager.Instance == null)
        {
            Debug.LogWarning("LanguageManager.Instance es null");
            descriptionText.text = currentItem.descripcion?.es ?? "Sin descripción";
            return;
        }

        string lang = LanguageManager.Instance.currentLanguage;

        descriptionText.text = lang == "es"
            ? currentItem.descripcion?.es ?? "Sin descripción"
            : currentItem.descripcion?.en ?? "No description";
    }
}
