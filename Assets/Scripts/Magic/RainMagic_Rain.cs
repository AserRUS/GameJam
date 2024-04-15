using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class RainMagic_Rain : Magic
{
    public override MagicType MagicType => m_MagicType;
    public override float MagicDuration => m_MagicDuration;

    [SerializeField] private MagicType m_MagicType;
    [SerializeField] private float m_MagicDuration;
    [SerializeField] private ParticleSystem m_RainEffect;
    [SerializeField] private Transform m_PlayerTransform;


    protected virtual void OnTriggerEnter(Collider other)
    {
        
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        
    }

    protected virtual void Update()
    {
        
    }
    public override void MagicReset()
    {
        m_RainEffect.Stop();
        enabled = false;
    }

    public override void UseMagic()
    {
        transform.position = new Vector3(m_PlayerTransform.position.x, transform.position.y, m_PlayerTransform.position.z);
        m_RainEffect.Play();
        enabled = true;
    }
}
