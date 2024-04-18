using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SunMagic_HotSun : Magic
{
    
    public override MagicType MagicType => m_MagicType;
    public override float MagicDuration => m_MagicDuration;
    public override Sprite MagicImage => m_MagicImage;

    public override float CoolDown => m_CoolDown;

    [SerializeField] private float m_CoolDown;
    [SerializeField] private Sprite m_MagicImage;
    [SerializeField] private MagicType m_MagicType;
    [SerializeField] private float m_MagicDuration;
    [SerializeField] private ParticleSystem m_SunEffect;
    [SerializeField] private Transform m_PlayerTransform;
    [SerializeField] private float m_WaterEvaporationSpeed;
    [SerializeField] private Collider m_Collider;

    private List<Water> waterList = new List<Water>();

    protected  void OnTriggerEnter(Collider other)
    {
        Water water = other.GetComponent<Water>();
        if (water != null)
        {
            waterList.Add(water);
        }
    }

    protected  void OnTriggerExit(Collider other)
    {
        Water water = other.GetComponent<Water>();
        if (water == null) return;

        if (waterList.Contains(water))
        {
            waterList.Remove(water);
        }
    }

    protected void Update()
    {
        for (int i = 0; i < waterList.Count; i++)
        {            
            waterList[i].RemoveWater(m_WaterEvaporationSpeed);
        }
    }
    public override void MagicReset()
    {
        m_SunEffect.Stop();
        waterList.Clear();
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
