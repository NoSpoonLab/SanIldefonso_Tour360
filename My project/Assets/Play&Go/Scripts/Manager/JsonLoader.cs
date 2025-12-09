using UnityEngine;

public class JsonLoader : MonoBehaviour
{
    private string _jsonResourceName = "Enviroment";

    private TourConfig _config;

    void Awake()
    {
        LoadFromResources();
        EnvironmentService.Initialize(_config);
    }

    void LoadFromResources()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(_jsonResourceName);

        if (jsonFile == null)
        {
            Debug.LogError("No se encontró el archivo JSON en Resources: " + _jsonResourceName);
            return;
        }

        LoadJson(jsonFile.text);
    }

    public void LoadJson(string json)
    {
        _config = JsonUtility.FromJson<TourConfig>(json);

        if (_config == null)
        {
            Debug.LogError("Error deserializando el JSON");
            return;
        }
    }
}
