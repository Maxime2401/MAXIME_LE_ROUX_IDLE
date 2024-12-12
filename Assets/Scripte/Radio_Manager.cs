using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Button onOffButton;
    public Button[] stationButtons;
    public AudioSource audioSource;
    public AudioClip[] stationAudioClips;
    public Scrollbar volumeScrollbar;

    private bool isOn = false;

    void Start()
    {
        onOffButton.onClick.AddListener(OnOnOffButtonClicked);
        volumeScrollbar.onValueChanged.AddListener(OnVolumeChanged);

        foreach (Button button in stationButtons)
        {
            button.onClick.AddListener(() => OnStationButtonClicked(button));
        }

        UpdateUI();
    }

    void OnOnOffButtonClicked()
    {
        isOn = !isOn;
        UpdateUI();
    }

    void OnStationButtonClicked(Button clickedButton)
    {
        int index = System.Array.IndexOf(stationButtons, clickedButton);
        if (index >= 0 && index < stationAudioClips.Length)
        {
            audioSource.clip = stationAudioClips[index];
            audioSource.loop = true; // Activer la lecture en boucle
            audioSource.Play();
        }
    }

    void OnVolumeChanged(float value)
    {
        audioSource.volume = value;
    }

    void UpdateUI()
    {
        foreach (Button button in stationButtons)
        {
            button.interactable = isOn;
        }

        volumeScrollbar.interactable = isOn;

        if (!isOn)
        {
            audioSource.Stop(); // Arrêter la musique si le bouton On/Off est désactivé
        }
    }
}
