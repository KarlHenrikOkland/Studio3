using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlatform : MonoBehaviour
{
    MovingElevator platform;

    private void Start() 
    {
        platform = GetComponent<MovingElevator>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        platform.canMove = true;
    }
}
