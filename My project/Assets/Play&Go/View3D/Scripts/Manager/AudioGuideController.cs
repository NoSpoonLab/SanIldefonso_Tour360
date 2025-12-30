using UnityEngine;

public class AudioGuideController : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;
    private bool _isAudioPlaying = false;
    public GameObject textPlay;
    public GameObject textPause;
    private string _currentID;

    public void LoadAudio(string modelId)
    {
        if (audioSource == null)
            return;

        _currentID = modelId;

        audioSource.Stop();
        audioSource.clip = null;
        _isAudioPlaying = false;

        string languageCode = LanguageManager.Instance.currentLanguage;
        string path = "Audio/" + languageCode + "/" + modelId;

        AudioClip clip = Resources.Load<AudioClip>(path);

        if (clip == null)
        {
            Debug.LogWarning("No se encontró audio en el idioma " + languageCode + " para el modelo: " + modelId);
            return;
        }

        audioSource.clip = clip;
    }

    public void Clear()
    {
        audioSource.Stop();
        audioSource.clip = null;
        _isAudioPlaying = false;
        textPlay.SetActive(true);
        textPause.SetActive(false);
    }

    public void PressButtonPlayPause() //Add in inspector ButtonPlayPause in Interactable Unity Events
    {
        if (audioSource == null || audioSource.clip == null)
            return;

        if (_isAudioPlaying)
        {
            audioSource.Pause();
            _isAudioPlaying = false;
            textPlay.SetActive(true);
            textPause.SetActive(false);
        }
        else
        {
            audioSource.Play();
            _isAudioPlaying = true;
            textPlay.SetActive(false);
            textPause.SetActive(true);
        }
    }

    public void ResetAudioInChangeLanguage() //Add in inspector Español and Ingles in Interactable Unity Events
    {
        audioSource.Stop();
        _isAudioPlaying = false;
        LoadAudio(_currentID);
        textPlay.SetActive(true);
        textPause.SetActive(false);
    }
}
