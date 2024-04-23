using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Finish : MonoBehaviour
{
    public UnityEvent OnFinish;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<Player>() == null) return;
        
        OnFinish?.Invoke();
        
    }
}
