using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class TourConfig
{
    public string startPoint;
    public Point[] points;

    public Point GetPoint(string id)
    {
        foreach (var p in points)
            if (p.id == id)
                return p;

        Debug.LogWarning($"Punto con id '{id}' no encontrado.");
        return null;
    }
}

[Serializable]
public class Point : ILocalizedDescribable
{
    public string id;
    public string imageResource;
    public string title;
    public Hotspot[] hotspots;
    public Descripcion descripcion;

    Descripcion ILocalizedDescribable.descripcion => descripcion;
}

[Serializable]
public class Descripcion
{
    public string es;
    public string en;

    public string Get(string lang)
    {
        return lang == "es" ? es : en;
    }
}

[Serializable]
public class Hotspot
{
    public string target;
    public Vector3 position;
    public Rotation rotation; 
}

[System.Serializable]
public class Rotation
{
    public float x;
    public float y;
    public float z;
}
