using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tour360Controller : MonoBehaviour
{
    [Header("Esfera de 360° (Material)")]
    public Renderer sphereRenderer;

    [Header("Fade UI")]
    public CanvasGroup fadeCanvas;
    public float fadeDuration = 1f;

    [Header("UI")]
    public TextMeshProUGUI title;

    [Header("Hotspots")]
    public GameObject hotspotPrefab;
    public Transform hotspotContainer;
    private List<GameObject> activeHotspots = new List<GameObject>();

    void Start()
    {
        string start = EnvironmentService.GetStartPoint();

        if (!string.IsNullOrEmpty(start))
        {
            LoadPoint(start,false);
        }
        else
        {
            Debug.LogError("No hay startPoint definido en el JSON.");
        }
    }


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

    public void LoadPoint(string id, bool anim = true)
    {
        var point = EnvironmentService.GetPoint(id);

        if (point == null)
        {
            Debug.LogError("No existe un punto con ID: " + id);
            return;
        }

        ChangeTitle(point.title);
        SpawnHotspots(point);
        LoadImageToSphere(point.imageResource);
    }

    public IEnumerator Fade(float start, float end)
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

            data.pointTester = this;

            if (data.arrow != null)
            {
            data.arrow.transform.localRotation = Quaternion.Euler(
                 hotspot.rotation.x,
                 hotspot.rotation.y,
                 hotspot.rotation.z
            );
            }

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
