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

    public void Start()
    {
        _cam = Camera.main;

        if (idTxt != null)
            idTxt.text = id;

        LoadSprite();
    }

    void Update()
    {
        if (canvas != null && _cam != null)
        {
            Vector3 camPos = Camera.main.transform.position;
            Vector3 lookPos = new Vector3(camPos.x, canvas.transform.position.y, camPos.z);

            canvas.transform.LookAt(lookPos);
            canvas.transform.Rotate(0, 180f, 0);
        }
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
