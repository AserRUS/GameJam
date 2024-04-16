using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPanel : MonoBehaviour
{
    [SerializeField] private GameObject m_DeathPanel;
    [SerializeField] private Destructible m_Destructible;

    private void Start()
    {
        m_Destructible.DeathEvent += DeathPanelActivate;
    }
    private void OnDestroy()
    {
        m_Destructible.DeathEvent -= DeathPanelActivate;
    }
    private void DeathPanelActivate()
    {
        m_DeathPanel.SetActive(true);
    }
}
