using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Fire : MonoBehaviour
{
    [SerializeField] private int m_Damage;
    [SerializeField] private float impactForce;
    [SerializeField][Range(0, 1)] private float m_FireLevel;
    [SerializeField] private float m_MaxRateOverTime;
    [SerializeField] private float m_MaxLifeTime;
    [SerializeField] private ParticleSystem m_FireParticleSystem;
    [SerializeField] private ParticleSystem m_SmokeParticleSystem;
    [SerializeField] private ParticleSystem m_SteamParticleSystem;
    [SerializeField] private Collider m_Collider;
    [SerializeField] private float m_FireCoolDown;
    [SerializeField] private SoundController m_SoundController;

    private float fireCoolDownTimer;
    private void Start()
    {
        m_SoundController.Play();
    }
    private void Update()
    {
        var startLifetime = m_FireParticleSystem.main;
        startLifetime.startLifetime = Mathf.MoveTowards(0, m_MaxLifeTime, m_FireLevel * (m_MaxLifeTime - 0));
        var rateOverTime = m_FireParticleSystem.emission;
        rateOverTime.rateOverTime = Mathf.MoveTowards(0, m_MaxRateOverTime, m_FireLevel * (m_MaxRateOverTime - 0));
        m_Collider.enabled = !(m_FireLevel <= 0.1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Damage(collision);
        fireCoolDownTimer = m_FireCoolDown;
    }
    private void OnCollisionStay(Collision collision)
    {
        if (fireCoolDownTimer > 0)
        {
            fireCoolDownTimer -= Time.deltaTime;
        }
        else
        {
            fireCoolDownTimer = m_FireCoolDown;
            Damage(collision);
        }
    }

    private void Damage(Collision collision)
    {
        Player player = collision.transform.GetComponent<Player>();
        if (player == null) return;

        player.RemoveHitpoints(m_Damage);

        PlayerMovement playerMovement = collision.transform.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.Stun();
        }

        Rigidbody rb = collision.transform.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            Vector3 direction = (rb.transform.position - transform.position).normalized;
            rb.AddForce(direction * impactForce, ForceMode.Impulse);
        }
    }
    
    public void StartReducingFire()
    {
        m_SoundController.Stop();
        m_SmokeParticleSystem.Stop();
        m_SteamParticleSystem.Play();
    }

    public void ReducingFire(float fireReducingSpeed)
    {
        StartReducingFire();
        m_FireLevel = Mathf.MoveTowards(m_FireLevel, 0, fireReducingSpeed * Time.deltaTime);
    }
}
