using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MagicController : MonoBehaviour
{
    public event UnityAction UpdateSelectedMagicType;
    public event UnityAction StartCoolDown;
    public event UnityAction EndCoolDown;
    public event UnityAction<float> CoolDownTick;

    public MagicType CurrentMagicType => currentMagicType;
    public Magic CurrentRightButtonMagic => currentRightButtonMagic;
    public Magic CurrentLeftButtonMagic => currentLeftButtonMagic;
    public float CoolDownTimer => coolDownTimer;

    [SerializeField] private Magic[] m_RightButtonMagics;
    [SerializeField] private Magic[] m_LeftButtonMagics;
    [SerializeField] private PlayerAnimationState m_AnimationState; 

    private MagicType currentMagicType;
    private Magic currentRightButtonMagic;
    private Magic currentLeftButtonMagic;
    private bool isCoolDawn;
    private float coolDownTimer;
    private void Start()
    {
        SwapMagicType(MagicType.Wind);
    }

    private void Update()
    {
        if (coolDownTimer > 0)
        {
            coolDownTimer -= Time.deltaTime;
            CoolDownTick?.Invoke(coolDownTimer);
        }
        else
        {
            EndCoolDown?.Invoke();
            isCoolDawn = false;
            enabled = false;
        }
    }

    public void UseRightButtonMagic()
    {
        if (isCoolDawn) return;
        m_AnimationState.MagicAnimation();
        currentRightButtonMagic.UseMagic();
        CoolDown(currentRightButtonMagic.CoolDown);
    }
    public void UseLeftButtonMagic()
    {
        if (isCoolDawn) return;
        m_AnimationState.MagicAnimation();
        currentLeftButtonMagic.UseMagic();
        CoolDown(currentLeftButtonMagic.CoolDown);
    }    

    public void SwapMagicType(MagicType type)
    {
        currentMagicType = type;                

        for (int i = 0; i < m_RightButtonMagics.Length; i++)
        {
            if (m_RightButtonMagics[i].MagicType == currentMagicType)
            {
                currentRightButtonMagic = m_RightButtonMagics[i];
            }
        }
        for (int i = 0; i < m_LeftButtonMagics.Length; i++)
        {
            if (m_LeftButtonMagics[i].MagicType == currentMagicType)
            {
                currentLeftButtonMagic = m_LeftButtonMagics[i];
            }
        }

        UpdateSelectedMagicType?.Invoke();
    }

    private void CoolDown(float coolDown)
    {
        coolDownTimer = coolDown;
        StartCoolDown?.Invoke();
        isCoolDawn = true;
        enabled = true;
    }
}
