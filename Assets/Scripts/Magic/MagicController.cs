using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicController : MonoBehaviour
{ 
    [SerializeField] private Magic[] m_RightButtonMagics;
    [SerializeField] private Magic[] m_LeftButtonMagics;

    private MagicType magicType;
    private Magic currentRightButtonMagic;
    private Magic currentLeftButtonMagic;

    private bool isRightButtonMagic;
    private bool isLeftButtonMagic;
    private void Start()
    {
        SwapMagicType(MagicType.Wind);
    }
    public void UseRightButtonMagic()
    {
        if (isLeftButtonMagic) return;
        isRightButtonMagic = true;
        currentRightButtonMagic.UseMagic();
    }
    public void UseLeftButtonMagic()
    {
        if (isRightButtonMagic) return;
        isLeftButtonMagic = true;
        currentLeftButtonMagic.UseMagic();
    }

    public void LeftButtonMagicReset()
    {
        currentLeftButtonMagic.MagicReset();
        isLeftButtonMagic = false;
    }
    public void RightButtonMagicReset()
    {
        currentRightButtonMagic.MagicReset();
        isRightButtonMagic = false;
    }
    public void SwapMagicType(MagicType type)
    {
        magicType = type;

        for (int i = 0; i < m_RightButtonMagics.Length; i++)
        {
            if (m_RightButtonMagics[i].MagicType == magicType)
            {
                currentRightButtonMagic = m_RightButtonMagics[i];
            }
        }
        for (int i = 0; i < m_LeftButtonMagics.Length; i++)
        {
            if (m_LeftButtonMagics[i].MagicType == magicType)
            {
                currentLeftButtonMagic = m_LeftButtonMagics[i];
            }
        }
    }
}
