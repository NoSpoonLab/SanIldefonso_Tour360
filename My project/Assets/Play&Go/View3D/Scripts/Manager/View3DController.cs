using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class View3DController : MonoBehaviour
{
    #region Variables
    [Header("Player / Camera Rig")]
    public Transform playerRig;
    private Vector3Data[] _teleportPoints;
    private int _currentTeleportIndex = 0;
    public GameObject arrow;

    [Header("Model Parent")]
    public Transform modelContainerBig;
    public Transform modelContainerSmall;

    [Header("Current Model")]
    private GameObject currentModelBig;
    private GameObject currentModelSmall;

    [Header("Objects")]
    public GameObject modelSanIldefonso;
    public GameObject arrowTransform;

    [Header("Fade UI")]
    public CanvasGroup fadeCanvas;
    public float fadeDuration = 1f;

    [Header("UI")]
    public TextMeshProUGUI title;

    [Header("Arrow Rotation")]
    private readonly float[] arrowYRotations =
    {
        -90f,
         0f,
        -90f,
        180f,
        90f
    };
    #endregion

    void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
            LoadModel("Ayuntamiento");

        if (Keyboard.current.wKey.wasPressedThisFrame)
            LoadModel("Capilla de San Juan Nepomuceno");

        if (Keyboard.current.eKey.wasPressedThisFrame)
            LoadModel("Casa de la Máquina del Pulimento de Dowling");

        if (Keyboard.current.rKey.wasPressedThisFrame)
            LoadModel("Cobertizos para la Leña");

        if (Keyboard.current.tKey.wasPressedThisFrame)
            LoadModel("Fuente del Príncipe");

        if (Keyboard.current.yKey.wasPressedThisFrame)
            LoadModel("Fábrica de Cristales Labrados y Entrefinos");

        if (Keyboard.current.uKey.wasPressedThisFrame)
            LoadModel("Iglesia de los Dolores");

        if (Keyboard.current.iKey.wasPressedThisFrame)
            LoadModel("Iglesia de Nuestra Señora del Rosario o de Cristo");

        if (Keyboard.current.oKey.wasPressedThisFrame)
            LoadModel("Iglesia del Convento");

        if (Keyboard.current.pKey.wasPressedThisFrame)
            LoadModel("Puerta de la Reina");

        if (Keyboard.current.aKey.wasPressedThisFrame)
            LoadModel("Puerta de Segovia");

        if (Keyboard.current.sKey.wasPressedThisFrame)
            LoadModel("Puerta del Horno");

        if (Keyboard.current.dKey.wasPressedThisFrame)
            LoadModel("La Primera Casa del Pulimento");

        if (Keyboard.current.fKey.wasPressedThisFrame)
            LoadModel("Palacio de Valsaín");

        if (Keyboard.current.gKey.wasPressedThisFrame)
            LoadModel("Fábrica Antigua de Cristales Planos");

        if (Keyboard.current.hKey.wasPressedThisFrame)
            LoadModel("Real Fábrica de Cristales Planos de Carlos III");


        if (Keyboard.current.zKey.wasPressedThisFrame)
            PressButtonReset();

        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            TeleportNext();
    }


    #region Public Methods

    public void LoadModel(string id, bool anim = true)
    {
        ClearCurrentModel();

        GameObject prefab = Resources.Load<GameObject>("Models3D/" + id);

        if (prefab == null)
        {
            Debug.LogError("No se encontró el prefab del modelo: " + id);
            return;
        }

        ChangeTitle(id);

        LoadModelBig(prefab, id);
        LoadModelSmall(prefab, id);

        _teleportPoints = EnvironmentView3DService.GetTeleportPoints(id);
        _currentTeleportIndex = 0;

        if (_teleportPoints != null && _teleportPoints.Length > 0)
        {
            arrow.gameObject.SetActive(true);
        }
        else
        {
            arrow.gameObject.SetActive(false);
        }
    }

    public void TeleportNext()
    {
        StartCoroutine(Fade(0f, 1f));

        if (_teleportPoints == null || _teleportPoints.Length == 0)
            return;

        _currentTeleportIndex++;
        if (_currentTeleportIndex >= _teleportPoints.Length)
            _currentTeleportIndex = 0;

        TeleportToIndex(_currentTeleportIndex);

        StartCoroutine(Fade(1f, 0f));
    }



    public void LoadModelBig(GameObject prefab, string id)
    {
        var scaleBig = 1;

        currentModelBig = Instantiate(prefab, modelContainerBig);
        currentModelBig.transform.localScale = new Vector3(scaleBig, scaleBig, scaleBig);
        currentModelBig.transform.localPosition = Vector3.zero;
        currentModelBig.name = "Model3D - " + id;
    }

    public void LoadModelSmall(GameObject prefab, string id)
    {
        string size = EnvironmentView3DService.GetSize(id);

        float scale = 0;

        switch (size)
        {
            case "small":
                scale = 0.09f;
                break;

            case "medium":
                scale = 0.03f;
                break;

            case "large":
                scale = 0.015f;
                break;
        }


        currentModelSmall = Instantiate(prefab, modelContainerSmall);
        currentModelSmall.transform.localScale = new Vector3(scale, scale, scale);
        currentModelSmall.transform.localPosition = Vector3.zero;
        currentModelSmall.name = "Model3D - " + id;
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
    #endregion

    #region Inputs Methods
    public void PressButtonReset() //Add in inspector ButtonReset in Interactable Unity Events
    {
        ClearCurrentModel();
        modelSanIldefonso.SetActive(true);
        arrow.SetActive(false);
        title.text = "";
    }
    #endregion

    #region Private Methods
    void TeleportToIndex(int index)
    {
        Vector3 pos = _teleportPoints[index].ToVector3();
        playerRig.position = pos;

        if (arrowTransform != null && arrowYRotations.Length > 0)
        {
            int rotIndex = index % arrowYRotations.Length;
            Vector3 euler = arrowTransform.transform.localEulerAngles;
            arrowTransform.transform.localRotation = Quaternion.Euler(
                euler.x,
                arrowYRotations[rotIndex],
                euler.z
            );
        }
    }

    void ClearCurrentModel()
    {
        modelSanIldefonso.SetActive(false);

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

    void ChangeTitle(string tittleTxt)
    {
        title.text = tittleTxt;
    }
    #endregion
}
