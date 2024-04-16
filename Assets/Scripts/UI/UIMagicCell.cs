using UnityEngine;
using UnityEngine.UI;

public class UIMagicCell : MonoBehaviour
{
    [SerializeField] private Image m_MagicCellImage;
    [SerializeField] private Image m_CloseImage;
    [SerializeField] private Text m_CoolDownTimerText;

    private void Start()
    {
        EndCoolDawn();
    }

    

    public void UpdateMagicCellImage(Magic magic)
    {
        m_MagicCellImage.sprite = magic.MagicImage; 
    }

    public void UpdateCoolDownTimer(float time)
    {
        m_CoolDownTimerText.text = time.ToString("f0");
    }

    public void StartCoolDawn()
    {
        m_CloseImage.enabled = true;
        m_CoolDownTimerText.enabled = true;
    }

    public void EndCoolDawn()
    {
        m_CloseImage.enabled = false;
        m_CoolDownTimerText.enabled = false;
    }
}
