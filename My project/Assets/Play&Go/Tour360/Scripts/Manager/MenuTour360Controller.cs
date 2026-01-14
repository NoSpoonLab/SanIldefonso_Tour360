using UnityEngine;
using UnityEngine.Audio;

public class MenuTour360Controller : MonoBehaviour
{
    private bool _isMapActive = false;
    public GameObject mapContent;
    public GameObject descriptionContent;
    public GameObject mapText;
    public GameObject descriptionText;

    public void PressButtonMapDescription() //Add in inspector ButtonMap & ButtonDescription in Interactable Unity Events
    {
        if (_isMapActive)
        {
            mapContent.SetActive(false);
            descriptionContent.SetActive(true);
            mapText.SetActive(true);
            descriptionText.SetActive(false);
            _isMapActive = false;
        }
        else
        {
            mapContent.SetActive(true);
            descriptionContent.SetActive(false);
            mapText.SetActive(false);
            descriptionText.SetActive(true);
            _isMapActive = true;
        }
    }
}
