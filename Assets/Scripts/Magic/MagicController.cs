using System.Collections;
using UnityEngine;

public class MagicController : MonoBehaviour
{
    [SerializeField] private float m_CoolDown;
    [SerializeField] private Magic[] m_RightButtonMagics;
    [SerializeField] private Magic[] m_LeftButtonMagics;

    private MagicType magicType;
    private Magic currentRightButtonMagic;
    private Magic currentLeftButtonMagic;

    private bool isCoolDawn;
    private void Start()
    {
        SwapMagicType(MagicType.Wind);
    }
    public void UseRightButtonMagic()
    {
        if (isCoolDawn) return;
        StartCoroutine(CoolDown(currentRightButtonMagic.MagicDuration));
        currentRightButtonMagic.UseMagic();
    }
    public void UseLeftButtonMagic()
    {
        if (isCoolDawn) return;
        StartCoroutine(CoolDown(currentLeftButtonMagic.MagicDuration));
        currentLeftButtonMagic.UseMagic();
    }

    public void LeftButtonMagicReset()
    {
        currentLeftButtonMagic.MagicReset();
        
    }
    public void RightButtonMagicReset()
    {
        currentRightButtonMagic.MagicReset();
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

    private IEnumerator CoolDown(float magicDuration)
    {
        isCoolDawn = true;
        yield return new WaitForSeconds(magicDuration);
        LeftButtonMagicReset();
        RightButtonMagicReset();
        yield return new WaitForSeconds(m_CoolDown );
        isCoolDawn = false;
    }
}
