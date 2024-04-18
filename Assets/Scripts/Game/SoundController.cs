using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_Clip;
    [SerializeField] private float m_Volume;
    [SerializeField] private float m_VolumeChangeSpeed;
    [SerializeField] private float m_MinPitch;
    [SerializeField] private float m_MaxPitch;
    private float targetVolume;
    private void Update()
    {
        m_AudioSource.volume = Mathf.MoveTowards(m_AudioSource.volume, targetVolume, m_VolumeChangeSpeed * Time.deltaTime);

        if (m_AudioSource.volume == 0)
        {
            m_AudioSource.Stop();
        }
    }
    public void Play()
    {
        m_AudioSource.pitch = Random.Range(m_MinPitch, m_MaxPitch);
        m_AudioSource.clip = m_Clip;
        m_AudioSource.Play();
        targetVolume = m_Volume;
    }
    public void Stop()
    {
        targetVolume = 0;
    }
}
