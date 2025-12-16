using UnityEngine;

public class VRUIFollowCamera : MonoBehaviour
{
    private Transform _cam;

    [Header("Follow Settings")]
    public float distance;
    public bool followHeight = true;   
    public bool rotateOnlyY = true;

    void Start()
    {
        _cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        Vector3 forward = _cam.forward;
        if (!followHeight)
        {
            forward.y = 0f;
            forward.Normalize();
        }

        Vector3 targetPos = _cam.position + forward * distance;

        if (!followHeight)
            targetPos.y = transform.position.y;

        transform.position = targetPos;

        Vector3 lookDir = transform.position - _cam.position;

        if (rotateOnlyY)
            lookDir.y = 0f;

        if (lookDir.sqrMagnitude > 0.001f)
            transform.rotation = Quaternion.LookRotation(lookDir);
    }
}
