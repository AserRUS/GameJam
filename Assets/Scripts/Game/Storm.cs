using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Storm : MonoBehaviour
{
    [SerializeField] float m_StormSpeed;
    [SerializeField] int m_Damage;

    private List<Destructible> destructibles = new List<Destructible>();

    private void FixedUpdate()
    {
        for (int i = 0; i < destructibles.Count; i++)
        {
            destructibles[i].RemoveHitpoints(m_Damage);
        }
    }

    private void Update()
    {
        transform.Translate(transform.right * m_StormSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        Destructible destructible = other.GetComponent<Destructible>();
        if (destructible == null) return;
        destructibles.Add(destructible);
    }

    private void OnTriggerExit(Collider other)
    {
        Destructible destructible = other.GetComponent<Destructible>();
        if (destructible == null) return;

        if (destructibles.Contains(destructible))
        {
            destructibles.Remove(destructible);
        }
    }
}
