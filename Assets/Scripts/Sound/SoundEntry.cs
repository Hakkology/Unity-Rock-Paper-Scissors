using UnityEngine;

public enum SoundID
{
    RockConvert,
    PaperConvert,
    ScissorConvert,
    ButtonClick,
    GameStart,
    GameOverFail,
    GameOverSuccess,
}

[System.Serializable]
public class SoundEntry
{
    public SoundID soundID;
    public AudioClip clip;
}