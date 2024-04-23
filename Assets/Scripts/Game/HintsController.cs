using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintsController : MonoBehaviour
{
    [SerializeField] private Transform m_Target;
    [SerializeField] private Vector3 m_Offset;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;

    private void Update()
    {
        transform.position = m_Target.position + m_Offset;
    }

    public void HintsActivation(Sprite sprite)
    {
        m_SpriteRenderer.sprite = sprite;
        m_SpriteRenderer.enabled = true;
    }
    public void HintsDeactivation()
    {
        m_SpriteRenderer.enabled = false;
    }
}
