using UnityEngine;
using UnityEngine.Audio;

public enum Language { English, Turkish }
public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance { get; private set; }

    [Header("Audio")]
    public AudioMixer mainMixer;
    public string masterVolumeParam = "MasterVolume";
    public string musicVolumeParam = "MusicVolume";
    public string soundVolumeParam = "SoundVolume";

    [Range(0f, 1f)] public float masterVolume = 1f;
    [Range(0f, 1f)] public float musicVolume = 1f;
    [Range(0f, 1f)] public float soundVolume = 1f;
    public bool isMuted = false;

    [Header("Vibration")]
    public bool vibrationEnabled = true;
    public Language currentLanguage = Language.English;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        ApplyAudioSettings();
    }

    public void SetMasterVolume(float value)
    {
        masterVolume = value;
        SetMixerVolume(masterVolumeParam, value);
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = value;
        SetMixerVolume(musicVolumeParam, value);
    }

    public void SetSoundVolume(float value)
    {
        soundVolume = value;
        SetMixerVolume(soundVolumeParam, value);
    }

    public void ToggleMute(bool mute)
    {
        isMuted = mute;
        float volumeDb = mute ? -80f : Mathf.Lerp(-40f, 0f, masterVolume);
        mainMixer.SetFloat(masterVolumeParam, volumeDb);
    }

    public void SetVibration(bool enabled)
    {
        vibrationEnabled = enabled;
    }

    public void SetLanguage(Language lang)
    {
        currentLanguage = lang;
    }

    private void SetMixerVolume(string param, float value)
    {
        float volumeDb = Mathf.Lerp(-40f, 0f, value);
        mainMixer.SetFloat(param, volumeDb);
    }

    private void ApplyAudioSettings()
    {
        SetMasterVolume(masterVolume);
        SetMusicVolume(musicVolume);
        SetSoundVolume(soundVolume);
        ToggleMute(isMuted);
    }
}
