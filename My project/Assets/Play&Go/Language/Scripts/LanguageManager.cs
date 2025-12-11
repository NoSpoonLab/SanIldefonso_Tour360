using UnityEngine;
using System.Collections.Generic;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance;

    public string currentLanguage = "es";
    private Dictionary<string, string> translations;

    public delegate void LanguageChanged();
    public event LanguageChanged OnLanguageChanged;

    //Desactivado el singleton
    #region SINGLETON 
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadLanguage(currentLanguage);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private void Start()
    {
        LoadLanguage(currentLanguage);
    }

    public void LoadLanguage(string languageCode)
    {
        currentLanguage = languageCode;

        TextAsset file = Resources.Load<TextAsset>($"Languages/{languageCode}");

        if (file == null)
        {
            Debug.LogError($"No se encontró el archivo de idioma: {languageCode}");
            return;
        }

        translations = JsonUtility.FromJson<LocalizationData>(file.text).ToDictionary();

        OnLanguageChanged?.Invoke();
    }

    public string GetText(string key)
    {
        return translations != null && translations.TryGetValue(key, out string value)
            ? value
            : $"#{key}";
    }
}

[System.Serializable]
public class LocalizationEntry
{
    public string key;
    public string value;
}

[System.Serializable]
public class LocalizationData
{
    public LocalizationEntry[] entries;

    public Dictionary<string, string> ToDictionary()
    {
        var dict = new Dictionary<string, string>();
        foreach (var entry in entries)
            dict[entry.key] = entry.value;
        return dict;
    }
}
