using UnityEngine;

public static class EnvironmentView3DService
{
    private static View3DConfig _data;

    public static bool IsInitialized => _data != null;

    public static void Initialize(View3DConfig config)
    {
        _data = config;
        Debug.Log("EnvironmentView3DService inicializado correctamente.");
    }

    public static Model3D GetModel(string id)
    {
        if (!IsInitialized)
        {
            Debug.LogError("EnvironmentView3DService no está inicializado.");
            return null;
        }

        return _data.GetModel(id);
    }

    public static string GetStartModel()
    {
        if (!IsInitialized)
        {
            Debug.LogError("EnvironmentView3DService no está inicializado.");
            return null;
        }

        return _data.startModel;
    }
}
