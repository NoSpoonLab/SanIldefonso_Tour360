using UnityEngine;
using UnityEngine.Serialization;

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
public class Model3D : ILocalizedDescribable
{
    public string id;
    public string name;
    public string prefab;
    public string size;
    public Vector3Data[] teleportPoints;
    public Descripcion descripcion;

    Descripcion ILocalizedDescribable.descripcion => descripcion;
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
