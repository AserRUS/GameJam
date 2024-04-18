using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class RainMagic_Rain : Magic
{
    public override MagicType MagicType => m_MagicType;
    public override float MagicDuration => m_MagicDuration;
    public override Sprite MagicImage => m_MagicImage;

    public override float CoolDown => m_CoolDown;

    [SerializeField] private float m_CoolDown = 2f;
    [SerializeField] private Sprite m_MagicImage;
    [SerializeField] private MagicType m_MagicType;
    [SerializeField] private float m_MagicDuration;
    [SerializeField] private ParticleSystem m_RainEffect;
    [SerializeField] private Transform m_PlayerTransform;
    [SerializeField] private float m_FireReducingSpeed;
    [SerializeField] private Collider m_Collider;
    [SerializeField] private SoundController m_SoundController;

    private List<Fire> fireList = new List<Fire>();

    
    protected virtual void OnTriggerEnter(Collider other)
    {
        Fire fire = other.GetComponent<Fire>();
        if (fire != null)
        {
            fireList.Add(fire);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        Fire fire = other.GetComponent<Fire>();
        if (fire == null) return;

        if (fireList.Contains(fire))
        {
            fireList.Remove(fire);
        }
    }

    protected virtual void Update()
    {
        for (int i = 0; i < fireList.Count; i++)
        {
            fireList[i].ReducingFire(m_FireReducingSpeed);
        }
    }
    public override void MagicReset()
    {
        m_SoundController.Stop();
        m_RainEffect.Stop();
        fireList.Clear();
        m_Collider.enabled = false;
    }

    public override void UseMagic()
    {
        m_SoundController.Play();
        transform.position = new Vector3(m_PlayerTransform.position.x, transform.position.y, m_PlayerTransform.position.z);
        m_Collider.enabled = true;
        m_RainEffect.Play();
        StartCoroutine(MagicTimer());     
    }
    private IEnumerator MagicTimer()
    {
        yield return new WaitForSeconds(MagicDuration);
        MagicReset();
    }
}
