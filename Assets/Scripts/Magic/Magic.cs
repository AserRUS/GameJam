using UnityEngine;
public enum MagicType
{
    Wind,
    Rain,
    Sun
}
public abstract class Magic : MonoBehaviour
{
    public abstract MagicType MagicType { get;}
    public abstract void UseMagic();
    public abstract void MagicReset();
}
