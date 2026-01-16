using UnityEngine;

public class TourNavigationController : MonoBehaviour
{
    public Transform playerRig;
    private Vector3Data[] _teleportPoints;
    public GameObject arrowTransform;
    private int _currentTeleportIndex = 0;
    public GameObject arrow;

    private FadeTransition fadeTransition;

    private Vector3 arrowTransformLocalPosition;
    private Quaternion arrowTransformLocalRotation;
    private Vector3 arrowPosition;
    private Quaternion arrowRotation;

    private VRFollowCamera arrowFollowCamera;

    private void Start()
    {
        fadeTransition = FindAnyObjectByType<FadeTransition>();

        arrowTransformLocalPosition = arrowTransform.transform.localPosition;
        arrowTransformLocalRotation = arrowTransform.transform.localRotation;
        arrowPosition = arrow.transform.localPosition;
        arrowRotation = arrow.transform.localRotation;

        arrowFollowCamera = arrow.GetComponent<VRFollowCamera>();
    }
    void Update()
    {
        if (_currentTeleportIndex == 0)
            return;

        UpdateArrowMovement();
    }

    public void LoadModel(string id)
    {
        _teleportPoints = EnvironmentView3DService.GetTeleportPoints(id);
        _currentTeleportIndex = 0;

        if (_teleportPoints != null && _teleportPoints.Length > 0)
        {
            arrow.gameObject.SetActive(true);
            ApplyArrowState();
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

        ApplyArrowState();
        TeleportToIndex(_currentTeleportIndex);

        if (fadeTransition != null)
            StartCoroutine(fadeTransition.Fade(1f, 0f));
    }

    void ApplyArrowState()
    {
        if (arrowTransform == null)
            return;

        if (_currentTeleportIndex == 0)
        {
            arrowTransform.transform.localPosition = new Vector3(
                arrowTransform.transform.localPosition.x,
                arrowTransform.transform.localPosition.y,
                -0.8f
            );

            arrowTransform.transform.localRotation = Quaternion.Euler(
                arrowTransform.transform.localRotation.x,
                -180f,
                arrowTransform.transform.localRotation.z
            );

            if (arrowFollowCamera != null)
                arrowFollowCamera.enabled = false;

            arrow.transform.localPosition = arrowPosition;
            arrow.transform.localRotation = arrowRotation;
        }
        else
        {
            arrowTransform.transform.localPosition = arrowTransformLocalPosition;
            arrowTransform.transform.localRotation = arrowTransformLocalRotation;
            arrow.transform.localPosition = arrowPosition;
            arrow.transform.localRotation = arrowRotation;

            if (arrowFollowCamera != null)
                arrowFollowCamera.enabled = true;
        }
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
