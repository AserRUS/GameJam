using System.Collections.Generic;
using UnityEngine;

public class WindMagic_Jump : Magic
{
    public override MagicType MagicType => m_MagicType;

    [SerializeField] private MagicType m_MagicType;

    [SerializeField] private Rigidbody m_TargetRigidbody;
    [SerializeField] private float m_JumpHeight;
    [SerializeField] private float m_JumpForce;
    [SerializeField] private ParticleSystem m_WindEffect;
    [SerializeField] private LayerMask m_LayerMask;

    private float distanceToGround;
    private RaycastHit hit;
    private void Update()
    {
        if (Physics.Raycast(transform.position, -transform.up, out hit, 100, m_LayerMask) == true)
        {
            distanceToGround = Vector3.Distance(transform.position, hit.point);
            m_WindEffect.transform.position = hit.point;
        }
        else
        {
            distanceToGround = 100;
        }
    }

    private void FixedUpdate()
    {
        m_TargetRigidbody.AddForce(Vector3.up * m_JumpForce);// * (Mathf.Abs(m_TargetRigidbody.DistanceToGround - m_JumpHeight) / m_JumpHeight));
        m_TargetRigidbody.AddForce((-Vector3.up * m_JumpForce) * (distanceToGround / m_JumpHeight));
    }
    public override void UseMagic()
    {
        m_WindEffect.Play();
        m_TargetRigidbody.drag = 1;
        enabled = true;
    }
    public override void MagicReset()
    {
        m_WindEffect.Stop();
        m_TargetRigidbody .drag = 0;
        enabled = false;
    }
}
