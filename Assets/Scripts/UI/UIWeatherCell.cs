using UnityEngine;
using UnityEngine.UI;

public class UIWeatherCell : MonoBehaviour
{    
    public MagicType MagicType => m_MagicType;

    [SerializeField] private GameObject m_Frame;
    [SerializeField] private MagicType m_MagicType;


    public void Select()
    {
        m_Frame.SetActive(true);
    }

    public void UnSelect()
    {
        m_Frame.SetActive(false);
    }
}
