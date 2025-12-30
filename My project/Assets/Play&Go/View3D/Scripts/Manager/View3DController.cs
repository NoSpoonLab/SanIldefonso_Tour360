using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[HelpURL("https://laboratorio.atlassian.net/wiki/spaces/PG/pages/5055512577/Visualizador+y+Tour+Modelos+3D")]
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
    private string currentID;

    [Header("UI Localizada")]
    public LocalizedTextContent descriptionUI;

    [Header("Audio")]
    public AudioSource audioSource;
    private bool isAudioPlaying = false;
    public GameObject textPlay;
    public GameObject textPause;
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

        if (Keyboard.current.mKey.wasPressedThisFrame)
            PressButtonPlayPause();

        UpdateArrowMovement();
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

        LoadAudio(id);

        if (descriptionUI != null)
        {
            Model3D modelData = EnvironmentView3DService.GetModel(id);
            descriptionUI.SetModel(modelData);
        }

        LoadModelBig(prefab, id);
        LoadModelSmall(prefab, id);

        _teleportPoints = EnvironmentView3DService.GetTeleportPoints(id);
        _currentTeleportIndex = 0;

        if (_teleportPoints != null && _teleportPoints.Length > 0)
        {
            arrow.gameObject.SetActive(true);
            TeleportToIndex(0);
        }
        else
        {
            arrow.gameObject.SetActive(false);
        }

        currentID = id;
    }

    public void TeleportNext()
    {

        if (_teleportPoints == null || _teleportPoints.Length == 0)
            return;

        StartCoroutine(Fade(0f, 1f));

        _currentTeleportIndex++;
        if (_currentTeleportIndex >= _teleportPoints.Length)
            _currentTeleportIndex = 0;

        TeleportToIndex(_currentTeleportIndex);

        StartCoroutine(Fade(1f, 0f));
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
        textPlay.SetActive(true);
        textPause.SetActive(false);
    }

    public void PressButtonBack(string scene) //Add in inspector ButtonBack in Interactable Unity Events
    {
        SceneHelper.LoadScene(scene);
    }

    public void ResetAudioInChangeLanguage() //Add in inspector Español and Ingles in Interactable Unity Events
    {
        audioSource.Stop();
        isAudioPlaying = false;
        LoadAudio(currentID);
        textPlay.SetActive(true);
        textPause.SetActive(false);
    }   

    public void PressButtonPlayPause() //Add in inspector ButtonPlayPause in Interactable Unity Events
    {
        if (audioSource == null || audioSource.clip == null)
            return;

        if (isAudioPlaying)
        {
            audioSource.Pause();
            isAudioPlaying = false;
            textPlay.SetActive(true);
            textPause.SetActive(false);
        }
        else
        {
            audioSource.Play();
            isAudioPlaying = true;
            textPlay.SetActive(false);
            textPause.SetActive(true);
        }
    }
    #endregion

    #region Private Methods
    void UpdateArrowMovement()
    {
        if (arrowTransform == null || Camera.main == null)
            return;

        if (_teleportPoints == null || _teleportPoints.Length < 2)
            return;

        int nextIndex = _currentTeleportIndex + 1;
        if (nextIndex >= _teleportPoints.Length)
            nextIndex = 0;

        Vector3 nextPos = _teleportPoints[nextIndex].ToVector3();

        Vector3 camPos = Camera.main.transform.position;
        camPos.y = arrowTransform.transform.position.y;

        Vector3 direction = nextPos - camPos;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f)
            return;

        arrowTransform.transform.rotation = Quaternion.LookRotation(direction);

    }
    void LoadModelBig(GameObject prefab, string id)
    {
        var scaleBig = 1;
        var posZ = 0;

        string size = EnvironmentView3DService.GetSize(id);        
        switch (size)
        {
            case "small":
                posZ = 0;
                break;

            case "medium":
                posZ = 0;
                break;

            case "large":
                posZ = -70;
                break;
        }

        currentModelBig = Instantiate(prefab, modelContainerBig);
        currentModelBig.transform.localScale = new Vector3(scaleBig, scaleBig, scaleBig);
        currentModelBig.transform.localPosition = new Vector3(0, 0, posZ);
        currentModelBig.name = "Model3D - " + id;
    }

    void LoadModelSmall(GameObject prefab, string id)
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
    void TeleportToIndex(int index)
    {
        Vector3 pos = _teleportPoints[index].ToVector3();
        playerRig.position = pos;
    }

    void LoadAudio(string modelId)
    {
        if (audioSource == null)
            return;

        audioSource.Stop();
        audioSource.clip = null;
        isAudioPlaying = false;

        string languageCode = LanguageManager.Instance.currentLanguage;
        string path = "Audio/" + languageCode + "/" + modelId;

        AudioClip clip = Resources.Load<AudioClip>(path);

        if (clip == null)
        {
            Debug.LogWarning("No se encontró audio en el idioma " + languageCode + " para el modelo: " + modelId);
            return;
        }

        audioSource.clip = clip;
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

        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.clip = null;
            isAudioPlaying = false;
            textPlay.SetActive(true);
            textPause.SetActive(false);
        }

        descriptionUI.descriptionText.text = "";
    }

    void ChangeTitle(string tittleTxt)
    {
        title.text = tittleTxt;
    }
    #endregion
}
