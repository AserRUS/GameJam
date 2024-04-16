using UnityEngine;
using UnityEngine.Events;

public class Destructible : MonoBehaviour
{
    public event UnityAction DeathEvent;
    public event UnityAction<int> HitPointChangeEvent;
    public int MaxHitPoints => m_MaxHitPoints;


    [SerializeField] private int m_MaxHitPoints;
    [SerializeField] private GameObject m_DeathEffect;

    private int currentHitPoints;
    private bool isInvulnerable;
    protected virtual void Start()
    {
        currentHitPoints = m_MaxHitPoints;
        HitPointChangeEvent?.Invoke(currentHitPoints);

        isInvulnerable = false;
    }

    public void SetInvulnerable(bool value)
    {
        isInvulnerable = value;
    }

    public virtual void RemoveHitpoints(int value)
    {

        if (isInvulnerable) return;

        currentHitPoints -= value;

        if (currentHitPoints < 0)
        {
            currentHitPoints = 0;
        }

        HitPointChangeEvent?.Invoke(currentHitPoints);

        if (currentHitPoints == 0)
        {  
            Death();
        }
    }
    public void AddHitpoints(int value)
    {
        currentHitPoints += value;

        if (currentHitPoints > m_MaxHitPoints)
        {
            currentHitPoints = m_MaxHitPoints;            
        }
        HitPointChangeEvent?.Invoke(currentHitPoints);
    }
    
    protected virtual void Death()
    {
        DeathEvent?.Invoke();
        if (m_DeathEffect != null)
            Instantiate (m_DeathEffect, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
