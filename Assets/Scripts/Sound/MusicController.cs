using System;
using UnityEngine;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{
    [SerializeField] private MusicLibrary musicLibrary;
    [SerializeField] private AudioMixer audioMixer; 
    [SerializeField] private AudioMixerGroup musicMixerGroup; 

    private AudioSource _audioSource;

    public event Action OnStartMusicRequested;
    public event Action OnStopMusicRequested;

    void Awake()
    {
        var others = FindObjectsOfType<MusicController>();
        if (others.Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.outputAudioMixerGroup = musicMixerGroup;
        _audioSource.loop = true;

        OnStartMusicRequested += HandleStartMusic;
        OnStopMusicRequested += HandleStopMusic;

        OnStartMusicRequested.Invoke();
    }

    void OnDestroy()
    {
        OnStartMusicRequested -= HandleStartMusic;
        OnStopMusicRequested -= HandleStopMusic;
    }

    private void HandleStartMusic()
    {
        if (_audioSource.isPlaying) return;

        var clip = musicLibrary.GetRandomClip();
        if (clip == null)
        {
            Debug.LogWarning("MusicManager: No music clip available to play.");
            return;
        }

        _audioSource.clip = clip;
        _audioSource.volume = 1f; 
        _audioSource.Play();
    }

    private void HandleStopMusic()
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
    }

    private float DbToLinear(float db) => Mathf.Pow(10f, db / 20f);
    public void StartMusicRequest() => OnStartMusicRequested?.Invoke();
    public void StopMusicRequest() => OnStopMusicRequested?.Invoke();
}
