using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button routeButton;
    public Button neighborHood;

    private string routeScene;
    private string neighborHoodScene = "Tour360";

    private void Start()
    {
        routeButton.onClick.AddListener(() => SceneHelper.LoadScene(routeScene));
        neighborHood.onClick.AddListener(() => SceneHelper.LoadScene(neighborHoodScene));
    }
}
