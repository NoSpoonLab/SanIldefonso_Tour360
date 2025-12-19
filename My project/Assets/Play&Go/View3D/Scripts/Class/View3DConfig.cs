[System.Serializable]
public class View3DConfig
{
    public string startModel;
    public Model3D[] models;

    public Model3D GetModel(string id)
    {
        foreach (var m in models)
            if (m.id == id)
                return m;
        return null;
    }
}

[System.Serializable]
public class Model3D
{
    public string id;
    public string name;
    public string prefab;
}
