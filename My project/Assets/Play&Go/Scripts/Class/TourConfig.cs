using System;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

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
public class Point
{
    public string id;
    public string imageResource;
    public string title;
    public string description;
    public Hotspot[] hotspots;
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
