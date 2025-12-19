using UnityEngine;

public class JsonLoaderView3D : MonoBehaviour
{
    private string _jsonResourceName = "EnviromentView3D";

    private View3DConfig _config;

    void Awake()
    {
        LoadFromResources();
        EnvironmentView3DService.Initialize(_config);
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
        _config = JsonUtility.FromJson<View3DConfig>(json);

        if (_config == null)
        {
            Debug.LogError("Error deserializando el JSON");
            return;
        }
    }
}
