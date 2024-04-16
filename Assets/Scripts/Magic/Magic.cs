using UnityEngine;

public enum MagicType
{
    Wind,
    Rain,
    Sun
}
public abstract class Magic : MonoBehaviour
{
    public abstract float CoolDown { get; }
    public abstract Sprite MagicImage { get;}
    public abstract MagicType MagicType { get;}
    public abstract float MagicDuration { get;}
    public abstract void UseMagic();
    public abstract void MagicReset();
}
