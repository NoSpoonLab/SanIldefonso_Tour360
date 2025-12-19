using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class View3DController : MonoBehaviour
{
    [Header("Model Parent")]
    public Transform modelContainerBig;
    public Transform modelContainerSmall;

    [Header("Current Model")]
    private GameObject currentModelBig;
    private GameObject currentModelSmall;

    private int scaleBig;
    private int scaleSmall;

    void Start()
    {
        string startModel = EnvironmentView3DService.GetStartModel();

        if (!string.IsNullOrEmpty(startModel))
        {
            LoadModel(startModel);
        }
        else
        {
            Debug.LogError("No hay startModel definido en el JSON de View3D.");
        }
    }

    void Update()
    {
        if (Keyboard.current.aKey.wasPressedThisFrame)
            LoadModel("Capilla de San Juan Nepomuceno");

        if (Keyboard.current.bKey.wasPressedThisFrame)
            LoadModel("Iglesia de los Dolores");

        if (Keyboard.current.cKey.wasPressedThisFrame)
            LoadModel("Ayuntamiento");

        if (Keyboard.current.dKey.wasPressedThisFrame)
            LoadModel("Puerta del Horno");
    }

    public void LoadModel(string id)
    {
        Debug.Log("=== CARGANDO MODELO 3D ===");
        Debug.Log("ID: " + id);

        ClearCurrentModel();

        GameObject prefab = Resources.Load<GameObject>("Models3D/" + id);

        if (prefab == null)
        {
            Debug.LogError("No se encontró el prefab del modelo: " + id);
            return;
        }

        LoadModelBig(prefab,id);
        LoadModelSmall(prefab,id);
    }

    public void LoadModelBig(GameObject prefab,string id)
    {
        scaleBig = 200;

        currentModelBig = Instantiate(prefab, modelContainerBig);
        currentModelBig.transform.localScale = new Vector3(scaleBig, scaleBig, scaleBig);
        currentModelBig.transform.localPosition = Vector3.zero;
        currentModelBig.name = "Model3D - " + id;
    }

    public void LoadModelSmall(GameObject prefab, string id)
    {
        scaleSmall = 8;

        currentModelSmall = Instantiate(prefab, modelContainerSmall);
        currentModelSmall.transform.localScale = new Vector3(scaleSmall, scaleSmall, scaleSmall);
        currentModelSmall.transform.localPosition = Vector3.zero;
        currentModelSmall.name = "Model3D - " + id;
    }

    void ClearCurrentModel()
    {
        if (currentModelBig != null)
        {
            Destroy(currentModelBig);
            currentModelBig = null;
        }

        if (currentModelSmall != null)
        {
            Destroy(currentModelSmall);
            currentModelSmall = null;
        }
    }
}
