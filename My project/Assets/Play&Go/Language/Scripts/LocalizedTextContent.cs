using TMPro;
using UnityEngine;

public class LocalizedTextContent : MonoBehaviour
{
    public TMP_Text descriptionText;
    private Model3D currentModel;

    private void OnEnable()
    {
        if (LanguageManager.Instance != null)
        {
            LanguageManager.Instance.OnLanguageChanged += UpdateUI;
            UpdateDescription(); 
        }
    }

    private void OnDisable()
    {
        if (LanguageManager.Instance != null)
        {
            LanguageManager.Instance.OnLanguageChanged -= UpdateUI;
        }
    }

    public void SetModel(Model3D model)
    {
        currentModel = model;
        UpdateDescription();
    }

    private void UpdateDescription()
    {
        if (descriptionText == null) return;

        if (currentModel == null)
        {
            descriptionText.text = "";
            return;
        }

        if (LanguageManager.Instance == null)
        {
            Debug.LogWarning("LanguageManager.Instance es null");
            descriptionText.text = currentModel.descripcion?.es ?? "Sin descripción";
            return;
        }

        string lang = LanguageManager.Instance.currentLanguage;

        if (lang == "es")
            descriptionText.text = currentModel.descripcion?.es ?? "Sin descripción";
        else
            descriptionText.text = currentModel.descripcion?.en ?? "No description";
    }

    private void UpdateUI()
    {
        UpdateDescription();
    }
}