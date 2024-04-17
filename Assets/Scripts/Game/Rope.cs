using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private ConfigurableJoint m_Joint;
    [SerializeField] private ParticleSystem m_Fire;

    public void LightTheRope()
    {
        m_Fire.Play();
    }
    public void RopeDestroy()
    {
        if (m_Joint == null) return;
        Destroy(m_Joint);
    }
}
