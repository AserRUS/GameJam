using UnityEngine;

public class PlayerAnimationState : MonoBehaviour
{
    [SerializeField] private PlayerMovement m_PlayerMovement;
    [SerializeField] private Animator m_PlayerAnimator;
    
    

    private void Update()
    {
        m_PlayerAnimator.SetFloat("Velocity", Mathf.Abs(m_PlayerMovement.Velocity.x) /  m_PlayerMovement.MaxSpeed);
        m_PlayerAnimator.SetFloat("DistanceToGround", m_PlayerMovement.DistanceToGround);
        m_PlayerAnimator.SetBool("IsGround", m_PlayerMovement.IsGround);
    }

    public void MagicAnimation()
    {
        m_PlayerAnimator.SetTrigger("isMagic");
    }
}
