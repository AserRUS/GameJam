using UnityEngine;

public class PlayerInputControl : MonoBehaviour
{
    [SerializeField] private PlayerMovement m_Movement;
    [SerializeField] private MagicController m_MagicController;

    private void Update()
    {
        if (m_Movement != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Movement.Jump();
            }            
        }

        if (m_MagicController != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                m_MagicController.UseLeftButtonMagic();
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {                
                m_MagicController.UseRightButtonMagic();
            }
        }

    }
    private void FixedUpdate()
    { 
        if (m_Movement != null)
        {
            if (Input.GetKey(KeyCode.A))
            {
                m_Movement.RotateLeft();
                m_Movement.Move(true);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                m_Movement.RotateRight();
                m_Movement.Move(true);
            }
            else
            {
                m_Movement.Move(false);
            }
        }
            
    }


}
