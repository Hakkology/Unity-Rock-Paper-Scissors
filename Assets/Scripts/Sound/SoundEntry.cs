using UnityEngine;

public enum SoundID
{
    CorrectType,
    FalseType,
    LaserShoot,
    ImpactAstro,
    ExplosionAstro,
    LockTarget,
    ImpactShip,
    ExplosionShip,   
    HPLost,
    ButtonClick,
    GameOver,
    ScoreUp,
}

[System.Serializable]
public class SoundEntry
{
    public SoundID soundID;
    public AudioClip clip;
}