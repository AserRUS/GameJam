using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WindMagic_Airflow : Magic
{
    public override MagicType MagicType => m_MagicType;

    [SerializeField] private MagicType m_MagicType;
    [SerializeField] private float m_AirflowForce;
    [SerializeField] private ParticleSystem m_WindEffect;

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
        m_WindEffect.Play();
        enabled = true;
    }
}
