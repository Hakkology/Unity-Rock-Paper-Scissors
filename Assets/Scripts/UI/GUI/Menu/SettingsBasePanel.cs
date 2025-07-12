using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsBasePanel : BasePanel
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Dropdown languageDropdown;
    // [SerializeField] private Toggle vibrationToggle;
    [SerializeField] private Toggle muteToggle;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider soundVolumeSlider;

    void Start()
    {
        SetupUI();
    }

    protected virtual void SetupUI()
    {
        // Dropdown - Dil Seçimi
        languageDropdown.onValueChanged.AddListener(OnLanguageChanged);
        languageDropdown.value = (int)GameSettings.Instance.currentLanguage;

        // Titreşim
        // vibrationToggle.onValueChanged.AddListener(OnVibrationToggled);
        // vibrationToggle.isOn = GameSettings.Instance.vibrationEnabled;

        // Mute
        muteToggle.onValueChanged.AddListener(OnMuteToggled);
        muteToggle.isOn = GameSettings.Instance.isMuted;

        // Volume Sliders
        masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        masterVolumeSlider.value = GameSettings.Instance.masterVolume;

        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        musicVolumeSlider.value = GameSettings.Instance.musicVolume;

        soundVolumeSlider.onValueChanged.AddListener(OnSoundVolumeChanged);
        soundVolumeSlider.value = GameSettings.Instance.soundVolume;
    }

    private void OnLanguageChanged(int index)
    {
        GameSettings.Instance.SetLanguage((Language)index);
    }

    // private void OnVibrationToggled(bool isOn)
    // {
    //     GameSettings.Instance.SetVibration(isOn);
    // }

    private void OnMuteToggled(bool isMuted)
    {
        GameSettings.Instance.ToggleMute(isMuted);
    }

    private void OnMasterVolumeChanged(float value)
    {
        GameSettings.Instance.SetMasterVolume(value);
    }

    private void OnMusicVolumeChanged(float value)
    {
        GameSettings.Instance.SetMusicVolume(value);
    }

    private void OnSoundVolumeChanged(float value)
    {
        GameSettings.Instance.SetSoundVolume(value);
    }
}
