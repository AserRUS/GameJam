using UnityEngine;
using UnityEngine.UI;

public class UIState : MonoBehaviour
{
    [SerializeField] private Slider m_HealthSlider;
    [SerializeField] private Destructible m_Destructible;
    private void Start()
    {
        if (m_Destructible == null) return;
        m_Destructible.HitPointChangeEvent += ValueChange;
        m_HealthSlider.maxValue = m_Destructible.MaxHitPoints;
        m_HealthSlider.value = m_HealthSlider.maxValue;
    }
    private void OnDestroy()
    {
        m_Destructible.HitPointChangeEvent -= ValueChange;
    }    

    public void ValueChange(int value)
    {
        m_HealthSlider.value = value;
    }
}
