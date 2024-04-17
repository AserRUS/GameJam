using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public event UnityAction OnJumpEvent;
    public event UnityAction OnRotationEvent;

    public bool IsRotation => isRotation;
    public Rigidbody Rigidbody => m_Rigidbody;
    public float MaxSpeed => m_MaxSpeed;
    public float DistanceToGround => m_DistanceToGround;
    public bool IsGround => isGround;

    [SerializeField] private Rigidbody m_Rigidbody;

    [Header("Movement")]
    [SerializeField] private float m_RotationSpeed;
    [SerializeField] private float m_GroundSpeed;
    [SerializeField] private float m_AirSpeed;
    [SerializeField] private float m_MaxSpeed;
    [SerializeField] private float m_RayDistance;
    [SerializeField] private Vector3 m_RayOffset;
    [SerializeField] private LayerMask m_LayerMask;

    [Header("Jump")]
    [SerializeField] private float m_JumpForce;
    [SerializeField] private float m_StunTime;

    private float m_DistanceToGround;  
    private int direction = 1;
    private bool isGround;
    private bool isWater;
    private bool isMove;
    private bool isRotation;
    private bool isStun;
    private Vector3 targetRotation;
    private RaycastHit hit;


    private void Update()
    {
        CheckGround();
    }

    private void FixedUpdate()
    {
        Movement();
        Resistance();
        Rotation();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<Water>() != null)
        {
            isWater = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.GetComponent<Water>() != null)
        {
            isWater = false;
        }
    }
    private void CheckGround()
    {  
        m_DistanceToGround = 100;
        float dist;
        if (Physics.Raycast(transform.position + new Vector3(m_RayOffset.x, m_RayOffset.y, m_RayOffset.z), -transform.up, out hit, 100, m_LayerMask) == true)
        {
            dist = Vector3.Distance(transform.position + new Vector3(m_RayOffset.x, m_RayOffset.y, m_RayOffset.z), hit.point);
            if (dist < m_DistanceToGround)
                m_DistanceToGround = dist;
        }
        if (Physics.Raycast(transform.position + new Vector3(-m_RayOffset.x, m_RayOffset.y, m_RayOffset.z), -transform.up, out hit, 100, m_LayerMask) == true)
        {
            dist = Vector3.Distance(transform.position + new Vector3(-m_RayOffset.x, m_RayOffset.y, m_RayOffset.z), hit.point);
            if (dist < m_DistanceToGround)
                m_DistanceToGround = dist;
        }

        if (m_DistanceToGround <= m_RayDistance)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }


        Debug.DrawRay(transform.position + new Vector3(m_RayOffset.x, m_RayOffset.y, m_RayOffset.z), -Vector2.up * m_RayDistance, Color.yellow);
        Debug.DrawRay(transform.position + new Vector3(-m_RayOffset.x, m_RayOffset.y, m_RayOffset.z), -Vector2.up * m_RayDistance, Color.yellow);
    }

    


    private void Movement()
    {
        if (isMove == false) return;
        
        if (isGround)
            m_Rigidbody.AddForce(direction * Vector3.right * m_GroundSpeed);
        else if (isGround == false && Mathf.Abs(m_Rigidbody.velocity.x) < m_MaxSpeed)
            m_Rigidbody.AddForce(direction * Vector3.right * m_AirSpeed);
    }

    private void Rotation()
    {
        if (isRotation == false) return;

        Vector3 rotation = transform.rotation.eulerAngles;
        rotation = Vector3.MoveTowards(rotation, targetRotation, m_RotationSpeed * Time.fixedDeltaTime);
        m_Rigidbody.MoveRotation(Quaternion.Euler(rotation));

        if (transform.rotation.eulerAngles == targetRotation)
        {
            isRotation = false;
        }
    }

    public void Jump()
    {
        if (isStun == true) return;
        if (isGround || isWater)
        {
            m_Rigidbody.AddForce(transform.up * m_JumpForce, ForceMode.Impulse);
            OnJumpEvent?.Invoke();
        }                 
        
    }

    private void Resistance()
    {
        if (isGround && m_Rigidbody.velocity.x * direction >= m_MaxSpeed && isGround)
        {
            m_Rigidbody.velocity = new Vector3(direction * m_MaxSpeed, m_Rigidbody.velocity.y, m_Rigidbody.velocity.z);
        }  
    }

    public void RotateLeft()
    {
        if (direction == -1) return;
        targetRotation = new Vector3(0, 180, 0);
        isRotation = true;
        direction = -1;

        OnRotationEvent?.Invoke();
    }
    public void RotateRight()
    {
        if (direction == 1) return;
        targetRotation = new Vector3(0, 0, 0);
        isRotation = true;
        direction = 1;

        OnRotationEvent?.Invoke();
    }

    public void Move(bool isMove)
    {
        this.isMove = isMove;
    }
    public void Stun()
    {     
        if (gameObject.activeSelf == true)
            StartCoroutine(StunTimer());
    }
    private IEnumerator StunTimer()
    {
        isStun = true;
        yield return new WaitForSeconds(m_StunTime);
        isStun = false;
    }
}
