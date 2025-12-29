using UnityEngine;

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
    public Descripcion descripcion;
    public string prefab;
    public string size;
    public Vector3Data[] teleportPoints;
}

[System.Serializable]
public class Descripcion
{
    public string es;
    public string en;
}

[System.Serializable]
public class Vector3Data
{
    public float x;
    public float y;
    public float z;

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }
}
