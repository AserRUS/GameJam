using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Hint : MonoBehaviour
{
    [SerializeField] private HintsController m_HintsController;
    [SerializeField] private Sprite m_HintSprite;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<Player>() == null) return;

        m_HintsController.HintsActivation(m_HintSprite);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.GetComponent<Player>() == null) return;

        m_HintsController.HintsDeactivation();
    }
}
