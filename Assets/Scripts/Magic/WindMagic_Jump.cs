using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class WindMagic_Jump : Magic
{
    public override MagicType MagicType => m_MagicType;
    public override float MagicDuration => m_MagicDuration;
    public override Sprite MagicImage => m_MagicImage;

    public override float CoolDown => m_CoolDown;

    [SerializeField] private float m_CoolDown = 2f;
    [SerializeField] private Sprite m_MagicImage;
    [SerializeField] private MagicType m_MagicType;
    [SerializeField] private float m_MagicDuration;
    [SerializeField] private Transform m_PlayerTransform;
    [SerializeField] private float m_JumpForce;
    [SerializeField] private ParticleSystem m_WindEffect;
    [SerializeField] private LayerMask m_LayerMask;


    private List<Rigidbody> objects = new List<Rigidbody>();
    private RaycastHit hit;
    private void Update()
    {
        if ( Physics.Raycast(m_PlayerTransform.position, -transform.up, out hit, 100, m_LayerMask)) 
        {
            transform.position = hit.point;
        }     
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].AddForce(Vector3.up * m_JumpForce, ForceMode.Acceleration);
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

    public override void UseMagic()
    {
        m_WindEffect.Play();
        StartCoroutine(MagicTimer());
        enabled = true;
    }
    public override void MagicReset()
    {
        m_WindEffect.Stop();
        enabled = false;
    }

    private IEnumerator MagicTimer()
    {
        yield return new WaitForSeconds(MagicDuration);
        MagicReset();
    }
}
