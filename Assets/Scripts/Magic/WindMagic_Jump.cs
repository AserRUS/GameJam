using System.Collections;
using UnityEngine;
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
    [SerializeField] private Rigidbody m_Player;
    [SerializeField] private PlayerMovement m_PlayerMovement;
    [SerializeField] private float m_JumpForce;
    [SerializeField] private ParticleSystem m_WindEffect;
    [SerializeField] private LayerMask m_LayerMask;



    private void Update()
    {        
        transform.position = m_Player.transform.position;           
    }

    private void FixedUpdate()
    {        
        m_Player.AddForce(Vector3.up * m_JumpForce, ForceMode.Acceleration);        
    }

    

    public override void UseMagic()
    {
        m_Player.velocity = new Vector3(m_Player.velocity.x, 0, m_Player.velocity.z);
        m_WindEffect.Play();
        StartCoroutine(MagicTimer());
        m_PlayerMovement.Stun();
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
