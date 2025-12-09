using UnityEngine;

public static class EnvironmentService
{
    private static TourConfig _data;

    public static bool IsInitialized => _data != null;

    public static void Initialize(TourConfig env)
    {
        _data = env;
        Debug.Log("EnvironmentService inicializado correctamente.");
    }

    public static Point GetPoint(string id)
    {
        if (!IsInitialized)
        {
            Debug.LogError("EnvironmentService no está inicializado.");
            return null;
        }

        return _data.GetPoint(id);
    }

    public static string GetTitle(string id)
    {
        var p = GetPoint(id);
        return p?.title;
    }

    public static string GetDescription(string id)
    {
        var p = GetPoint(id);
        return p?.description;
    }

    public static string GetImageResource(string id)
    {
        var p = GetPoint(id);
        return p?.imageResource;
    }

    public static Hotspot[] GetHotspots(string id)
    {
        var p = GetPoint(id);
        return p?.hotspots;
    }

    public static string GetStartPoint() => _data.startPoint;
}
