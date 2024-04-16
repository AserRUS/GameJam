using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Storm : MonoBehaviour
{
    [SerializeField] float m_StormSpeed;
    [SerializeField] int m_Damage;
    [SerializeField] private float m_AirflowForce;

    private List<Destructible> destructibles = new List<Destructible>();
    private List<Rigidbody> objects = new List<Rigidbody>();

    private void FixedUpdate()
    {
        for (int i = 0; i < destructibles.Count; i++)
        {
            destructibles[i].RemoveHitpoints(m_Damage);
        }
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].AddForce(transform.up * m_AirflowForce);
        }
    }

    private void Update()
    {
        transform.Translate(transform.right * m_StormSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        Destructible destructible = other.GetComponent<Destructible>();
        if (destructible != null)
        {
            destructibles.Add(destructible);
        }
        
        Rigidbody rigidbody = other.GetComponent<Rigidbody>();
        if (rigidbody == null) return;
        objects.Add(rigidbody);
    }

    private void OnTriggerExit(Collider other)
    {
        Destructible destructible = other.GetComponent<Destructible>();
        if (destructible != null)
        {
            if (destructibles.Contains(destructible))
            {
                destructibles.Remove(destructible);
            }
        }
        Rigidbody rigidbody = other.GetComponent<Rigidbody>();
        if (rigidbody == null) return;

        if (objects.Contains(rigidbody))
        {
            objects.Remove(rigidbody);
        }

    }
}
