using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionTrigger : MonoBehaviour
{
    public UnityEvent onTriggerEnterEvent;
    public UnityEvent onTriggerExitEvent;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the trigger object has a specific tag, if necessary
        if (other.gameObject.CompareTag("Player"))
        {
            // Trigger the  onTriggerEnterEvent UnityEvent
            onTriggerEnterEvent.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the trigger object has a specific tag, if necessary
        if (other.gameObject.CompareTag("Player"))
        {
            // Trigger the  onTriggerEnterEvent UnityEvent
            onTriggerExitEvent.Invoke();
        }
    }
}
