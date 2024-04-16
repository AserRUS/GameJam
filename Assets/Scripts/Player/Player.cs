using UnityEngine;

public class Player : Destructible
{
    [SerializeField] private PlayerInputControl m_PlayerInputControl;
    protected override void Death()
    {
        m_PlayerInputControl.enabled = false;
        base.Death();

    }

}
