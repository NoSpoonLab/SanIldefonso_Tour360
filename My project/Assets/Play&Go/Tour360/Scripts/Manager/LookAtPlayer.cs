using Unity.VisualScripting;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    void Update()
    {
        Vector3 camPos = Camera.main.transform.position;
        Vector3 lookPos = new Vector3(camPos.x, transform.position.y, camPos.z);

        transform.LookAt(lookPos);
        transform.Rotate(0, 180f, 0);
    }
}
