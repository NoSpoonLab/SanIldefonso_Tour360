using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.Input;

public class HideHandVisual : MonoBehaviour
{
    [Header("Hand Visuals")]
    public GameObject leftHandVisual;
    public GameObject rightHandVisual;

    [Header("Hand Ref (from Palm Menu)")]
    public HandRef dominantHandRef;

    public void OnPalmMenuActivated()
    {
        if (dominantHandRef.Handedness == Handedness.Left)
        {
            leftHandVisual.SetActive(false);
        }
        else if (dominantHandRef.Handedness == Handedness.Right)
        {
            rightHandVisual.SetActive(false);
        }
    }

    public void OnPalmMenuDeactivated()
    {
        if (dominantHandRef.Handedness == Handedness.Left)
        {
            leftHandVisual.SetActive(true);
        }
        else if (dominantHandRef.Handedness == Handedness.Right)
        {
            rightHandVisual.SetActive(true);
        }
    }
}
