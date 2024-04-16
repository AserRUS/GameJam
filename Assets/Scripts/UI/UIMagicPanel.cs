using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMagicPanel : MonoBehaviour
{
    [SerializeField] private UIWeatherCell[] m_UIWeatherCells;
    [SerializeField] private MagicController m_MagicController;
    [SerializeField] private UIMagicCell m_RightMagicCell;
    [SerializeField] private UIMagicCell m_LeftMagicCell;

    private void Start()
    {
        m_MagicController.UpdateSelectedMagicType += OnUpdateSelectedMagicType;

        m_MagicController.StartCoolDown += StartCoolDown;        
        m_MagicController.EndCoolDown += EndCoolDawn;
        m_MagicController.CoolDownTick += UpdateCoolDownTimer;
    }
    private void OnDestroy()
    {
        m_MagicController.UpdateSelectedMagicType -= OnUpdateSelectedMagicType;


        m_MagicController.StartCoolDown -= StartCoolDown;
        m_MagicController.EndCoolDown -= EndCoolDawn;
        m_MagicController.CoolDownTick -= UpdateCoolDownTimer;
    }

    private void OnUpdateSelectedMagicType()
    {
        for (int i = 0; i < m_UIWeatherCells.Length; i++)
        {
            if (m_UIWeatherCells[i].MagicType == m_MagicController.CurrentMagicType)
            {
                m_UIWeatherCells[i].Select();
            }
            else
            {
                m_UIWeatherCells[i].UnSelect();
            }
        }
        m_RightMagicCell.UpdateMagicCellImage(m_MagicController.CurrentRightButtonMagic);
        m_LeftMagicCell.UpdateMagicCellImage(m_MagicController.CurrentLeftButtonMagic);
    }

    private void StartCoolDown()
    {
        m_RightMagicCell.StartCoolDawn();
        m_LeftMagicCell.StartCoolDawn();
    }
    private void EndCoolDawn()
    {
        m_RightMagicCell.EndCoolDawn();
        m_LeftMagicCell.EndCoolDawn();
    }
    private void UpdateCoolDownTimer(float time)
    {
        m_RightMagicCell.UpdateCoolDownTimer(time);
        m_LeftMagicCell.UpdateCoolDownTimer(time);
    }
}
