using UnityEngine;

public class TourNavigationController : MonoBehaviour
{
    public Transform playerRig;
    private Vector3Data[] _teleportPoints;
    public GameObject arrowTransform;
    private int _currentTeleportIndex = 0;
    public GameObject arrow;

    private FadeTransition fadeTransition;

    private void Start()
    {
        fadeTransition = FindAnyObjectByType<FadeTransition>();
    }
    void Update()
    {
        UpdateArrowMovement();
    }

    public void LoadModel(string id)
    {
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
    }

    public void TeleportNext() //Add in inspector ButtonArrow in Interactable Unity Events
    {

        if (_teleportPoints == null || _teleportPoints.Length == 0)
            return;

        if(fadeTransition != null)
            StartCoroutine(fadeTransition.Fade(0f, 1f));

        _currentTeleportIndex++;
        if (_currentTeleportIndex >= _teleportPoints.Length)
            _currentTeleportIndex = 0;

        TeleportToIndex(_currentTeleportIndex);

        if (fadeTransition != null)
            StartCoroutine(fadeTransition.Fade(1f, 0f));
    }

    void TeleportToIndex(int index)
    {
        Vector3 pos = _teleportPoints[index].ToVector3();
        playerRig.position = pos;
    }

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
}
