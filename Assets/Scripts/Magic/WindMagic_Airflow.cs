using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WindMagic_Airflow : Magic
{
    public override MagicType MagicType => m_MagicType;
    public override float MagicDuration => m_MagicDuration;

    [SerializeField] private MagicType m_MagicType;

    [SerializeField] private float m_MagicDuration;
    [SerializeField] private float m_AirflowForce;
    [SerializeField] private ParticleSystem m_WindEffect;
    [SerializeField] private Transform m_PlayerTransform;

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
        enabled = false;
    }

    public override void UseMagic()
    {
        transform.position = m_PlayerTransform.position;
        transform.rotation = m_PlayerTransform.rotation;
        m_WindEffect.Play();
        enabled = true;
    }
}
