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
    [Header("Model Parent")]
    public Transform modelContainerBig;
    public Transform modelContainerSmall;

    [Header("Current Model")]
    private GameObject _currentModelBig;
    private GameObject _currentModelSmall;

    [Header("UI")]
    public TextMeshProUGUI title;
    public GameObject helpText;
    public GameObject helpTextRotateYou;
    private string _currentID;

    [Header("UI Localizada")]
    public LocalizedTextContent descriptionUI;
    public GameObject descriptionText;

    [Header("AudioGuide")]
    public AudioGuideController audioGuideController;

    [Header("NavigationArrowIndicator")]
    public TourNavigationController tourNavigationController;

    [Header("CityController")]
    public CityController cityController;

    private FadeTransition fadeTransition;

    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        fadeTransition = FindAnyObjectByType<FadeTransition>();

        StartCoroutine(fadeTransition.Fade(1f, 0f));

    }
    #endregion

    #region Public Methods
    public void LoadModel(string id)
    {
        ClearCurrentModel();

        GameObject prefab = Resources.Load<GameObject>("Models3D/" + id);

        if (prefab == null)
        {
            Debug.LogError("No se encontró el prefab del modelo: " + id);
            return;
        }

        ChangeTitle(id);

        audioGuideController.LoadAudio(id);

        if (descriptionUI != null)
        {
            descriptionText.GetComponent<LocalizedText>().enabled = false;
            descriptionUI.enabled = true;
            Model3D modelData = EnvironmentView3DService.GetModel(id);
            descriptionUI.SetItem(modelData);
        }

        tourNavigationController.LoadModel(id);

        helpText.gameObject.SetActive(true);
        helpTextRotateYou.gameObject.SetActive(true);

        LoadModelBig(prefab, id);
        LoadModelSmall(prefab, id);

        _currentID = id;
    }
    #endregion

    #region Inputs Methods
    public void PressButtonReset() //Add in inspector ButtonReset in Interactable Unity Events
    {
        ClearCurrentModel();
        helpText.gameObject.SetActive(false);
        helpTextRotateYou.gameObject.SetActive(false);
        cityController.modelSanIldefonso.SetActive(true);
        tourNavigationController.arrow.SetActive(false);
        title.text = "";
        audioGuideController.textPlay.SetActive(true);
        audioGuideController.textPause.SetActive(false);
    }

    public void PressButtonBack(string scene) //Add in inspector ButtonBack in Interactable Unity Events
    {
        //SceneHelper.LoadScene(scene);
        StartCoroutine(SceneHelper.LoadSceneWithFade(scene, fadeTransition));
    }
    #endregion

    #region Private Methods
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

        _currentModelBig = Instantiate(prefab, modelContainerBig);
        _currentModelBig.transform.localScale = Vector3.one * scaleBig;
        Vector3 pos = _currentModelBig.transform.localPosition;
        pos.z = posZ;
        _currentModelBig.transform.localPosition = pos;
        _currentModelBig.name = "Model3D - " + id;

        ActiveSoundFX(_currentModelBig);
    }

    void ActiveSoundFX(GameObject model)
    {
        if (model == null)
            return;

        if (model.transform.childCount > 0)
        {
            Transform child = model.transform.GetChild(0);
            if (child != null)
                child.gameObject.SetActive(true);
        }
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


        _currentModelSmall = Instantiate(prefab, modelContainerSmall);
        _currentModelSmall.transform.localScale = new Vector3(scale, scale, scale);
        _currentModelSmall.transform.localPosition = Vector3.zero;
        _currentModelSmall.name = "Model3D - " + id;
    }


    void ClearCurrentModel()
    {
        cityController.modelSanIldefonso.SetActive(false);

        if (_currentModelBig != null)
        {
            Destroy(_currentModelBig);
            _currentModelBig = null;
        }

        if (_currentModelSmall != null)
        {
            Destroy(_currentModelSmall);
            _currentModelSmall = null;
        }

        if (audioGuideController.audioSource != null)
        {
            audioGuideController.Clear();
        }

        descriptionText.GetComponent<LocalizedText>().enabled = true;
        descriptionUI.enabled = false;
    }

    void ChangeTitle(string tittleTxt)
    {
        title.text = tittleTxt;
    }
    #endregion
}
