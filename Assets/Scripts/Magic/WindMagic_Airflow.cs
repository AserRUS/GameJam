using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class WindMagic_Airflow : Magic
{
    public override MagicType MagicType => m_MagicType;
    public override float MagicDuration => m_MagicDuration;
    public override Sprite MagicImage => m_MagicImage;
    public override float CoolDown => m_CoolDown;

    [SerializeField] private float m_CoolDown = 2f;
    [SerializeField] private Sprite m_MagicImage;
    [SerializeField] private MagicType m_MagicType;
    [SerializeField] private float m_MagicDuration;
    [SerializeField] private float m_AirflowForce;
    [SerializeField] private ParticleSystem m_WindEffect;
    [SerializeField] private Transform m_PlayerTransform;
    [SerializeField] private Collider m_Collider;

    private List<Rigidbody> objects = new List<Rigidbody>();

    private void FixedUpdate()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].AddForce(transform.right * m_AirflowForce);
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == m_PlayerTransform) return;
        Rigidbody rigidbody = other.GetComponent<Rigidbody>();
        if (rigidbody == null) return;
        objects.Add(rigidbody);

    }
    private void OnTriggerExit(Collider other)
    {
        Rigidbody rigidbody = other.GetComponent<Rigidbody>();
        if (rigidbody == null) return;

        if (objects.Contains(rigidbody))
        {
            objects.Remove(rigidbody);
        }
    }
    public override void MagicReset()
    {
        m_WindEffect.Stop();
        objects.Clear();
        m_Collider.enabled = false;
    }

    public override void UseMagic()
    {
        transform.position = m_PlayerTransform.position;
        transform.rotation = m_PlayerTransform.rotation;
        m_Collider.enabled = true;
        m_WindEffect.Play();
        StartCoroutine(MagicTimer());
    }

    private IEnumerator MagicTimer()
    {
        yield return new WaitForSeconds(MagicDuration);
        MagicReset();
    }
}
