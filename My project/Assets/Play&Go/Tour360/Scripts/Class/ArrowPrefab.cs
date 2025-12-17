using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArrowPrefab : MonoBehaviour
{
    [Header("Info")]
    public string id;
    public string image;
    public string description;

    [Header("FBX")]
    public GameObject arrow;

    [Header("UI")]
    public GameObject canvas;
    public TextMeshProUGUI idTxt;
    public Image imageUI;

    private Camera _cam;

    [NonSerialized]
    public Tour360Controller pointTester;

    public void Start()
    {
        _cam = Camera.main;

        if (idTxt != null)
            idTxt.text = id;

        LoadSprite();
    }

    public void PressButton() //Add in inspector to CirclePoint in Interactable Unity Event Wrapper
    {
        pointTester.LoadPoint(id);
    }

    public void ActiveOrDesactiveImage(bool active) //Add in inspector to CirclePoint in Interactable Unity Event Wrapper
    {
        imageUI.gameObject.SetActive(active);
    }

    private void LoadSprite()
    {
        if (string.IsNullOrEmpty(image))
            return;

        Sprite sprite = Resources.Load<Sprite>("Thumbnails/" + image);

        if (sprite == null)
        {
            Debug.LogWarning("No se pudo cargar el sprite: " + image);
            return;
        }

        if (imageUI != null)
            imageUI.sprite = sprite;
    }
}
