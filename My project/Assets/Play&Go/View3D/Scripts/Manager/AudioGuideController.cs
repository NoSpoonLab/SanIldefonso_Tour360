using UnityEngine;

public class AudioGuideController : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;
    public bool isAudioPlaying = false;
    public GameObject textPlay;
    public GameObject textPause;

    public string currentID;

    public void LoadAudio(string modelId)
    {
        if (audioSource == null)
            return;

        currentID = modelId;

        audioSource.Stop();
        audioSource.clip = null;
        isAudioPlaying = false;

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
        isAudioPlaying = false;
        textPlay.SetActive(true);
        textPause.SetActive(false);
    }

    public void PressButtonPlayPause() //Add in inspector ButtonPlayPause in Interactable Unity Events
    {
        if (audioSource == null || audioSource.clip == null)
            return;

        if (isAudioPlaying)
        {
            audioSource.Pause();
            isAudioPlaying = false;
            textPlay.SetActive(true);
            textPause.SetActive(false);
        }
        else
        {
            audioSource.Play();
            isAudioPlaying = true;
            textPlay.SetActive(false);
            textPause.SetActive(true);
        }
    }

    public void ResetAudioInChangeLanguage() //Add in inspector Español and Ingles in Interactable Unity Events
    {
        audioSource.Stop();
        isAudioPlaying = false;
        LoadAudio(currentID);
        textPlay.SetActive(true);
        textPause.SetActive(false);
    }
}
