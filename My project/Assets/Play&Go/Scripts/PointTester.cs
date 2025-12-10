using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PointTester : MonoBehaviour
{
    [Header("Esfera de 360° (Material)")]
    public Renderer sphereRenderer;

    [Header("Fade UI")]
    public CanvasGroup fadeCanvas;
    public float fadeDuration = 1f;

    [Header("UI")]
    public TextMeshProUGUI title;

    [Header("Camera FOV Transition")]
    public float fovStart = 55f;
    public float fovEnd = 60f;
    public float fovDuration = 1f;

    [Header("Hotspots")]
    public GameObject hotspotPrefab;              
    public Transform hotspotContainer;
    private List<GameObject> activeHotspots = new List<GameObject>();

    void Update()
    {
        if (Keyboard.current.aKey.wasPressedThisFrame)
            LoadPoint("Plaza de España");

        if (Keyboard.current.bKey.wasPressedThisFrame)
            LoadPoint("Interior del teatro");

        if (Keyboard.current.cKey.wasPressedThisFrame)
            LoadPoint("Plaza de los Dolores");

        if (Keyboard.current.dKey.wasPressedThisFrame)
            LoadPoint("Calle de la Reina");
    }

    void LoadPoint(string id)
    {
        var point = EnvironmentService.GetPoint(id);

        if (point == null)
        {
            Debug.LogError("No existe un punto con ID: " + id);
            return;
        }

        Debug.Log("=== CARGANDO PUNTO ===");
        Debug.Log("ID: " + point.id);
        Debug.Log("Título: " + point.title);
        Debug.Log("Descripción: " + point.description);
        Debug.Log("Imagen: " + point.imageResource);
        Debug.Log("Hotspots: " + point.hotspots.Length);

        StartCoroutine(Fade(0f, 1f)); //Cambiarlo
        StartCoroutine(ChangeFOV(fovEnd, fovStart)); //Cambiarlo

        ChangeTitle(point.title);
        SpawnHotspots(point);
        LoadImageToSphere(point.imageResource);

        StartCoroutine(Fade(1f, 0f)); //Cambiarlo
        StartCoroutine(ChangeFOV(fovStart, fovEnd)); //Cambiarlo
    }

    IEnumerator Fade(float start, float end)
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(start, end, t / fadeDuration);
            fadeCanvas.alpha = alpha;
            yield return null;
        }

        fadeCanvas.alpha = end;
    }

    IEnumerator ChangeFOV(float start, float end)
    {
        float t = 0f;

        while (t < fovDuration)
        {
            t += Time.deltaTime;
            Camera.main.fieldOfView = Mathf.Lerp(start, end, t / fovDuration);
            yield return null;
        }

        Camera.main.fieldOfView = end;
    }

    void SpawnHotspots(Point point)
    {
        ClearHotspots();

        if (point.hotspots == null) return;

        foreach (var hotspot in point.hotspots)
        {
            GameObject obj = Instantiate(hotspotPrefab, hotspotContainer);

            obj.transform.localPosition = new Vector3(
                hotspot.position.x,
                hotspot.position.y,
                hotspot.position.z
            );

            Point targetPoint = EnvironmentService.GetPoint(hotspot.target);
            ArrowPrefab data = obj.GetComponent<ArrowPrefab>();

            data.arrow.transform.localRotation = Quaternion.Euler(
                hotspot.rotation.x,
                hotspot.rotation.y,
                hotspot.rotation.z
            );

            data.id = hotspot.target;

            if (targetPoint != null)
            {
                data.image = targetPoint.imageResource;    
                data.description = targetPoint.description;
            }

            obj.name = "Hotspot " + hotspot.target;

            activeHotspots.Add(obj);
        }
    }

    void ClearHotspots()
    {
        foreach (var obj in activeHotspots)
            Destroy(obj);

        activeHotspots.Clear();
    }

    void ChangeTitle(string tittleTxt)
    {
        title.text = tittleTxt;
    }


    void LoadImageToSphere(string imageName)
    {
        string fullPath = "Images360/" + imageName;

        Cubemap cubemap = Resources.Load<Cubemap>(fullPath);

        if (cubemap == null)
        {
            Debug.LogError("No se encontró el cubemap en Resources: " + fullPath);
            return;
        }

        sphereRenderer.material.SetTexture("_Cubemap", cubemap);

        Debug.Log("Cubemap aplicado: " + fullPath);
    }
}
