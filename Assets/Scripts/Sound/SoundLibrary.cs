using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SoundLibrary", menuName = "Audio/Sound Library", order = 0)]
public class SoundLibrary : ScriptableObject
{
    public List<SoundEntry> sounds;

    private Dictionary<SoundID, AudioClip> _soundMap;

    public void Initialize()
    {
        if (_soundMap != null) return;

        _soundMap = new Dictionary<SoundID, AudioClip>();
        foreach (var entry in sounds)
        {
            if (!_soundMap.ContainsKey(entry.soundID))
            {
                _soundMap.Add(entry.soundID, entry.clip);
            }
        }
    }

    public AudioClip GetClip(SoundID id)
    {
        Initialize();
        return _soundMap.TryGetValue(id, out var clip) ? clip : null;
    }
}
