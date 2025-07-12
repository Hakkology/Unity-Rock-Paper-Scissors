using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    [SerializeField] private SoundLibrary soundLibrary;
    [SerializeField] private AudioMixerGroup mixerGroup; 

    private AudioSource _audioSource;

    public event Action<SoundID> OnSoundRequested;

    void Awake()
    {
        // Single AudioSource for all SFX
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.outputAudioMixerGroup = mixerGroup;

        soundLibrary.Initialize();
        foreach (var entry in soundLibrary.sounds)
            entry.clip.LoadAudioData(); 
        OnSoundRequested += HandleSoundRequest;
    }

    private void HandleSoundRequest(SoundID id)
    {
        var clip = soundLibrary.GetClip(id);
        if (clip == null)
        {
            Debug.LogWarning($"SoundController: Clip not found for {id}");
            return;
        }

        _audioSource.PlayOneShot(clip);
    }

    void OnDestroy()
    {
        OnSoundRequested -= HandleSoundRequest;
    }

    public void RequestSound(SoundID id) => OnSoundRequested?.Invoke(id);
}
