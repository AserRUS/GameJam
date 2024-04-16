using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Fire : MonoBehaviour
{
    [SerializeField] private int m_Damage;
    [SerializeField] private float impactForce;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player == null) return;

        player.RemoveHitpoints(m_Damage);

        PlayerMovement playerMovement = other.transform.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.Stun();
        }

        Rigidbody rb = other.transform.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            Vector3 direction = other.transform.position - transform.position;            
            rb.AddForce(direction * impactForce, ForceMode.Impulse);            
        }
    }
}
