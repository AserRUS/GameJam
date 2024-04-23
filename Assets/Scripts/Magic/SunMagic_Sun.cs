using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SunMagic_Sun : Magic
{
    public override MagicType MagicType => m_MagicType;
    public override float MagicDuration => m_MagicDuration;
    public override Sprite MagicImage => m_MagicImage;

    public override float CoolDown => m_CoolDown;

    [SerializeField] private float m_CoolDown = 2f;
    [SerializeField] private Sprite m_MagicImage;
    [SerializeField] private MagicType m_MagicType;
    [SerializeField] private float m_MagicDuration;
    [SerializeField] private ParticleSystem m_SunEffect;
    [SerializeField] private Transform m_PlayerTransform;
    [SerializeField] private Collider m_Collider;

    private List<Rope> ropeList = new List<Rope>();

    protected virtual void OnTriggerEnter(Collider other)
    {
        Rope rope = other.transform.parent?.GetComponent<Rope>();        
        if (rope != null)
        {
            rope.LightTheRope();
            ropeList.Add(rope);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        Rope rope = other.transform.parent?.GetComponent<Rope>();
        if (rope == null) return;

        if (ropeList.Contains(rope))
        {
            ropeList.Remove(rope);
        }
    }

   
    public override void MagicReset()
    {
        m_SunEffect.Stop();
        for (int i = 0; i < ropeList.Count; i++)
        {
            ropeList[i].RopeDestroy();
        }
        ropeList.Clear();
        m_Collider.enabled = false;
    }

    public override void UseMagic()
    {
        transform.position = new Vector3(m_PlayerTransform.position.x, transform.position.y, m_PlayerTransform.position.z);
        m_Collider.enabled = true;
        m_SunEffect.Play();
        StartCoroutine(MagicTimer());
    }
    private IEnumerator MagicTimer()
    {
        yield return new WaitForSeconds(MagicDuration);
        MagicReset();
    }
}
