using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera m_Camera;
    [SerializeField] private Vector3 m_CameraBorderX;
    [SerializeField] private Vector3 m_CameraBorderY;
    [SerializeField] private Parallax[] m_Parallax;
    [SerializeField] private Vector2 m_SpeedFollow;
    [SerializeField] private Transform cameraFollowTarget;

    public void SetCameraFollowTarget(Transform target)
    {
        cameraFollowTarget = target;
    }


    private void LateUpdate()
    {        
        if (cameraFollowTarget == null) return;
        float height = (Mathf.Abs(transform.position.z) + m_CameraBorderY.z) * Mathf.Tan(((m_Camera.fieldOfView / 2) * Mathf.PI) / 180);
        float width = (Mathf.Abs(transform.position.z) + m_CameraBorderX.z) * Mathf.Tan(((Camera.VerticalToHorizontalFieldOfView(m_Camera.fieldOfView, m_Camera.aspect) / 2) * Mathf.PI) / 180);
        float camSpeedX = m_SpeedFollow.x * Time.deltaTime;
        float camSpeedY = m_SpeedFollow.y * Time.deltaTime;

        float camPosX = Mathf.Lerp(transform.position.x, cameraFollowTarget.position.x, camSpeedX);
        float camPosY = Mathf.Lerp(transform.position.y, cameraFollowTarget.position.y, camSpeedY); 

        Vector3 newCamPos = new Vector3(Mathf.Clamp(camPosX, m_CameraBorderX.x + width, m_CameraBorderX.y - width), Mathf.Clamp(camPosY, m_CameraBorderY.x + height, m_CameraBorderY.y - height), transform.position.z);

        transform.position = newCamPos;

        if (m_Parallax != null)
        {
            for (int i = 0; i < m_Parallax.Length; i++)
            {
                m_Parallax[i].ParallaxUpdate();
            }
        }
            
    }    
    

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {          
        
        Gizmos.DrawLine(new Vector3(m_CameraBorderX.x, -10, m_CameraBorderX.z), new Vector3(m_CameraBorderX.x, 10, m_CameraBorderX.z));

        Gizmos.DrawLine(new Vector3(m_CameraBorderX.y, -10, m_CameraBorderX.z), new Vector3(m_CameraBorderX.y, 10, m_CameraBorderX.z));

        Gizmos.DrawLine(new Vector3(-10, m_CameraBorderY.y, m_CameraBorderY.z), new Vector3(10, m_CameraBorderY.y, m_CameraBorderY.z));

        Gizmos.DrawLine(new Vector3(-10, m_CameraBorderY.x, m_CameraBorderY.z), new Vector3(10, m_CameraBorderY.x, m_CameraBorderY.z));
    }

#endif
}
