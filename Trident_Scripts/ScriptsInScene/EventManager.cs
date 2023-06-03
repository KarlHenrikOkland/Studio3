using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public bool isInRange;
    public UnityEvent interactAction;

    // Update is called once per frame
    void Update()
    {
        if (isInRange)
        {


            if (Input.GetKeyDown(KeyCode.E))
            {
                //Interaction starts
                interactAction.Invoke();
            }

        }

    }

    private void OnTriggerEnter(Collider collision)


    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
        }

    }
    private void OnTriggerExit(Collider collision)

    {
        if (collision.gameObject.CompareTag("Player"))

        {
            isInRange = false;
        }

    }


}

